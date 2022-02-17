﻿using System;
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
    public class TaskManagementViewModel : ViewModelBase
    {
        private TaskManagementView _view;
        public static bool asd = false;


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
                if (TabcontrolViewModel.IsLanguageEnglish)
                {
                    return LoginViewModel.LoggedUser.Status == 0 ? "331.5 0 0 0" : "152.5 0 0 0";//"468.7 0 0 0" : "290 0 0 0"
                }
                else
                {
                    return LoginViewModel.LoggedUser.Status == 0 ? "316 0 0 0" : "122 0 0 0";//"438 0 0 0" : "244 0 0 0"
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
                return SelectedTask == null || asd == true ? Visibility.Hidden : Visibility.Visible;
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
        public RelayCommand RefreshTaskListCommand { get; private set; }
        public RelayCommand SortingByCheckBoxCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }
        public RelayCommand HasReadCommand { get; private set; }
        public RelayCommand NotificationsSwitchOnOffCommand { get; private set; }



        public TaskManagementViewModel(TaskManagementView view)
        {
            _view = view;
            LoadUsers();
            RefreshTaskList(view);

            CreateTaskCommand = new RelayCommand(CreateTask, CanExecuteShow);
            RefreshTaskListCommand = new RelayCommand(RefreshTaskList, CanExecuteRefresh);
            SortingByCheckBoxCommand = new RelayCommand(SortingByCheckBox, CanExecuteSort);
            DeleteCommand = new RelayCommand(DeleteTask, CanDeleteTask);
            HasReadCommand = new RelayCommand(HasRead, CanExecuteReadTaskNotifications);
            NotificationsSwitchOnOffCommand = new RelayCommand(NotificationSwitchOnOff, CanExecuteSwitch);
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

                    taskViewModel.TaskCreated += OnTaskCreated;
                });
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError);
            }
            
        }
        private void OnTaskCreated(Task task)
        {
            TaskList.Add(new TaskViewModel(task));
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
                MessageBox.Show(Resources.ServerError);
            }
        }


        private bool CanDeleteTask(object arg)
        {
            return _selectedTask != null && LoginViewModel.LoggedUser.Status != 0;
        }

        private void DeleteTask(object obj)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show(Resources.TaskDeleteQuestion1 + SelectedTask.Title + Resources.TaskDeleteQuestion2, Resources.Warning, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    new TaskRepository(new TaskLogic()).DeleteTask(SelectedTask.IdTask);
                    MessageBox.Show(Resources.TaskDeletedMessage, Resources.Information, MessageBoxButton.OK, MessageBoxImage.Information);

                    if(this.SelectedTask.User_Username != LoginViewModel.LoggedUser.Username)
                    {
                        SendNotificationEmail(SelectedTask.Title);
                    }
                    RefreshTaskList(obj);
                }
                catch (SqlException)
                {
                    MessageBox.Show(Resources.ServerError);
                }
            }
        }

        private bool CanExecuteReadTaskNotifications(object arg)
        {
            if (TaskViewModel.IsNotificationsOn)
            {
                if (LoginViewModel.LoggedUser.Status == 1)
                {
                    return _selectedTask != null && new NotificationRepository(new NotificationLogic()).GetTaskNotificationsForAdmin(SelectedTask.IdTask) != null;
                }
                else
                {
                    return _selectedTask != null && new NotificationRepository(new NotificationLogic()).GetTaskNotificationsForEmployee(SelectedTask.IdTask) != null;
                }
            }
            else
            {
                return false;
            }
        }

        private void HasRead(object obj)
        {
            try
            {
                new NotificationRepository(new NotificationLogic()).HasReadNotification(SelectedTask.IdTask, LoginViewModel.LoggedUser.Status);

                RefreshTaskList(obj);
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError);
            }
        }

        private bool CanExecuteSwitch(object arg)
        {
            return true;
        }
        private void NotificationSwitchOnOff(object obj)
        {
            if (_view.NotificationsCheckBox.IsChecked == true)
            {
                TaskViewModel.IsNotificationsOn = true;
                RefreshTaskList(obj);
            }
            else
            {
                TaskViewModel.IsNotificationsOn = false;
                RefreshTaskList(obj);
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
            MailMessage mm = new MailMessage("wpfszakdoga@gmail.com", SelectedTask.User.Email, EmailSubject, EmailMessage);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            client.Send(mm);
        }

        public string NewString
        {
            get
            {
                return Resources.New;
            }
        }
        public string RefreshString
        {
            get
            {
                return Resources.Refresh;
            }
        }
        public string MyTasksString
        {
            get
            {
                return Resources.MyTasks;
            }
        }
        public string ActiveTasksString
        {
            get
            {
                return Resources.ActiveTasks;
            }
        }
        public string NotificationsString
        {
            get
            {
                return Resources.Notifications;
            }
        }
        public string SearchString
        {
            get
            {
                return Resources.Search;
            }
        }
        public string DeleteString
        {
            get
            {
                return Resources.Delete;
            }
        }
        public string TitleString
        {
            get
            {
                return Resources.Title;
            }
        }
        public string UserString
        {
            get
            {
                return Resources.User;
            }
        }
        public string DescriptionString
        {
            get
            {
                return Resources.Description;
            }
        }
        public string DeadlineString
        {
            get
            {
                return Resources.Deadline;
            }
        }
        public string StatusString
        {
            get
            {
                return Resources.Status;
            }
        }
    }
}
