using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using TimeSheet.DataAccess;
using TimeSheet.Logic;
using TimeSheet.Model;
using WpfDemo.View;
using WpfDemo.ViewModel.Command;

namespace WpfDemo.ViewModel
{
    public class TaskManagementViewModel : ViewModelBase
    {
        private TaskManagementView _view;


        public ObservableCollection<User> UserList { get; } = new ObservableCollection<User>(); //List?
        public ObservableCollection<TaskViewModel> TaskList { get; } = new ObservableCollection<TaskViewModel>();


        private TaskViewModel _selectedTask;
        public TaskViewModel SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
                OnPropertyChanged(nameof(SelectedTaskVisibility));
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
                return LoginViewModel.LoggedUser.Status == 0 ? "468.7 0 0 0" : "290 0 0 0";
            }
        }

        public Visibility NewTaskButtonVisibility
        {
            get
            {
                return LoginViewModel.LoggedUser.Status == 0 ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility TaskCheckBoxAndTextVisibility
        {
            get
            {
                return LoginViewModel.LoggedUser.Status == 0 ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility SelectedTaskVisibility
        {
            get
            {
                return SelectedTask == null ? Visibility.Hidden : Visibility.Visible;
            }
        }


        public RelayCommand CreateTaskCommand { get; private set; }
        public RelayCommand RefreshTaskListCommand { get; private set; }
        public RelayCommand SortingByCheckBoxCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }
        public RelayCommand HasReadCommand { get; private set; }


        public TaskManagementViewModel(TaskManagementView view)
        {
            _view = view;
            LoadUsers();
            LoadTasks();
            SortingByCheckBox(view);

            CreateTaskCommand = new RelayCommand(CreateTask, CanExecuteShow);
            RefreshTaskListCommand = new RelayCommand(RefreshTaskList, CanExecuteRefresh);
            SortingByCheckBoxCommand = new RelayCommand(SortingByCheckBox, CanExecuteSort);
            DeleteCommand = new RelayCommand(DeleteTask, CanDeleteTask);
            HasReadCommand = new RelayCommand(HasRead, IsTaskClicked);
        }


        private bool CanExecuteShow(object arg)
        {
            return true;
        }
        private void CreateTask(object obj)
        {
            LoadUsers();
            SelectedTask = new TaskViewModel(new Task() { Deadline = DateTime.Today.AddDays(1) });
        }

        private bool CanExecuteRefresh(object arg)
        {
            return true;
        }
        private void RefreshTaskList(object obj)
        {
            LoadTasks();
            SortingByCheckBox(obj);
        }

        public void LoadTasks()
        {
            TaskList.Clear();

            var tasks = new TaskRepository(new TaskLogic()).GetAllTasks();

            tasks.ForEach(task =>
            {
                var taskViewModel = new TaskViewModel(task);
                taskViewModel.User = UserList.First(user => user.IdUser == task.User_idUser);
                TaskList.Add(taskViewModel);
            });
        }

        public void LoadUsers()
        {
            UserList.Clear();

            var users = new UserRepository(new UserLogic()).GetAllUsers();
            users.ForEach(user => UserList.Add(user));
        }


        private bool CanExecuteSort(object arg)
        {
            return true;
        }
        private void SortingByCheckBox(object obj)
        {
            if (String.IsNullOrWhiteSpace(_searchValue))
            {
                if (_view.ShowingMyTasksCheckBox.IsChecked == true && _view.ShowingActiveTasksCheckBox.IsChecked == false)
                {
                    TaskList.Clear();

                    var tasks = new TaskRepository(new TaskLogic()).GetUserTasks(LoginViewModel.LoggedUser.IdUser);

                    tasks.ForEach(task =>
                    {
                        var taskViewModel = new TaskViewModel(task);
                        taskViewModel.User = UserList.First(user => user.IdUser == task.User_idUser);
                        TaskList.Add(taskViewModel);
                    });
                }
                else if (_view.ShowingMyTasksCheckBox.IsChecked == true && _view.ShowingActiveTasksCheckBox.IsChecked == true)
                {
                    TaskList.Clear();

                    var tasks = new TaskRepository(new TaskLogic()).GetAllActiveTasksFromUser(LoginViewModel.LoggedUser.IdUser);

                    tasks.ForEach(task =>
                    {
                        var taskViewModel = new TaskViewModel(task);
                        taskViewModel.User = UserList.First(user => user.IdUser == task.User_idUser);
                        TaskList.Add(taskViewModel);
                    });
                }
                else if (_view.ShowingMyTasksCheckBox.IsChecked == false && _view.ShowingActiveTasksCheckBox.IsChecked == true)
                {
                    TaskList.Clear();

                    var tasks = new TaskRepository(new TaskLogic()).GetAllActiveTasks();

                    tasks.ForEach(task =>
                    {
                        var taskViewModel = new TaskViewModel(task);
                        taskViewModel.User = UserList.First(user => user.IdUser == task.User_idUser);
                        TaskList.Add(taskViewModel);
                    });
                }
                else
                {
                    LoadTasks();
                }
            }
            else
            {
                if (_view.ShowingMyTasksCheckBox.IsChecked == true && _view.ShowingActiveTasksCheckBox.IsChecked == false)
                {
                    TaskList.Clear();

                    var tasks = new TaskRepository(new TaskLogic()).GetUserTasks(LoginViewModel.LoggedUser.IdUser).Where(task => task.Title.Contains(_searchValue) || LoginViewModel.LoggedUser.Username.Contains(_searchValue) || task.Description.Contains(_searchValue) || task.Deadline.ToShortDateString().Contains(_searchValue) || task.Status.ToString().Contains(_searchValue)).ToList();

                    tasks.ForEach(task =>
                    {
                        var taskViewModel = new TaskViewModel(task);
                        taskViewModel.User = UserList.First(user => user.IdUser == task.User_idUser);
                        TaskList.Add(taskViewModel);
                    });
                }
                else if (_view.ShowingMyTasksCheckBox.IsChecked == true && _view.ShowingActiveTasksCheckBox.IsChecked == true)
                {
                    TaskList.Clear();

                    var tasks = new TaskRepository(new TaskLogic()).GetAllActiveTasksFromUser(LoginViewModel.LoggedUser.IdUser).Where(task => task.Title.Contains(_searchValue) || LoginViewModel.LoggedUser.Username.Contains(_searchValue) || task.Description.Contains(_searchValue) || task.Deadline.ToShortDateString().Contains(_searchValue) || task.Status.ToString().Contains(_searchValue)).ToList();

                    tasks.ForEach(task =>
                    {
                        var taskViewModel = new TaskViewModel(task);
                        taskViewModel.User = UserList.First(user => user.IdUser == task.User_idUser);
                        TaskList.Add(taskViewModel);
                    });
                }
                else if (_view.ShowingMyTasksCheckBox.IsChecked == false && _view.ShowingActiveTasksCheckBox.IsChecked == true)
                {
                    TaskList.Clear();

                    var tasks = new TaskRepository(new TaskLogic()).GetAllActiveTasks().Where(task => task.Title.Contains(_searchValue) || task.User_Username.Contains(_searchValue) || task.Description.Contains(_searchValue) || task.Deadline.ToShortDateString().Contains(_searchValue) || task.Status.ToString().Contains(_searchValue)).ToList();

                    tasks.ForEach(task =>
                    {
                        var taskViewModel = new TaskViewModel(task);
                        taskViewModel.User = UserList.First(user => user.IdUser == task.User_idUser);
                        TaskList.Add(taskViewModel);
                    });
                }
                else
                {
                    TaskList.Clear();

                    var tasks = new TaskRepository(new TaskLogic()).GetAllTasks().Where(task => task.Title.Contains(_searchValue) || task.User_Username.Contains(_searchValue) || task.Description.Contains(_searchValue) || task.Deadline.ToShortDateString().Contains(_searchValue) || task.Status.ToString().Contains(_searchValue)).ToList();
                    tasks.ForEach(task =>
                    {
                        var taskViewModel = new TaskViewModel(task);
                        taskViewModel.User = UserList.First(user => user.IdUser == task.User_idUser);
                        TaskList.Add(taskViewModel);
                    });
                }
            }

        }


        private bool CanDeleteTask(object arg)
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
                    
                    LoadTasks();
                    SortingByCheckBox(obj);
                }
                catch (SqlException)
                {
                    MessageBox.Show("Server error!");
                }
            }
        }

        private bool IsTaskClicked(object arg)
        {
            return _selectedTask != null && new NotificationRepository(new NotificationLogic()).GetTaskNotifications(SelectedTask.IdTask) != null;//new NotificationRepository(new NotificationLogic()).GetTaskNotifications(SelectedTask.IdTask).Count != 0;
        }

        private void HasRead(object obj)
        {
            new NotificationRepository(new NotificationLogic()).HasReadNotification(SelectedTask.IdTask);
        }
    }
}
