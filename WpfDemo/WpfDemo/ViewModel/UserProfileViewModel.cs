using System;
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
using WpfDemo.View;
using WpfDemo.ViewModel.Command;

namespace WpfDemo.ViewModel
{
    public class UserProfileViewModel : ViewModelBase
    {
        private User _currentUser;
        private TaskViewModel _selectedTask;
        public ObservableCollection<User> UserListForTaskList { get; } = new ObservableCollection<User>();

        private ObservableCollection<TaskViewModel> _taskList = new ObservableCollection<TaskViewModel>();
        public ObservableCollection<Task> TaskListForRecordList { get; } = new ObservableCollection<Task>();
        private ObservableCollection<RecordViewModel> _recordList = new ObservableCollection<RecordViewModel>();


        public ObservableCollection<TaskViewModel> TaskList
        {
            get
            {
                return _taskList;
            }
        }
        public ObservableCollection<RecordViewModel> RecordList
        {
            get
            {
                return _recordList;
            }
        }

        public User CurrentUser
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


        public TaskViewModel SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
            }
        }


        private DateTime _deadlineFrom = DateTime.Today.AddYears(100);
        private DateTime _deadlineFromLowest;
        public DateTime DeadlineFrom
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

        private DateTime _deadlineTo = DateTime.Parse("0001.01.01");
        private DateTime _deadlineToHighest;
        public DateTime DeadlineTo
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


        private DateTime _dateFrom = DateTime.Today;
        private DateTime _dateFromLowest;
        public DateTime DateFrom
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

        private DateTime _dateTo = DateTime.Parse("0001.01.01");
        private DateTime _dateToHighest;
        public DateTime DateTo
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

        private int _durationFrom = 720;
        private int _durationFromLowest;
        public int DurationFrom
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

        private int _durationTo = 0;
        private int _durationToHighest;
        public int DurationTo
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

        public RelayCommand ShowTaskCommand { get; private set; }
        public RelayCommand SearchingTaskListCommand { get; private set; }
        public RelayCommand ResetTaskListCommand { get; private set; }
        public RelayCommand SearchingRecordListCommand { get; private set; }
        public RelayCommand ResetRecordListCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }

        public UserProfileViewModel(int userid)
        {
            LoadTasks(userid); // Betolti a Felhasznalo Feladatait
            LoadRecords(userid); // Betolti a Felhasznalo Rogziteseit

            ShowTaskCommand = new RelayCommand(ShowTask, CanShowTask);
            SearchingTaskListCommand = new RelayCommand(SearchTaskList, CanExecuteSearchTaskList);
            ResetTaskListCommand = new RelayCommand(ResetTaskList, CanExecuteResetTaskList);
            SearchingRecordListCommand = new RelayCommand(SearchRecordList, CanExecuteSearchRecordList);
            ResetRecordListCommand = new RelayCommand(ResetRecordList, CanExecuteResetRecordList);
            DeleteCommand = new RelayCommand(DeleteTask, CanDeleteTask);
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

                //OnTaskCreated(SelectedTask);
            }
            else
            {
                UserProfileTaskView Ipage = new UserProfileTaskView();
                (Ipage.DataContext as UserProfileTaskViewModel).CurrentTask = SelectedTask.Task;
                Ipage.ShowDialog();
            }

            LoadTasks(CurrentUser.IdUser);
        }
        /*private void OnTaskCreated(Task task) NEEEEM JOOOO!!!!
        {
            TaskList.Add(task);
            //SelectedTask = new UserProfileTaskViewModel(new Task() { Deadline = DateTime.Today.AddDays(1) });
            //SelectedTask.TaskCreated += OnTaskCreated;
        }*/


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
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
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
                if (String.IsNullOrWhiteSpace(_searchRecordListValue))
                {
                    _recordList.Clear();

                    var tasks = new TaskRepository(new TaskLogic()).GetUserTasks(CurrentUser.IdUser);
                    tasks.ForEach(task => TaskListForRecordList.Add(task));

                    var records = new RecordRepository(new RecordLogic()).GetUserRecords(CurrentUser.IdUser).Where(record =>
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

                    var records = new RecordRepository(new RecordLogic()).GetUserRecords(CurrentUser.IdUser).Where(record =>
                                    (record.Date >= _dateFrom && record.Date <= _dateTo) && (record.Duration >= _durationFrom && record.Duration <= _durationTo)
                                    && (record.Comment.Contains(_searchRecordListValue) || new TaskRepository(new TaskLogic()).GetTaskByID(record.Task_idTask).Title.Contains(_searchRecordListValue))).ToList();

                    records.ForEach(record =>
                    {
                        var recordViewModel = new RecordViewModel(record, TaskListForRecordList.ToList());
                        recordViewModel.Task = TaskListForRecordList.First(task => task.IdTask == record.Task_idTask);
                        _recordList.Add(recordViewModel);
                    });
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
        private void LoadRecords(int userid)
        {
            _recordList.Clear();
            TaskListForRecordList.Clear();

            try
            {
                var tasks = new TaskRepository(new TaskLogic()).GetUserTasks(userid);
                tasks.ForEach(task => TaskListForRecordList.Add(task));

                var records = new RecordRepository(new RecordLogic()).GetUserRecords(userid);
                records.ForEach(record =>
                {
                    var recordViewModel = new RecordViewModel(record, TaskListForRecordList.ToList());
                    recordViewModel.Task = TaskListForRecordList.First(task => task.IdTask == record.Task_idTask);
                    _recordList.Add(recordViewModel);
                });

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
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
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
    }
}
