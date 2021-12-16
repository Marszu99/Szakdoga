using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;
using TimeSheet.DataAccess;
using TimeSheet.Logic;
using TimeSheet.Model;
using WpfDemo.View;
using WpfDemo.ViewModel.Command;

namespace WpfDemo.ViewModel
{
    public class UserProfileViewModel : ViewModelBase
    {
        private User _user;
        private Task _selectedTask;

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


        public Task SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
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
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure you want to delete " + SelectedTask.Title + " task?", "Warning!", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    new TaskRepository(new TaskLogic()).DeleteTask(SelectedTask.IdTask);
                    MessageBox.Show("Task has been deleted succesfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                    LoadTasks(SelectedTask.User_idUser);
                }
                catch (SqlException)
                {
                    MessageBox.Show("Server error!");
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

        void LoadRecords(int userid)
        {
            _recordList.Clear();

            var records = new RecordRepository(new RecordLogic()).GetUserRecords(userid);//CurrentUser.IdUser
            records.ForEach(record => _recordList.Add(record));  //TimeSpan.FromMinutes(_record.Duration).ToString("hh':'mm");
        }
        void LoadTasks(int userid)
        {
            _taskList.Clear();

            var tasks = new TaskRepository(new TaskLogic()).GetUserTasks(userid); //CurrentUser.IdUser
            tasks.ForEach(task => _taskList.Add(task));
        }
    }
}
