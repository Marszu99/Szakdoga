using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using TimeSheet.DataAccess;
using TimeSheet.Logic;
using TimeSheet.Model;
using TimeSheet.Model.Extension;
using TimeSheet.Resource;
using WpfDemo.View;
using WpfDemo.ViewModel.Command;


namespace WpfDemo.ViewModel
{
    public class TaskViewModel : ViewModelBase, IDataErrorInfo
    {
        private Task _task;
        //private TaskView _view;
        private bool _isChanged = false;
        private bool _isTitleChanged = false;
        private bool _isDescriptionChanged = false;
        private bool _isDeadlineChanged = false;


        public Task Task
        {
            get
            {
                return _task;
            }
        }

        public int IdTask
        {
            get
            {
                return _task.IdTask;
            }
            set
            {
                _task.IdTask = value;
                OnPropertyChanged(nameof(IdTask));
            }
        }

        public string Title
        {
            get
            {
                return _task.Title;
            }
            set
            {
                _task.Title = value;
                OnPropertyChanged(nameof(Title));
                _isChanged = true;
                _isTitleChanged = true;
            }
        }
        public string Description
        {
            get
            {
                return _task.Description;
            }
            set
            {
                _task.Description = value;
                OnPropertyChanged(nameof(Description));
                _isChanged = true;
                _isDescriptionChanged = true;
            }
        }

        public DateTime Deadline
        {
            get
            {
                return _task.Deadline;
            }
            set
            {
                _task.Deadline = value;
                OnPropertyChanged(nameof(Deadline));
                _isChanged = true;
                _isDeadlineChanged = true;
            }
        }

        public TaskStatus Status
        {
            get
            {
                return _task.Status;
            }
            set
            {
                _task.Status = value;
                OnPropertyChanged(nameof(Status));
                _isChanged = true;
            }
        }

        public Dictionary<TaskStatus, string> TaskStatuses
        {
            get
            {
                if(_task.IdTask != 0)
                {
                    return Enum.GetValues(typeof(TaskStatus)).Cast<TaskStatus>()
                    .ToDictionary<TaskStatus, TaskStatus, string>(
                    item => item,
                    item => item.ToString());
                }
                else
                {
                    return Enum.GetValues(typeof(TaskStatus)).Cast<TaskStatus>()
                    .Where(item => item == TaskStatus.Created)
                    .ToDictionary<TaskStatus, TaskStatus, string>(
                    item => item,
                    item => item.ToString());
                }     
            }
        }

        public User User
        {
            get
            {
                return _task.User;
            }
            set
            {
                _task.User = value;
                OnPropertyChanged(nameof(User));
                if (_task.IdTask == 0) // kell ez mert kulonbon update eseten a Save gomb nem lennne disabled-d
                {
                    _isChanged = true;
                }
            }
        }

        public int User_idUser
        {
            get
            {
                return _task.User_idUser;
            }
            set
            {
                _task.User_idUser = value;
                OnPropertyChanged(nameof(User_idUser));
            }
        }

        public string User_Username
        {
            get
            {
                return _task.User_Username;
            }
            set
            {
                _task.User_Username = value;
                OnPropertyChanged(nameof(User_Username));
            }
        }

        public DateTime CreationDate
        {
            get
            {
                return _task.CreationDate;
            }
            set
            {
                _task.CreationDate = value;
                OnPropertyChanged(nameof(CreationDate));
            }
        }

        public bool IsUserEnabled // ez igy kene TaskViewUserRowHeight mellett?
        {
            get
            {
                return _task.IdTask == 0;
            }
        }

        public string TaskViewUserRowHeight
        {
            get
            {
                return LoginViewModel.LoggedUser.Status == 1 ? "0.8*" : "0";
            }
        }

        public bool IsTaskViewValuesReadOnly
        {
            get
            { 
                return LoginViewModel.LoggedUser.Status == 0;
            }
        }

        public bool IsTaskViewDeadlineHitTestVisible
        {
            get
            {
                return LoginViewModel.LoggedUser.Status == 1;
            }
        }


        //private string _notificationText;
        public string NotificationText
        {
            get
            {
                return _task.User_Username == LoginViewModel.LoggedUser.Username ? new NotificationRepository(new NotificationLogic()).GetTaskNotifications(this._task.IdTask) : null;
            }
            /*set
            {
                _notificationText = value;
                OnPropertyChanged(NotificationText);
            }*/
        }

        public string ListTasksBackground
        {
            get
            {
                return _task.User_Username == LoginViewModel.LoggedUser.Username && new NotificationRepository(new NotificationLogic()).GetTaskNotifications(this._task.IdTask) != null ? "DarkOrange" : "#eee";
            }
        }


        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();
        public string Error { get { return null; } }

        public string this[string propertyName]
        {
            get
            {
                string result = null;

                switch (propertyName)
                {
                    case nameof(User):
                        result = TaskValidationHelper.ValidateUser(_task.User);
                        break;

                    case nameof(Title):
                        result = TaskValidationHelper.ValidateTitle(_task.Title);
                        break;

                    case nameof(Deadline):
                        result = TaskValidationHelper.ValidateDeadline(_task.Deadline);
                        break;

                    case nameof(Status):
                        result = TaskValidationHelper.ValidateStatus(_task.Status, _task.IdTask);
                        break;

                    default: // ez kell???
                        break;
                }

                if (ErrorCollection.ContainsKey(propertyName))
                {
                    ErrorCollection[propertyName] = result;
                }
                else if (result != null)
                {
                    ErrorCollection.Add(propertyName, result);
                }
                OnPropertyChanged("ErrorCollection");

                return result;
            }
        }


        public ICommand SaveCommand { get; }

        public TaskViewModel(Task task)
        {
            _task = task;
            //_view = view;

            SaveCommand = new RelayCommand(Save, CanSave);
        }

        private bool CanSave(object arg)
        {
            return User != null && !string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Deadline.ToString()) && _isChanged;
        }

        private void Save(object obj)
        {
            try
            {
                if (CheckIfNewTask())
                {
                    CreateTask();
                }
                else
                {
                    UpdateTask();
                }
                TaskManagementViewModel.RefreshTaskListCommand.Execute(this); // ez igy jo????
            }
            catch (SqlException)
            {
                MessageBox.Show(ResourceHandler.GetResourceString("ServerError"));
            }
            catch (TaskValidationException)
            {

            }
        }


        private bool CheckIfNewTask()
        {
            return this._task.IdTask == 0;
        }


        private void CreateTask()
        {
            /*if (this._view.OneTimeTask.IsChecked == true)
            {
                //this._task.IdTask = new TaskRepository(new TaskLogic()).CreateTask(this._task, this._task.User.IdUser);
                MessageBox.Show(ResourceHandler.GetResourceString("OneTime"));
            }
            else if (this._view.DailyTask.IsChecked == true)
            {
                //TaskService ts = new TaskService();
                MessageBox.Show(ResourceHandler.GetResourceString("Daily"));
            }
            else if (this._view.WeeklyTask.IsChecked == true)
            {
                MessageBox.Show(ResourceHandler.GetResourceString("Weekly"));
            }
            else if (this._view.MonthlyTask.IsChecked == true)
            {
                MessageBox.Show(ResourceHandler.GetResourceString("Monthly"));
            }*/


            this._task.IdTask = new TaskRepository(new TaskLogic()).CreateTask(this._task, this._task.User.IdUser);
            MessageBox.Show(ResourceHandler.GetResourceString("TaskCreatedMessage"), ResourceHandler.GetResourceString("Information"), MessageBoxButton.OK, MessageBoxImage.Information);

            if (this._task.User.Status != 1)
            {
                new NotificationRepository(new NotificationLogic()).CreateNotificationForTask("New task!", this._task.IdTask);
            }
            RefreshValues();
        }

        public class EnumMatchToBooleanConverter : IValueConverter
        {
            public object Convert(object value, Type targetType,
                                  object parameter, CultureInfo culture)
            {
                if (value == null || parameter == null)
                    return false;

                string checkValue = value.ToString();
                string targetValue = parameter.ToString();
                return checkValue.Equals(targetValue,
                         StringComparison.InvariantCultureIgnoreCase);
            }

            public object ConvertBack(object value, Type targetType,
                                      object parameter, CultureInfo culture)
            {
                if (value == null || parameter == null)
                    return null;

                bool useValue = (bool)value;
                string targetValue = parameter.ToString();
                if (useValue)
                    return Enum.Parse(targetType, targetValue);

                return null;
            }
        }

        private void UpdateTask()
        {
            new TaskRepository(new TaskLogic()).UpdateTask(this._task, this._task.IdTask, this._task.User_idUser);
            MessageBox.Show(ResourceHandler.GetResourceString("TaskUpdatedMessage"), ResourceHandler.GetResourceString("Information"), MessageBoxButton.OK, MessageBoxImage.Information);
            _isChanged = false;

            if (this._task.User.Status != 1)
            {
                if (_isTitleChanged && !_isDescriptionChanged && !_isDeadlineChanged)
                {
                    new NotificationRepository(new NotificationLogic()).CreateNotificationForTask("Task has been updated! Title has changed!", this._task.IdTask);
                }
                else if (!_isTitleChanged && _isDescriptionChanged && !_isDeadlineChanged)
                {
                    new NotificationRepository(new NotificationLogic()).CreateNotificationForTask("Task has been updated! Description has changed!", this._task.IdTask);
                }
                else if (!_isTitleChanged && !_isDescriptionChanged && _isDeadlineChanged)
                {
                    new NotificationRepository(new NotificationLogic()).CreateNotificationForTask("Task has been updated! Deadline has changed!", this._task.IdTask);
                }
                else if (_isTitleChanged && _isDescriptionChanged && !_isDeadlineChanged)
                {
                    new NotificationRepository(new NotificationLogic()).CreateNotificationForTask("Task has been updated! Title and Description has changed!", this._task.IdTask);
                }
                else if (!_isTitleChanged && _isDescriptionChanged && _isDeadlineChanged)
                {
                    new NotificationRepository(new NotificationLogic()).CreateNotificationForTask("Task has been updated! Description and Deadline has changed!", this._task.IdTask);
                }
                else if (_isTitleChanged && !_isDescriptionChanged && _isDeadlineChanged)
                {
                    new NotificationRepository(new NotificationLogic()).CreateNotificationForTask("Task has been updated! Title and Deadline has changed!", this._task.IdTask);
                }
                else if (_isTitleChanged && _isDescriptionChanged && _isDeadlineChanged)
                {
                    new NotificationRepository(new NotificationLogic()).CreateNotificationForTask("Task has been updated! Title and Description and Deadline has changed!", this._task.IdTask);
                }
                _isTitleChanged = false;
                _isDescriptionChanged = false;
                _isDeadlineChanged = false;
            }
        }


        private void RefreshValues()
        {
            this.IdTask = 0;
            this.User = null;
            this.Title = "";
            this.Description = "";
            this.Deadline = DateTime.Today.AddDays(1);
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
        public string SaveString
        {
            get
            {
                return ResourceHandler.GetResourceString("Save");
            }
        }
    }
}
