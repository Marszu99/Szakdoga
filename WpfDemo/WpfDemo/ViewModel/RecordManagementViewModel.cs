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

        public string SearchTextMargin
        {
            get
            {
                if (ResourceHandler.isEnglish)
                {
                    return "400 0 0 0";
                }
                else
                {
                    return "389 0 0 0";
                }
            }
        }

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
                return SelectedRecord == null ? Visibility.Hidden : Visibility.Visible;
                /*if (SelectedRecord == null)
                {
                    return Visibility.Hidden;
                }
                else
                {
                    if (SelectedRecord.User_Username == LoginViewModel.LoggedUser.Username || SelectedRecord.IdRecord == 0)
                    {
                        return Visibility.Visible;
                    }
                    else
                    {
                        return Visibility.Hidden;
                    }
                }*/
            }
        }

        public Visibility ListRecordsViewContextMenuVisibility // Delete Header Visibility
        {
            get
            {
                return SelectedRecord.User_Username != LoginViewModel.LoggedUser.Username ? Visibility.Collapsed : Visibility.Visible;
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
        public static RelayCommand RefreshRecordListCommand { get; private set; }
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
                TaskList
                .Where(task => task.User_idUser == LoginViewModel.LoggedUser.IdUser && task.Status.ToString() != "Done").ToList());
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
                MessageBox.Show("Server errror!");
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
                MessageBox.Show(ResourceHandler.GetResourceString("ServerError"));
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

                        var records = new RecordRepository(new RecordLogic()).GetUserRecords(LoginViewModel.LoggedUser.IdUser).Where(record => record.Task_Title.Contains(_searchValue) || record.User_Username.Contains(_searchValue) || record.Date.ToShortDateString().Contains(_searchValue) || record.Comment.Contains(_searchValue) || record.Duration.ToString().Contains(_searchValue) || record.Task_Status.ToString().Contains(_searchValue)).ToList();
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

                        var records = new RecordRepository(new RecordLogic()).GetAllRecords().Where(record => record.Task_Title.Contains(_searchValue) || record.User_Username.Contains(_searchValue) || record.Date.ToShortDateString().Contains(_searchValue) || record.Comment.Contains(_searchValue) || record.Duration.ToString().Contains(_searchValue) || record.Task_Status.ToString().Contains(_searchValue)).ToList();
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
                MessageBox.Show(ResourceHandler.GetResourceString("ServerError"));
            }
        }


        private bool CanDeleteRecord(object arg)
        {
            return _selectedRecord != null && SelectedRecord.User_Username == LoginViewModel.LoggedUser.Username;
        }

        private void DeleteRecord(object obj)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show(ResourceHandler.GetResourceString("RecordDeleteQuestion"), ResourceHandler.GetResourceString("Warning"), System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    new RecordRepository(new RecordLogic()).DeleteRecord(SelectedRecord.IdRecord);
                    MessageBox.Show(ResourceHandler.GetResourceString("RecordDeletedMessage"), ResourceHandler.GetResourceString("Information"), MessageBoxButton.OK, MessageBoxImage.Information);

                    RefreshRecordList(obj);
                }
                catch (SqlException)
                {
                    MessageBox.Show(ResourceHandler.GetResourceString("ServerError"));
                }
            }
        }

        public string NewString
        {
            get
            {
                return ResourceHandler.GetResourceString("New");
            }
        }
        public string RefreshString
        {
            get
            {
                return ResourceHandler.GetResourceString("Refresh");
            }
        }
        public string MyRecordsString
        {
            get
            {
                return ResourceHandler.GetResourceString("MyRecords");
            }
        }
        public string SearchString
        {
            get
            {
                return ResourceHandler.GetResourceString("Search");
            }
        }
        public string DeleteString
        {
            get
            {
                return ResourceHandler.GetResourceString("Delete");
            }
        }
        public string TaskString
        {
            get
            {
                return ResourceHandler.GetResourceString("Task");
            }
        }
        public string UserString
        {
            get
            {
                return ResourceHandler.GetResourceString("User");
            }
        }
        public string DateString
        {
            get
            {
                return ResourceHandler.GetResourceString("Date");
            }
        }
        public string CommentString
        {
            get
            {
                return ResourceHandler.GetResourceString("Comment");
            }
        }
        public string DurationString
        {
            get
            {
                return ResourceHandler.GetResourceString("Duration");
            }
        }
        public string StatusString
        {
            get
            {
                return ResourceHandler.GetResourceString("Status");
            }
        }
    }
}
