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
                if (ResourceHandler.isEnglish)
                {
                    return LoginViewModel.LoggedUser.Status == 0 ? "468.7 0 0 0" : "290 0 0 0";
                }
                else
                {
                    return LoginViewModel.LoggedUser.Status == 0 ? "438 0 0 0" : "243 0 0 0";
                }
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

        public Visibility ListTasksViewUserVisibility
        {
            get
            {
                return LoginViewModel.LoggedUser.Status == 0 ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility ListTasksViewContextMenuVisibility // Delete Header Visibility
        {
            get
            {
                return LoginViewModel.LoggedUser.Status == 0 ? Visibility.Collapsed : Visibility.Visible;
            }
        }


        public RelayCommand CreateTaskCommand { get; private set; }
        public static RelayCommand RefreshTaskListCommand { get; private set; }
        public RelayCommand SortingByCheckBoxCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }
        public RelayCommand HasReadCommand { get; private set; }


        public TaskManagementViewModel(TaskManagementView view)
        {
            _view = view;
            LoadUsers();
            RefreshTaskList(view);

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
            RefreshTaskList(obj);
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

            try
            {
                var tasks = new TaskRepository(new TaskLogic()).GetAllTasks();

                tasks.ForEach(task =>
                {
                    var taskViewModel = new TaskViewModel(task);
                    taskViewModel.User = UserList.First(user => user.IdUser == task.User_idUser);
                    TaskList.Add(taskViewModel);
                });
            }
            catch (SqlException)
            {
                MessageBox.Show(ResourceHandler.GetResourceString("ServerError"));
            }
            
        }

        public void LoadUsers()
        {
            UserList.Clear();

            try
            {
                var users = new UserRepository(new UserLogic()).GetAllUsers();
                users.ForEach(user => UserList.Add(user));
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

                        var tasks = new TaskRepository(new TaskLogic()).GetUserTasks(LoginViewModel.LoggedUser.IdUser).Where(task => task.Title.Contains(_searchValue) || task.Description.Contains(_searchValue) || task.Deadline.ToShortDateString().Contains(_searchValue) || task.Status.ToString().Contains(_searchValue)).ToList();

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

                        var tasks = new TaskRepository(new TaskLogic()).GetAllActiveTasksFromUser(LoginViewModel.LoggedUser.IdUser).Where(task => task.Title.Contains(_searchValue) || task.Description.Contains(_searchValue) || task.Deadline.ToShortDateString().Contains(_searchValue) || task.Status.ToString().Contains(_searchValue)).ToList();

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
            catch (SqlException)
            {
                MessageBox.Show(ResourceHandler.GetResourceString("ServerError"));
            }
        }


        private bool CanDeleteTask(object arg)
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

                    RefreshTaskList(obj);
                }
                catch (SqlException)
                {
                    MessageBox.Show(ResourceHandler.GetResourceString("ServerError"));
                }
            }
        }

        private bool IsTaskClicked(object arg)
        {
            return _selectedTask != null && new NotificationRepository(new NotificationLogic()).GetTaskNotifications(SelectedTask.IdTask) != null;//new NotificationRepository(new NotificationLogic()).GetTaskNotifications(SelectedTask.IdTask).Count != 0;
        }

        private void HasRead(object obj)
        {
            try
            {
                new NotificationRepository(new NotificationLogic()).HasReadNotification(SelectedTask.IdTask);

                RefreshTaskList(obj);
            }
            catch (SqlException)
            {
                MessageBox.Show(ResourceHandler.GetResourceString("ServerError"));
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
        public string MyTasksString
        {
            get
            {
                return ResourceHandler.GetResourceString("MyTasks");
            }
        }
        public string ActiveTasksString
        {
            get
            {
                return ResourceHandler.GetResourceString("ActiveTasks");
            }
        }
        public string SearchString
        {
            get
            {
                return ResourceHandler.GetResourceString("Search");
            }
        }
        public string TitleString
        {
            get
            {
                return ResourceHandler.GetResourceString("Title");
            }
        }
        public string UserString
        {
            get
            {
                return ResourceHandler.GetResourceString("User");
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
    }
}
