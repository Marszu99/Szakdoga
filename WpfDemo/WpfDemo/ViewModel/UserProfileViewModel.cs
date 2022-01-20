using System;
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
    public class UserProfileViewModel : ViewModelBase
    {
        private User _user;
        private Task _selectedTask;

        private ObservableCollection<Record> _recordList = new ObservableCollection<Record>();
        private ObservableCollection<Task> _taskList = new ObservableCollection<Task>();

        public ObservableCollection<Record> RecordList
        {
            get
            {
                return _recordList;
            }
        }
        public ObservableCollection<Task> TaskList
        {
            get
            {
                return _taskList;
            }
        }

        /*private UserProfileTaskViewModel _selectedTask;
        public UserProfileTaskViewModel SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
            }
        }*/

        public User CurrentUser
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }

        private int _currentUserIdUser;
        public int CuttentUserIdUser
        {
            get
            {
                return _currentUserIdUser;
            }
            set
            {
                _currentUserIdUser = value;
                OnPropertyChanged(nameof(CuttentUserIdUser));
            }
        }


        public Task SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
            }
        }

        private string _searchTaskListValue;
        public string SearchTaskListValue
        {
            get { return _searchTaskListValue; }
            set
            {
                _searchTaskListValue = value;
                OnPropertyChanged(nameof(SearchTaskListValue));
                if (String.IsNullOrWhiteSpace(_searchTaskListValue))
                {
                    LoadTasks(CuttentUserIdUser);
                }
                else
                {
                    _taskList.Clear();

                    try
                    {
                        var tasks = new TaskRepository(new TaskLogic()).GetUserTasks(CuttentUserIdUser).Where(task => task.Title.Contains(_searchTaskListValue) || task.Description.Contains(_searchTaskListValue) || task.Deadline.ToShortDateString().Contains(_searchTaskListValue) || task.Status.ToString().Contains(_searchTaskListValue)).ToList();
                        tasks.ForEach(task => _taskList.Add(task));
                    }
                    catch (SqlException)
                    {
                        MessageBox.Show(ResourceHandler.GetResourceString("ServerError"));
                    }

                }
            }
        }

        private string _searchRecordListValue;
        public string SearchRecordListValue
        {
            get { return _searchRecordListValue; }
            set
            {
                _searchRecordListValue = value;
                OnPropertyChanged(nameof(SearchRecordListValue));
                if (String.IsNullOrWhiteSpace(_searchRecordListValue))
                {
                    LoadRecords(CuttentUserIdUser);
                }
                else
                {
                    _recordList.Clear();

                    try
                    {
                        var records = new RecordRepository(new RecordLogic()).GetUserRecords(CuttentUserIdUser).Where(record => record.Task_Title.Contains(_searchRecordListValue) || record.User_Username.Contains(_searchRecordListValue) || record.Date.ToShortDateString().Contains(_searchRecordListValue) || record.Comment.Contains(_searchRecordListValue) || record.Duration.ToString().Contains(_searchRecordListValue) || record.Task_Status.ToString().Contains(_searchRecordListValue)).ToList();
                        records.ForEach(record => _recordList.Add(record));
                    }
                    catch (SqlException)
                    {
                        MessageBox.Show(ResourceHandler.GetResourceString("ServerError"));
                    }

                }
            }
        }

        public Visibility AddTaskButtonVisibility
        {
            get
            {
                return LoginViewModel.LoggedUser.Status == 0 ? Visibility.Hidden : Visibility.Visible;
            }
        }


        public RelayCommand DeleteCommand { get; private set; }
        public RelayCommand ShowTaskCommand { get; private set; } // nem joooo!!!
        public RelayCommand ShowAddTaskCommand { get; private set; }
        public RelayCommand ShowUpdateTaskCommand { get; private set; }

        public UserProfileViewModel(int userid)
        {
            _currentUserIdUser = userid;
            LoadTasks(userid);
            LoadRecords(userid); 
            
            DeleteCommand = new RelayCommand(DeleteTask, CanDeleteTask);
            ShowTaskCommand = new RelayCommand(ShowTask, CanShowTask);
            ShowAddTaskCommand = new RelayCommand(ShowAddTask, CanShowAddTask);
            ShowUpdateTaskCommand = new RelayCommand(ShowUpdateTask, CanShowUpdateTask);
        }

        private bool CanShowTask(object arg)
        {
            return true;
        }

        private void ShowTask(object obj) // nem joooo!!!
        {
            if(_selectedTask == null)
            {
                UserProfileTaskView Ipagee = new UserProfileTaskView();
                //SelectedTask = new UserProfileTaskViewModel(new Task() { Deadline = DateTime.Today.AddDays(1) });
                (Ipagee.DataContext as UserProfileTaskViewModel).Task.User_idUser = CurrentUser.IdUser;
                (Ipagee.DataContext as UserProfileTaskViewModel).Task.User_Username = CurrentUser.Username;
                Ipagee.ShowDialog();
            }
            else
            {
                UserProfileTaskView Ipagee = new UserProfileTaskView();
                //SelectedTask = new UserProfileTaskViewModel(new Task() { Deadline = DateTime.Today.AddDays(1) });
                //(Ipage.DataContext as UserProfileTaskViewModel).CurrentTask = SelectedTask;
                (Ipagee.DataContext as UserProfileTaskViewModel).Task.Title = SelectedTask.Title;
                (Ipagee.DataContext as UserProfileTaskViewModel).Description = SelectedTask.Description;
                (Ipagee.DataContext as UserProfileTaskViewModel).Deadline = SelectedTask.Deadline;
                (Ipagee.DataContext as UserProfileTaskViewModel).Status = SelectedTask.Status;
                (Ipagee.DataContext as UserProfileTaskViewModel).User_idUser = CurrentUser.IdUser;
                (Ipagee.DataContext as UserProfileTaskViewModel).User_Username = CurrentUser.Username;

                Ipagee.ShowDialog();
            }

            LoadTasks(CurrentUser.IdUser);
        }

        private bool CanDeleteTask(object arg)
        {
            return _selectedTask != null && LoginViewModel.LoggedUser.Status != 0;
        }
        private bool CanShowAddTask(object arg)
        {
            return true;
        }
        private bool CanShowUpdateTask(object arg)
        {
            return _selectedTask != null && LoginViewModel.LoggedUser.Status != 0;
        }

        private void DeleteTask(object obj)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show(ResourceHandler.GetResourceString("TaskDeleteQuestion1") + SelectedTask.Title + ResourceHandler.GetResourceString("TaskDeleteQuestion2"), ResourceHandler.GetResourceString("Warning"), System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    new TaskRepository(new TaskLogic()).DeleteTask(SelectedTask.IdTask);
                    MessageBox.Show(ResourceHandler.GetResourceString("TaskDeletedMessage"), ResourceHandler.GetResourceString("Information"), MessageBoxButton.OK, MessageBoxImage.Information);

                    LoadTasks(SelectedTask.User_idUser);
                }
                catch (SqlException)
                {
                    MessageBox.Show(ResourceHandler.GetResourceString("ServerError"));
                }
            }
        }

        private void ShowAddTask(object obj)
        {
            AddTaskToUser Ipage = new AddTaskToUser();
            //(Ipage.DataContext as AddTaskToUserViewModel).CurrentUser = (UserTasksDataGrid.SelectedItem as User);
            (Ipage.DataContext as AddTaskToUserViewModel).User_idUser = CurrentUser.IdUser;
            (Ipage.DataContext as AddTaskToUserViewModel).User_Username = CurrentUser.Username;
            Ipage.ShowDialog();
            LoadTasks(CurrentUser.IdUser);
        }

        private void ShowUpdateTask(object obj)
        {
            UpdateTask Ipage = new UpdateTask();
            (Ipage.DataContext as UpdateTaskViewModel).CurrentTask = SelectedTask;
            (Ipage.DataContext as UpdateTaskViewModel).CurrentTask.User_idUser = CurrentUser.IdUser;
            (Ipage.DataContext as UpdateTaskViewModel).CurrentTask.User_Username = CurrentUser.Username;
            Ipage.ShowDialog();
            LoadTasks(CurrentUser.IdUser);
        }


        void LoadRecords(int userid)
        {
            _recordList.Clear();

            var records = new RecordRepository(new RecordLogic()).GetUserRecords(userid);//CurrentUser.IdUser
            records.ForEach(record => _recordList.Add(record));  //TimeSpan.FromMinutes(_record.Duration).ToString("hh':'mm");
        }
        void LoadTasks(int userid)
        {
            _taskList.Clear();

            var tasks = new TaskRepository(new TaskLogic()).GetUserTasks(userid);
            tasks.ForEach(task => _taskList.Add(task));
        }


        public string UsernameString
        {
            get
            {
                return ResourceHandler.GetResourceString("Username");
            }
        }
        public string FirstNameString
        {
            get
            {
                return ResourceHandler.GetResourceString("FirstName");
            }
        }
        public string LastNameString
        {
            get
            {
                return ResourceHandler.GetResourceString("LastName");
            }
        }
        public string EmailString
        {
            get
            {
                return ResourceHandler.GetResourceString("Email");
            }
        }
        public string TelephoneString
        {
            get
            {
                return ResourceHandler.GetResourceString("Telephone");
            }
        }
        public string TasksString
        {
            get
            {
                return ResourceHandler.GetResourceString("Tasks");
            }
        }
        public string TitleString
        {
            get
            {
                return ResourceHandler.GetResourceString("Title");
            }
        }
        public string DescriptionString
        {
            get
            {
                return ResourceHandler.GetResourceString("Description");
            }
        }
        public string DeadlineString
        {
            get
            {
                return ResourceHandler.GetResourceString("Deadline");
            }
        }
        public string StatusString
        {
            get
            {
                return ResourceHandler.GetResourceString("Status");
            }
        }
        public string RecordsString
        {
            get
            {
                return ResourceHandler.GetResourceString("Records");
            }
        }
        public string TaskString
        {
            get
            {
                return ResourceHandler.GetResourceString("Tasks");
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
        public string SearchString
        {
            get
            {
                return ResourceHandler.GetResourceString("Search");
            }
        }
    }
}
