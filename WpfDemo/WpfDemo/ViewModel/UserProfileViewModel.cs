using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Windows;
using TimeSheet.DataAccess;
using TimeSheet.Logic;
using TimeSheet.Model;
using TimeSheet.Resource;
using WpfDemo.Components;
using WpfDemo.View;
using WpfDemo.ViewModel.Command;

namespace WpfDemo.ViewModel
{
    public class UserProfileViewModel : ViewModelBase
    {
        private User _currentUser; // Jelenlegi felhasznalot tarolja
        private TaskViewModel _selectedTask; // kivalasztott feladatot tarolja
        public ObservableCollection<User> UserListForTaskList { get; } = new ObservableCollection<User>(); // TaskViewModel miatt kell a feladatok betoltesehez

        private ObservableCollection<TaskViewModel> _taskList = new ObservableCollection<TaskViewModel>(); // Feladatok listaja
        private ObservableCollection<TaskForChartViewModel> _taskListForChart = new ObservableCollection<TaskForChartViewModel>(); // Feladatok listaja a kordiagramhoz
        public ObservableCollection<Task> TaskListForRecordList { get; } = new ObservableCollection<Task>(); // Feladatok listaja a rogzites lista miatt
        private ObservableCollection<RecordViewModel> _recordList = new ObservableCollection<RecordViewModel>(); // Rogzites listaja
        private ObservableCollection<RecordViewModel> _recordListForChart = new ObservableCollection<RecordViewModel>(); // Rogzites listaja az oszlopdiagramhoz


        public ObservableCollection<TaskViewModel> TaskList // Feladat lista bindolashoz
        {
            get
            {
                return _taskList;
            }
        }
        public ObservableCollection<RecordViewModel> RecordList // Rogzites lista bindolashoz
        {
            get
            {
                return _recordList;
            }
        }

        public ObservableCollection<TaskForChartViewModel> TaskListForChart // Feladat lista bindolashoz a kordiagramnak
        {
            get
            {
                return _taskListForChart;
            }
        }
        public ObservableCollection<RecordViewModel> RecordListForChart // Rogzites lista bindolashoz az oszlopdiagramnak
        {
            get
            {
                return _recordListForChart;
            }
        }

        public User CurrentUser // Jelenlegi felhasznlo
        {
            get
            {
                return _currentUser;
            }
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }


        public TaskViewModel SelectedTask // kivalasztott feladat bindolashoz
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));

                LoadRecords(CurrentUser.IdUser); // Betolti a feladat rogziteseit a listaba
                LoadRecordsForChart(CurrentUser.IdUser); // Betolti a feladat rogziteseit az oszlopdiagramba

                // Ha a feladatot valt es annak nincs rogzitese akkor kiirja ha van akkor nem irja ki
                OnPropertyChanged(nameof(UserProfileViewRecordListMessageVisibility));
                OnPropertyChanged(nameof(UserProfileViewRecordListMessage));

                // Ha feladatot valt akkor ne mentse el az oszlopdiagramnal kivalasztott szurest
                _chartTimeSorterForColumSeries = ChartTimeSorter.AllTime;
                OnPropertyChanged(nameof(ChartTimeSorterForColumSeries));
            }
        }


        private DateTime _deadlineFrom = DateTime.Today.AddYears(100); // hataridotol tarolashoz (szuresnel/keresesnel)
        private DateTime _deadlineFromLowest; // legkorabbi hataridot tarolja (szuresnel/keresesnel)
        public DateTime DeadlineFrom // hataridotol bindolashoz (szuresnel/keresesnel)
        {
            get
            {
                return _deadlineFrom;
            }
            set
            {
                _deadlineFrom = value;
                OnPropertyChanged(nameof(DeadlineFrom));
            }
        }

        private DateTime _deadlineTo = DateTime.Parse("0001.01.01"); // hataridoig tarolashoz (szuresnel/keresesnel)
        private DateTime _deadlineToHighest; // legkesobbi hataridot tarolja (szuresnel/keresesnel)
        public DateTime DeadlineTo // hataridoig bindolashoz (szuresnel/keresesnel)
        {
            get
            {
                return _deadlineTo;
            }
            set
            {
                _deadlineTo = value;
                OnPropertyChanged(nameof(DeadlineTo));
            }
        }

        private string _searchTaskListValue;
        public string SearchTaskListValue // Feladatok listajaban valo keresesi szoveg bindolashoz
        {
            get { return _searchTaskListValue; }
            set
            {
                _searchTaskListValue = value;
                OnPropertyChanged(nameof(SearchTaskListValue));
            }
        }


        private DateTime _dateFrom = DateTime.Today; // datumtol tarolasahoz (szuresnel/keresesnel)
        private DateTime _dateFromLowest; // Legkorabbi datum tarolasahoz (szuresnel/keresesnel)
        public DateTime DateFrom // datumtol bindolashoz (szuresnel/keresesnel)
        {
            get
            {
                return _dateFrom;
            }
            set
            {
                _dateFrom = value;
                OnPropertyChanged(nameof(DateFrom));
            }
        }

        private DateTime _dateTo = DateTime.Parse("0001.01.01"); // datumig tarolasahoz (szuresnel/keresesnel)
        private DateTime _dateToHighest; // Legkesobbi datum tarolasahoz (szuresnel/keresesnel)
        public DateTime DateTo // datumig bindolashoz (szuresnel/keresesnel)
        {
            get
            {
                return _dateTo;
            }
            set
            {
                _dateTo = value;
                OnPropertyChanged(nameof(DateTo));
            }
        }

        private int _durationFrom = 720; // idotartamtol tarolasahoz (szuresnel/keresesnel)
        private int _durationFromLowest; // legkisebb idotartam tarolashoz (szuresnel/keresesnel)
        public int DurationFrom // idotartamtol bindolashoz (szuresnel/keresesnel)
        {
            get
            {
                return _durationFrom;
            }
            set
            {
                _durationFrom = value;
                OnPropertyChanged(nameof(DurationFrom));
            }
        }
        public string DurationFromFormat // Idotartam megfelelo kiirasanak a bindaloshoz
        {
            get
            {
                return TimeSpan.FromMinutes(_durationFrom).ToString("hh':'mm");
            }
            set
            {
                try
                {
                    TimeSpan input = TimeSpan.ParseExact(value, "hh':'mm", null);

                    _durationFrom = (int)input.TotalMinutes;
                    OnPropertyChanged(nameof(DurationFrom));
                }
                catch (FormatException)
                {
                    //Do Nothing
                }
            }
        }

        private int _durationTo = 0; // idotartamig tarolashoz (szuresnel/keresesnel)
        private int _durationToHighest; // legnagyobb idotartam tarolashoz (szuresnel/keresesnel)
        public int DurationTo // idotartamig bindolashoz (szuresnel/keresesnel)
        {
            get
            {
                return _durationTo;
            }
            set
            {
                _durationTo = value;
                OnPropertyChanged(nameof(DurationTo));
            }
        }
        public string DurationToFormat // Idotartam megfelelo kiirasanak a bindaloshoz
        {
            get
            {
                return TimeSpan.FromMinutes(_durationTo).ToString("hh':'mm");
            }
            set
            {
                try
                {
                    TimeSpan input = TimeSpan.ParseExact(value, "hh':'mm", null);

                    _durationTo = (int)input.TotalMinutes;
                    OnPropertyChanged(nameof(DurationTo));
                }
                catch (FormatException)
                {
                    //Do Nothing
                }
            }
        }

        private string _searchRecordListValue;
        public string SearchRecordListValue // Rogzitesi listaban valo keresesi szoveg bindolashoz
        {
            get { return _searchRecordListValue; }
            set
            {
                _searchRecordListValue = value;
                OnPropertyChanged(nameof(SearchRecordListValue));
            }
        }

        private ChartTimeSorter _chartTimeSorterForPieSeries;
        public ChartTimeSorter ChartTimeSorterForPieSeries // Kordiagram szureshez a bindolas
        {
            get
            {
                return _chartTimeSorterForPieSeries;
            }
            set
            {
                _chartTimeSorterForPieSeries = value;
                OnPropertyChanged(nameof(ChartTimeSorterForPieSeries));
                LoadTasksForChart(CurrentUser.IdUser); // ha valtozik akkor betolti ujra a listat a szures alapjan
            }
        }

        public Dictionary<ChartTimeSorter, string> ChartTimeSorterListForPieSeries // Kordiagram szureshez(Mindenkor,Évben,Hónapban,Héten)
        {
            get
            {
                return Enum.GetValues(typeof(ChartTimeSorter)).Cast<ChartTimeSorter>()
                .ToDictionary<ChartTimeSorter, ChartTimeSorter, string>(
                item => item,
                item => ResourceHandler.GetResourceString(item.ToString()));
            }
        }

        private ChartTimeSorter _chartTimeSorterForColumSeries;
        public ChartTimeSorter ChartTimeSorterForColumSeries // Oszlopdiagram szureshez a bindolas
        {
            get
            {
                return _chartTimeSorterForColumSeries;
            }
            set
            {
                _chartTimeSorterForColumSeries = value;
                OnPropertyChanged(nameof(ChartTimeSorterForColumSeries));
                ReloadRecordsForChartBySelectedItem(CurrentUser.IdUser); // ha valtozik akkor betolti ujra a listat a szures alapjan
            }
        }

        public Dictionary<ChartTimeSorter, string> ChartTimeSorterListForColumSeries // Oszlopdiagram szureshez(Mindenkor,Évben,Hónapban,Héten)
        {
            get
            {
                return Enum.GetValues(typeof(ChartTimeSorter)).Cast<ChartTimeSorter>()
                .ToDictionary<ChartTimeSorter, ChartTimeSorter, string>(
                item => item,
                item => ResourceHandler.GetResourceString(item.ToString()));
            }
        }

        public Visibility AddTaskButtonVisibility // uj Feladat hozzaadas gomb lathatosaga(Admin eseteben lathato)
        {
            get
            {
                return LoginViewModel.LoggedUser.Status == 0 ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public Visibility UserProfileViewTasksContextMenuVisibility // (Delete Header Visibility) Csak Admin eseteben jelenik meg jobb klikkre egy torles lehetoseg
        {
            get
            {
                return LoginViewModel.LoggedUser.Status == 0 ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility UserProfileViewTaskListMessageVisibility // Ha nincs a felhasznalonak feladata akkor az kiirja a listaba/diagramba
        {
            get
            {
                return TaskList.Count < 1 ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility UserProfileViewRecordListMessageVisibility // Ha nincs kivalasztva feladat vagy a feladathoz rogzites akkor azt kiirja a listaba/diagramba
        {
            get
            {
                return _selectedTask == null || RecordList.Count < 1 ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public string UserProfileViewRecordListMessage // Az uzenet bindolasa ha nincs kivalasztva feladat vagy a feladathoz rogzites akkor azt kiirja a listaba/diagramba
        {
            get
            {
                if (_selectedTask == null)
                {
                    return Resources.UserProfileRecordListNoTaskSelectedMessage;
                }
                else
                {
                    return Resources.UserProfileRecordListNoRecordsForTaskMessage;
                }
            }
        }

        public RelayCommand ShowTaskCommand { get; private set; }
        public RelayCommand SearchingTaskListCommand { get; private set; }
        public RelayCommand ResetTaskListCommand { get; private set; }
        public RelayCommand SearchingRecordListCommand { get; private set; }
        public RelayCommand ResetRecordListCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }
        public RelayCommand SpentTimeWithCommand { get; private set; }

        public UserProfileViewModel(int userid)
        {
            LoadTasks(userid); // Betolti a Felhasznalo Feladatait
            LoadRecords(userid); // Betolti a Felhasznalo Rogziteseit
            LoadTasksForChart(userid); // Betolti a Felhasznlo Feladatait a kordiagramba
            LoadRecordsForChart(userid); // Betolti a Felhasznalo feladatanak rogziteseit az oszlopdiagramba

            ShowTaskCommand = new RelayCommand(ShowTask, CanShowTask);
            SearchingTaskListCommand = new RelayCommand(SearchTaskList, CanExecuteSearchTaskList);
            ResetTaskListCommand = new RelayCommand(ResetTaskList, CanExecuteResetTaskList);
            SearchingRecordListCommand = new RelayCommand(SearchRecordList, CanExecuteSearchRecordList);
            ResetRecordListCommand = new RelayCommand(ResetRecordList, CanExecuteResetRecordList);
            DeleteCommand = new RelayCommand(DeleteTask, CanDeleteTask);
            SpentTimeWithCommand = new RelayCommand(CalculateDurationForTask, CanExecuteCalculateDurationForTask);
        }


        private void LoadTasks(int userid)
        {
            _taskList.Clear();
            UserListForTaskList.Clear();

            try
            {
                var user = new UserRepository(new UserLogic()).GetUserByID(userid);
                UserListForTaskList.Add(user);
                var tasks = new TaskRepository(new TaskLogic()).GetUserTasks(userid);

                tasks.ForEach(task =>
                {
                    var taskViewModel = new TaskViewModel(task, UserListForTaskList.ToList());
                    taskViewModel.User = UserListForTaskList.First(user => user.IdUser == task.User_idUser);
                    TaskList.Add(taskViewModel);
                });

                // Resetelje a Hataridoket a megfelelore
                _deadlineFrom = DateTime.Today.AddYears(1);
                if (TaskList.Count == 0) // ha nincs task akkor a mai datumot kapja meg
                {
                    _deadlineTo = DateTime.Today;
                }
                else
                {
                    _deadlineTo = DateTime.Parse("0001.01.01");
                }
                foreach (TaskViewModel taskViewModel in TaskList)
                {
                    if (taskViewModel.Task.Deadline < _deadlineFrom)
                    {
                        _deadlineFrom = taskViewModel.Task.Deadline;
                        _deadlineFromLowest = _deadlineFrom;
                    }
                    if (taskViewModel.Task.Deadline > _deadlineTo)
                    {
                        _deadlineTo = taskViewModel.Task.Deadline;
                        _deadlineToHighest = _deadlineTo;
                    }
                }

                OnPropertyChanged(nameof(DeadlineFrom)); // kell h megvaltozzon a DatePickerben a datum
                OnPropertyChanged(nameof(DeadlineTo));  // kell h megvaltozzon a DatePickerben a datum

                SortTaskListByDeadline(); // Rendezzuk a listat csokkeno sorrendben a Hataridok szerint
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void SortTaskListByDeadline() // Rendezzuk a listat csokkeno sorrendben a Hataridok szerint
        {
            var sortedTaskListByDeadline = TaskList.OrderByDescending(task => DateTime.Parse(task.Deadline.ToString()));

            sortedTaskListByDeadline.ToList().ForEach(task =>
            {
                TaskList.Remove(task);
                TaskList.Add(task);
            });
        }


        private void LoadTasksForChart(int userid)
        {
            _taskListForChart.Clear();
            try
            {
                var tasks = new TaskRepository(new TaskLogic()).GetUserTasks(userid);

                tasks.ForEach(task =>
                {
                    var taskViewModel = new TaskForChartViewModel();
                    taskViewModel.Title = task.Title;
                    taskViewModel.Duration = CalculateSumDuration(task.IdTask);
                    taskViewModel.DurationFormat =  TimeSpan.FromMinutes(taskViewModel.Duration).ToString("hh':'mm"); // atalakija az idotartam formatumat
                    TaskListForChart.Add(taskViewModel);
                });
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private int CalculateSumDuration(int taskid) // kiszamitja a megfelelo szures eseten az osszes idotartamat a feladatnak
        {
            int sumDuration = 0;

            if (_chartTimeSorterForPieSeries == ChartTimeSorter.ThisYear)
            {
                foreach (Record record in new RecordRepository(new RecordLogic()).GetTaskRecords(taskid).Where(record =>
                                                    record.Date > DateTime.Today.AddYears(-1)).ToList())
                {
                    sumDuration += record.Duration;
                }
            }
            else if (_chartTimeSorterForPieSeries == ChartTimeSorter.ThisMonth)
            {
                foreach (Record record in new RecordRepository(new RecordLogic()).GetTaskRecords(taskid).Where(record =>
                                    record.Date > DateTime.Today.AddMonths(-1)).ToList())
                {
                    sumDuration += record.Duration;
                }
            }
            else if (_chartTimeSorterForPieSeries == ChartTimeSorter.ThisWeek)
            {
                foreach (Record record in new RecordRepository(new RecordLogic()).GetTaskRecords(taskid).Where(record =>
                     record.Date > DateTime.Today.AddDays(-7)).ToList())
                {
                    sumDuration += record.Duration;
                }
            }
            else
            {
                foreach (Record record in new RecordRepository(new RecordLogic()).GetTaskRecords(taskid)) // a kivalasztott feladat rogziteseinek az idotartamat osszeadjuk
                {
                    sumDuration += record.Duration;
                }
            }

            return sumDuration;
        }


        private void LoadRecords(int userid)
        {
            _recordList.Clear();
            TaskListForRecordList.Clear();

            try
            {
                var tasks = new TaskRepository(new TaskLogic()).GetUserTasks(userid);
                tasks.ForEach(task => TaskListForRecordList.Add(task));

                if (SelectedTask != null)
                {
                    var records = new RecordRepository(new RecordLogic()).GetTaskRecords(SelectedTask.IdTask);
                    records.ForEach(record =>
                    {
                        var recordViewModel = new RecordViewModel(record, TaskListForRecordList.ToList());
                        recordViewModel.Task = TaskListForRecordList.First(task => task.IdTask == record.Task_idTask);
                        _recordList.Add(recordViewModel);
                    });
                }

                _durationFrom = 720;
                _durationTo = 0;
                _dateFrom = DateTime.Today;
                if (RecordList.Count == 0) // ha nincs rekord akkor a mai datumot kapja meg
                {
                    _dateTo = DateTime.Today;
                }
                else
                {
                    _dateTo = DateTime.Parse("0001.01.01");
                }
                foreach (RecordViewModel recordViewModel in RecordList) // frissitett listabol kicserelem ha van uj legrovidebb,leghosszabb Idotartam es legkorabbi vagy legkesobbi Datum
                {
                    if (recordViewModel.Record.Date < _dateFrom)
                    {
                        _dateFrom = recordViewModel.Record.Date;
                        _dateFromLowest = _dateFrom;
                    }
                    if (recordViewModel.Record.Date > _dateTo)
                    {
                        _dateTo = recordViewModel.Record.Date;
                        _dateToHighest = _dateTo;
                    }
                    if (recordViewModel.Record.Duration < _durationFrom)
                    {
                        _durationFrom = recordViewModel.Record.Duration;
                        _durationFromLowest = _durationFrom;
                    }
                    if (recordViewModel.Record.Duration > _durationTo)
                    {
                        _durationTo = recordViewModel.Record.Duration;
                        _durationToHighest = _durationTo;
                    }
                }

                OnPropertyChanged(nameof(DateFrom)); // kell h megvaltozzon a DatePickerben a datum
                OnPropertyChanged(nameof(DateTo));  // kell h megvaltozzon a DatePickerben a datum
                OnPropertyChanged(nameof(DurationFromFormat)); // h megvaltozzon a kiiras
                OnPropertyChanged(nameof(DurationToFormat)); // h megvaltozzon a kiiras

                SortRecordListtByDate(); // Rendezzuk a listat csokkeno sorrendben a Datumok szerint
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void SortRecordListtByDate() // Rendezzuk a listat csokkeno sorrendben a Datumok szerint
        {
            var sortedRecordListtByDate = RecordList.OrderByDescending(record => DateTime.Parse(record.Date.ToString()));

            sortedRecordListtByDate.ToList().ForEach(record =>
            {
                RecordList.Remove(record);
                RecordList.Add(record);
            });
        }

        private void LoadRecordsForChart(int userid)
        {
            _recordListForChart.Clear();
            TaskListForRecordList.Clear();

            try
            {
                var tasks = new TaskRepository(new TaskLogic()).GetUserTasks(userid);
                tasks.ForEach(task => TaskListForRecordList.Add(task));

                if (SelectedTask != null)
                {
                    var records = new RecordRepository(new RecordLogic()).GetTaskRecords(SelectedTask.IdTask);
                    records.ForEach(record =>
                    {
                        var recordViewModel = new RecordViewModel(record, TaskListForRecordList.ToList());
                        recordViewModel.Task = TaskListForRecordList.First(task => task.IdTask == record.Task_idTask);
                        _recordListForChart.Add(recordViewModel);

                        var sortedRecordListtByDate = RecordListForChart.OrderBy(record => DateTime.Parse(record.Date.ToString()));

                        sortedRecordListtByDate.ToList().ForEach(record =>
                        {
                            RecordListForChart.Remove(record);
                            RecordListForChart.Add(record);
                        });
                    });
                }
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private bool CanShowTask(object arg) // Csak Admin lathatja egy masik ablakban a feladatok adatait
        {
            return LoginViewModel.LoggedUser.Status != 0;
        }

        private void ShowTask(object obj)
        {
            if(_selectedTask == null) // Ha a valasztott feladat null akkor ujkent jon letre ellenkezo esetben pedig betolti a letezo feladat adatait
            {
                UserProfileTaskView Ipage = new UserProfileTaskView();
                (Ipage.DataContext as UserProfileTaskViewModel).CurrentTask.User_idUser = CurrentUser.IdUser;
                (Ipage.DataContext as UserProfileTaskViewModel).CurrentTask.Deadline = DateTime.Today.AddDays(1);
                Ipage.ShowDialog();
            }
            else
            {
                UserProfileTaskView Ipage = new UserProfileTaskView();
                (Ipage.DataContext as UserProfileTaskViewModel).CurrentTask = SelectedTask.Task;
                Ipage.ShowDialog();
            }

            LoadTasks(CurrentUser.IdUser);
        }


        private bool CanExecuteSearchTaskList(object arg)
        {
            return true;
        }
        private void SearchTaskList(object obj) // Lista szurese
        {
            try
            {
                if (String.IsNullOrWhiteSpace(_searchTaskListValue))
                {
                    _taskList.Clear();

                    var tasks = new TaskRepository(new TaskLogic()).GetUserTasks(CurrentUser.IdUser).Where(task =>
                                task.Deadline >= _deadlineFrom && task.Deadline <= _deadlineTo).ToList();

                    tasks.ForEach(task =>
                    {
                        var taskViewModel = new TaskViewModel(task, UserListForTaskList.ToList());
                        taskViewModel.User = UserListForTaskList.First(user => user.IdUser == task.User_idUser);
                        TaskList.Add(taskViewModel);
                    });
                }
                else
                {
                    _taskList.Clear();

                    var tasks = new TaskRepository(new TaskLogic()).GetUserTasks(CurrentUser.IdUser).Where(task =>
                                (task.Deadline >= _deadlineFrom && task.Deadline <= _deadlineTo) 
                                && (task.Title.Contains(_searchTaskListValue) || task.Description.Contains(_searchTaskListValue)
                                || ResourceHandler.GetResourceString(task.Status.ToString()).Contains(_searchTaskListValue))).ToList();

                    tasks.ForEach(task =>
                    {
                        var taskViewModel = new TaskViewModel(task, UserListForTaskList.ToList());
                        taskViewModel.User = UserListForTaskList.First(user => user.IdUser == task.User_idUser);
                        TaskList.Add(taskViewModel);
                    });
                }

                SortTaskListByDeadline(); // Rendezzuk a listat csokkeno sorrendben a Hataridok szerint
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private bool CanExecuteResetTaskList(object arg)
        {
            return true;
        }
        private void ResetTaskList(object obj) // Lista frissitese es keresesi ertekek visszaallitasa az alapra 
        {
            if (!String.IsNullOrWhiteSpace(_searchTaskListValue) || _deadlineFromLowest != _deadlineFrom || _deadlineToHighest != _deadlineTo) // Megnezem h valamelyik ertek valtozott-e es ha igen akkor frissitem
            {
                SearchTaskListValue = "";
                OnPropertyChanged(nameof(SearchTaskListValue));
                LoadTasks(CurrentUser.IdUser);
            }
        }


        private bool CanExecuteSearchRecordList(object arg)
        {
            return true;
        }
        private void SearchRecordList(object obj) // Lista szurese
        {
            try
            {
                if (_selectedTask != null)
                {
                    if (String.IsNullOrWhiteSpace(_searchRecordListValue))
                    {
                        _recordList.Clear();

                        var tasks = new TaskRepository(new TaskLogic()).GetUserTasks(CurrentUser.IdUser);
                        tasks.ForEach(task => TaskListForRecordList.Add(task));

                        var records = new RecordRepository(new RecordLogic()).GetTaskRecords(SelectedTask.IdTask).Where(record =>
                                        (record.Date >= _dateFrom && record.Date <= _dateTo) && (record.Duration >= _durationFrom && record.Duration <= _durationTo)).ToList();

                        records.ForEach(record =>
                        {
                            var recordViewModel = new RecordViewModel(record, TaskListForRecordList.ToList());
                            recordViewModel.Task = TaskListForRecordList.First(task => task.IdTask == record.Task_idTask);
                            _recordList.Add(recordViewModel);
                        });
                    }
                    else
                    {
                        _recordList.Clear();

                        var records = new RecordRepository(new RecordLogic()).GetTaskRecords(SelectedTask.IdTask).Where(record =>
                                        (record.Date >= _dateFrom && record.Date <= _dateTo) && (record.Duration >= _durationFrom && record.Duration <= _durationTo)
                                        && (record.Comment.Contains(_searchRecordListValue))).ToList();

                        records.ForEach(record =>
                        {
                            var recordViewModel = new RecordViewModel(record, TaskListForRecordList.ToList());
                            recordViewModel.Task = TaskListForRecordList.First(task => task.IdTask == record.Task_idTask);
                            _recordList.Add(recordViewModel);
                        });
                    }

                    SortRecordListtByDate(); // Rendezzuk a listat csokkeno sorrendben a Datumok szerint
                }
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool CanExecuteResetRecordList(object arg)
        {
            return true;
        }
        private void ResetRecordList(object obj) // Lista frissitese es keresesi ertekek visszaallitasa az alapra 
        {
            if (!String.IsNullOrWhiteSpace(_searchRecordListValue) || _dateFromLowest != _dateFrom || _dateToHighest != _dateTo || _durationFromLowest != _durationFrom 
                || _durationToHighest != _durationTo) // Megnezem h valamelyik ertek valtozott-e es ha igen akkor frissitem
            {
                SearchRecordListValue = "";
                OnPropertyChanged(nameof(SearchRecordListValue));
                LoadRecords(CurrentUser.IdUser);
            }
        }


        private bool CanDeleteTask(object arg)
        {
            return _selectedTask != null && LoginViewModel.LoggedUser.Status != 0;
        }

        private void DeleteTask(object obj)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show(Resources.TaskDeleteQuestion1 + SelectedTask.Title + Resources.TaskDeleteQuestion2, 
                                                Resources.Warning, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    new TaskRepository(new TaskLogic()).DeleteTask(SelectedTask.IdTask);

                    if (this.CurrentUser.Username != LoginViewModel.LoggedUser.Username) // ha a User nem admin(Miutan erre csak az Admin kepes)
                    {
                        SendNotificationEmail(SelectedTask.Title); // kuld emailt h toroltek a feladatat
                    }
                    LoadTasks(SelectedTask.User_idUser); // Frissiti a listat torles eseten
                }
                catch (SqlException)
                {
                    MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }


        private bool CanExecuteCalculateDurationForTask(object arg)
        {
            return _selectedTask != null;
        }
        private void CalculateDurationForTask(object obj) // Kiszamitja a feladattal foglalkozott/eltoltott idot
        {
            int spentTime = 0;

            foreach (Record record in new RecordRepository(new RecordLogic()).GetTaskRecords(SelectedTask.IdTask)) // a kivalasztott feladat rogziteseinek az idotartamat osszeadjuk
            {
                spentTime += record.Duration;
            }

            MessageBox.Show(Resources.SpentTimeMessage1 + SelectedTask.Title + Resources.SpentTimeMessage2 + TimeSpan.FromMinutes(spentTime).ToString("hh':'mm"), Resources.Information);
        }


        private void SendNotificationEmail(string TaskTitle)
        {
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            //client.Timeout = 10;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("wpfszakdoga@gmail.com", "Marszu99");
            string EmailSubject = "Task Notification";
            string EmailMessage = TaskTitle + " has been deleted!";
            MailMessage mm = new MailMessage("wpfszakdoga@gmail.com", CurrentUser.Email, EmailSubject, EmailMessage);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            client.Send(mm);
        }


        private void ReloadRecordsForChartBySelectedItem(int userid) // a szuresnel megfeleloen betolti a listat
        {
            if (_chartTimeSorterForColumSeries == ChartTimeSorter.ThisWeek)
            {
                _recordListForChart.Clear();
                TaskListForRecordList.Clear();

                try
                {
                    var tasks = new TaskRepository(new TaskLogic()).GetUserTasks(userid);
                    tasks.ForEach(task => TaskListForRecordList.Add(task));

                    if (SelectedTask != null)
                    {
                        var records = new RecordRepository(new RecordLogic()).GetTaskRecords(SelectedTask.IdTask).Where(record =>
                                        record.Date > DateTime.Today.AddDays(-7)).ToList();
                        records.ForEach(record =>
                        {
                            var recordViewModel = new RecordViewModel(record, TaskListForRecordList.ToList());
                            recordViewModel.Task = TaskListForRecordList.First(task => task.IdTask == record.Task_idTask);
                            _recordListForChart.Add(recordViewModel);

                            var sortedRecordListtByDate = RecordListForChart.OrderBy(record => DateTime.Parse(record.Date.ToString()));

                            sortedRecordListtByDate.ToList().ForEach(record =>
                            {
                                RecordListForChart.Remove(record);
                                RecordListForChart.Add(record);
                            });
                        });
                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else if (_chartTimeSorterForColumSeries == ChartTimeSorter.ThisMonth)
            {
                _recordListForChart.Clear();
                TaskListForRecordList.Clear();

                try
                {
                    var tasks = new TaskRepository(new TaskLogic()).GetUserTasks(userid);
                    tasks.ForEach(task => TaskListForRecordList.Add(task));

                    if (SelectedTask != null)
                    {
                        var records = new RecordRepository(new RecordLogic()).GetTaskRecords(SelectedTask.IdTask).Where(record =>
                                        record.Date > DateTime.Today.AddMonths(-1)).ToList();
                        records.ForEach(record =>
                        {
                            var recordViewModel = new RecordViewModel(record, TaskListForRecordList.ToList());
                            recordViewModel.Task = TaskListForRecordList.First(task => task.IdTask == record.Task_idTask);
                            _recordListForChart.Add(recordViewModel);

                            var sortedRecordListtByDate = RecordListForChart.OrderBy(record => DateTime.Parse(record.Date.ToString()));

                            sortedRecordListtByDate.ToList().ForEach(record =>
                            {
                                RecordListForChart.Remove(record);
                                RecordListForChart.Add(record);
                            });
                        });
                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else if (_chartTimeSorterForColumSeries == ChartTimeSorter.ThisYear)
            {
                _recordListForChart.Clear();
                TaskListForRecordList.Clear();

                try
                {
                    var tasks = new TaskRepository(new TaskLogic()).GetUserTasks(userid);
                    tasks.ForEach(task => TaskListForRecordList.Add(task));

                    if (SelectedTask != null)
                    {
                        var records = new RecordRepository(new RecordLogic()).GetTaskRecords(SelectedTask.IdTask).Where(record =>
                                        record.Date > DateTime.Today.AddYears(-1)).ToList();
                        records.ForEach(record =>
                        {
                            var recordViewModel = new RecordViewModel(record, TaskListForRecordList.ToList());
                            recordViewModel.Task = TaskListForRecordList.First(task => task.IdTask == record.Task_idTask);
                            _recordListForChart.Add(recordViewModel);

                            var sortedRecordListtByDate = RecordListForChart.OrderBy(record => DateTime.Parse(record.Date.ToString()));

                            sortedRecordListtByDate.ToList().ForEach(record =>
                            {
                                RecordListForChart.Remove(record);
                                RecordListForChart.Add(record);
                            });
                        });
                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                LoadRecordsForChart(userid);
            }
        }
    }
}
