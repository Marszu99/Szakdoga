using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using TimeSheet.DataAccess;
using TimeSheet.Logic;
using TimeSheet.Model;
using TimeSheet.Resource;
using WpfDemo.View;
using WpfDemo.ViewModel.Command;

namespace WpfDemo.ViewModel
{
    public class RecordManagementViewModel : ViewModelBase
    {
        private RecordManagementView _view;

        public ObservableCollection<User> UserList { get; } = new ObservableCollection<User>();
        public ObservableCollection<Task> TaskList { get; } = new ObservableCollection<Task>();
        public ObservableCollection<RecordViewModel> RecordList { get; } = new ObservableCollection<RecordViewModel>();


        private RecordViewModel _selectedRecord;
        public RecordViewModel SelectedRecord
        {
            get { return _selectedRecord; }
            set
            {
                _selectedRecord = value;
                OnPropertyChanged(nameof(SelectedRecord));
                OnPropertyChanged(nameof(SelectedRecordVisibility));
                OnPropertyChanged(nameof(ListRecordsViewContextMenuVisibility));
            }
        }


        private string _searchValue;
        public string SearchValue
        {
            get { return _searchValue; }
            set
            {
                _searchValue = value;
                OnPropertyChanged(nameof(SearchValue));
                SortingByCheckBox(_searchValue);
            }
        }

        /*private DateTime _dateFrom = DateTime.Today;
        public DateTime DateFrom
        {
            get
            {
                foreach (RecordViewModel recordViewModel in RecordList)
                {
                    if (record.Record.Date < _dateFrom)
                    {
                        _dateFrom = record.Record.Date;
                    }
                }
                return _dateFrom;
            }
            set
            {
                _dateFrom = value;
                OnPropertyChanged(nameof(DateFrom));
                //SortingByCheckBox(_searchValue);
            }
        }
        private DateTime _dateTo = DateTime.Parse("0001.01.01");
        public DateTime DateTo
        {
            get
            {
                foreach(RecordViewModel recordViewModel in RecordList)
                {
                    if(record.Record.Date > _dateTo)
                    {
                        _dateTo = record.Record.Date;
                    }
                }
                return _dateTo;
            }
            set
            {
                _dateTo = value;
                OnPropertyChanged(nameof(DateTo));
                //SortingByCheckBox(_searchValue);
            }
        }*/

        public Visibility RecordCheckBoxAndTextVisibility
        {
            get
            {
                return LoginViewModel.LoggedUser.Status == 0 ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public Visibility ListRecordsViewUserVisibility
        {
            get
            {
                return LoginViewModel.LoggedUser.Status == 0 ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility SelectedRecordVisibility
        {
            get
            {
                if (SelectedRecord != null)
                {
                    SelectedRecord.RecordCanceled += OnRecordCanceled;

                }
                return SelectedRecord == null ? Visibility.Hidden : Visibility.Visible;
            }
        }
        private void OnRecordCanceled(Object obj)
        {
            SelectedRecord = null;
        }

        public Visibility ListRecordsViewContextMenuVisibility // Delete Header Visibility
        {
            get
            {
                return SelectedRecord.User.Username != LoginViewModel.LoggedUser.Username ? Visibility.Collapsed : Visibility.Visible;
            }
        }


        public Dictionary<TaskStatus, string> TaskStatuses
        {
            get
            {
                return Enum.GetValues(typeof(TaskStatus)).Cast<TaskStatus>()
                    .Where(item => item != TaskStatus.Created)
                    .ToDictionary<TaskStatus, TaskStatus, string>(
                    item => item,
                    item => item.ToString());
            }
        }


        public RelayCommand CreateRecordCommand { get; private set; }
        public RelayCommand RefreshRecordListCommand { get; private set; }
        public RelayCommand SortingByCheckBoxCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }


        public RecordManagementViewModel(RecordManagementView view)
        {
            _view = view;

            LoadTasks();
            RefreshRecordList(view);

            CreateRecordCommand = new RelayCommand(CreateRecord, CanExecuteShow);
            RefreshRecordListCommand = new RelayCommand(RefreshRecordList, CanExecuteRefresh);
            SortingByCheckBoxCommand = new RelayCommand(SortingByCheckBox, CanExecuteSort);
            DeleteCommand = new RelayCommand(DeleteRecord, CanDeleteRecord);
        }


        private bool CanExecuteShow(object arg)
        {
            return true;
        }
        private void CreateRecord(object obj)
        {
            RefreshRecordList(obj);
            LoadTasks();
            SelectedRecord = new RecordViewModel(new Record() { Date = DateTime.Today, Duration = 210 },
                TaskList.Where(task => task.User_idUser == LoginViewModel.LoggedUser.IdUser && task.Status.ToString() != "Done").ToList());
            SelectedRecord.RecordCreated += OnRecordCreated;
        }
        private void OnRecordCreated(RecordViewModel recordViewModel)
        {
            RecordList.Add(recordViewModel);
            SelectedRecord = new RecordViewModel(new Record() { Date = DateTime.Today, Duration = 210 },
                TaskList.Where(task => task.User_idUser == LoginViewModel.LoggedUser.IdUser && task.Status.ToString() != "Done").ToList());
            SelectedRecord.RecordCreated += OnRecordCreated;
        }


        private bool CanExecuteRefresh(object arg)
        {
            return true;
        }
        private void RefreshRecordList(object obj)
        {
            LoadRecords();
            SortingByCheckBox(obj);
        }

        public void LoadRecords()
        {
            RecordList.Clear();

            try
            {
                var records = new RecordRepository(new RecordLogic()).GetUserRecords(LoginViewModel.LoggedUser.IdUser);
                records.ForEach(record =>
                {
                    var recordViewModel = new RecordViewModel(record, TaskList.ToList());
                    recordViewModel.Task = TaskList.First(task => task.IdTask == record.Task_idTask);
                    RecordList.Add(recordViewModel);
                });
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError);
            }
        }


        public void LoadTasks()
        {
            TaskList.Clear();

            try
            {
                var tasks = new TaskRepository(new TaskLogic()).GetAllTasks();
                tasks.ForEach(task => TaskList.Add(task));
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError);
            }
        }


        private bool CanExecuteSort(object arg)
        {
            return true;
        }
        private void SortingByCheckBox(object obj)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(_searchValue))
                {
                    if (_view.ShowingMyRecordsCheckBox.IsChecked == true)
                    {
                        RecordList.Clear();

                        var records = new RecordRepository(new RecordLogic()).GetUserRecords(LoginViewModel.LoggedUser.IdUser);
                        records.ForEach(record =>
                        {
                            var recordViewModel = new RecordViewModel(record, TaskList.ToList());
                            recordViewModel.Task = TaskList.First(task => task.IdTask == record.Task_idTask);
                            RecordList.Add(recordViewModel);
                        });
                    }
                    else
                    {
                        RecordList.Clear();

                        var records = new RecordRepository(new RecordLogic()).GetAllRecords();
                        records.ForEach(record =>
                        {
                            var recordViewModel = new RecordViewModel(record, TaskList.ToList());
                            recordViewModel.Task = TaskList.First(task => task.IdTask == record.Task_idTask);
                            RecordList.Add(recordViewModel);
                        });
                    }
                }
                else
                {
                    if (_view.ShowingMyRecordsCheckBox.IsChecked == true)
                    {
                        RecordList.Clear();
                        
                        var records = new RecordRepository(new RecordLogic()).GetUserRecords(LoginViewModel.LoggedUser.IdUser).Where(record => record.Date.ToShortDateString().Contains(_searchValue)
                                        || record.Comment.Contains(_searchValue) || record.Duration.ToString().Contains(_searchValue) 
                                        || new TaskRepository(new TaskLogic()).GetTaskByID(record.Task_idTask).Title.Contains(_searchValue)
                                        || new TaskRepository(new TaskLogic()).GetTaskByID(record.Task_idTask).Status.ToString().Contains(_searchValue)).ToList();
                        records.ForEach(record =>
                        {
                            var recordViewModel = new RecordViewModel(record, TaskList.ToList());
                            recordViewModel.Task = TaskList.First(task => task.IdTask == record.Task_idTask);
                            RecordList.Add(recordViewModel);
                        });
                    }
                    else
                    {
                        RecordList.Clear();

                        var records = new RecordRepository(new RecordLogic()).GetAllRecords().Where(record => record.Date.ToShortDateString().Contains(_searchValue)
                                        || record.Comment.Contains(_searchValue) || record.Duration.ToString().Contains(_searchValue) 
                                        || new UserRepository(new UserLogic()).GetUserByID(record.User_idUser).Username.Contains(_searchValue)
                                        || new TaskRepository(new TaskLogic()).GetTaskByID(record.Task_idTask).Title.Contains(_searchValue)
                                        || new TaskRepository(new TaskLogic()).GetTaskByID(record.Task_idTask).Status.ToString().Contains(_searchValue)).ToList();
                        records.ForEach(record =>
                        {
                            var recordViewModel = new RecordViewModel(record, TaskList.ToList());
                            recordViewModel.Task = TaskList.First(task => task.IdTask == record.Task_idTask);
                            RecordList.Add(recordViewModel);
                        });
                    }
                }
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError);
            }
        }


        private bool CanDeleteRecord(object arg)
        {
            return _selectedRecord != null && SelectedRecord.User.Username == LoginViewModel.LoggedUser.Username;
        }

        private void DeleteRecord(object obj)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show(Resources.RecordDeleteQuestion, Resources.Warning, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    new RecordRepository(new RecordLogic()).DeleteRecord(SelectedRecord.IdRecord);
                    MessageBox.Show(Resources.RecordDeletedMessage, Resources.Information, MessageBoxButton.OK, MessageBoxImage.Information);

                    RefreshRecordList(obj);
                }
                catch (SqlException)
                {
                    MessageBox.Show(Resources.ServerError);
                }
            }
        }
    }
}
