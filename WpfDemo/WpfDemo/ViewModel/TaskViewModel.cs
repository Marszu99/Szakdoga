using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Windows;
using System.Windows.Input;
using TimeSheet.DataAccess;
using TimeSheet.Logic;
using TimeSheet.Model;
using TimeSheet.Model.Extension;
using TimeSheet.Resource;
using WpfDemo.ViewModel.Command;


namespace WpfDemo.ViewModel
{
    public class TaskViewModel : ViewModelBase, IDataErrorInfo
    {
        private Task _task;
        private User _user;
        private bool _isChanged = false;
        private bool _isTitleChanged = false;
        private bool _isDescriptionChanged = false;
        private bool _isDeadlineChanged = false;
        private bool _isStatusChanged = false;
        public static bool IsNotificationsOn = true;


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
                _isStatusChanged = true;
            }
        }

        public Dictionary<TaskStatus, string> TaskStatuses
        {
            get
            {
                if (_task.IdTask != 0)
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
                return _user;
            }
            set
            {
                _user = value;
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

        public List<User> UserList // Kell h mikor New utan kivalasztunk egy taskot akkor ne legyen ures a User
        {
            get;
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

        public bool IsTaskViewStatusEnabled
        {
            get
            {
                return this._task.IdTask != 0 && _user.Username == LoginViewModel.LoggedUser.Username;
            }
        }


        public string NotificationText
        {
            get
            {
                if (IsNotificationsOn)
                {
                    List<string> notificationList = new List<string>();

                    notificationList = LoginViewModel.LoggedUser.Status == 1 ?
                        new NotificationRepository(new NotificationLogic()).GetTaskNotificationsForAdmin(this._task.IdTask) :
                        new NotificationRepository(new NotificationLogic()).GetTaskNotificationsForEmployee(this._task.IdTask);

                    string notifications = string.Join(Environment.NewLine, notificationList.Select(notification => ResourceHandler.GetResourceString(notification)));

                    return string.IsNullOrWhiteSpace(notifications) ? null : notifications;
                }
                else
                {
                    return null;
                }
            }
        }

        public string ListTasksBackground
        {
            get
            {
                if (IsNotificationsOn)
                {
                    return (LoginViewModel.LoggedUser.Status == 1 && new NotificationRepository(new NotificationLogic()).GetTaskNotificationsForAdmin(this._task.IdTask).Count > 0)
                            || (LoginViewModel.LoggedUser.Status == 0 && new NotificationRepository(new NotificationLogic()).GetTaskNotificationsForEmployee(this._task.IdTask).Count > 0)
                            ? "DarkOrange" : "#eee";
                }
                else
                {
                    return "#eee";
                }
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
                        result = TaskValidationHelper.ValidateUser(this._user);
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
        public RelayCommand CancelTaskViewCommand { get; private set; }


        public TaskViewModel(Task task, List<User> userList)
        {
            _task = task;
            UserList = userList;

            SaveCommand = new RelayCommand(Save, CanSave);
            CancelTaskViewCommand = new RelayCommand(CancelTaskView, CanCancelTaskView);
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
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError);
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
            this._task.IdTask = new TaskRepository(new TaskLogic()).CreateTask(this._task, this._user.IdUser);
            MessageBox.Show(Resources.TaskCreatedMessage, Resources.Information, MessageBoxButton.OK, MessageBoxImage.Information);

            CreateTaskToList(this);


            if (this._user.Status != 1)
            {
                new NotificationRepository(new NotificationLogic()).CreateNotificationForTask("NotificationNewTask", 0, this._task.IdTask);
                SendNotificationEmail(" has been added to your tasks!");
            }
        }
        public event Action<TaskViewModel> TaskCreated;
        public void CreateTaskToList(TaskViewModel taskViewModel)
        {
            TaskCreated?.Invoke(taskViewModel);
        }

        private void UpdateTask()
        {
            new TaskRepository(new TaskLogic()).UpdateTask(this._task, this._task.IdTask, this._task.User_idUser);
            MessageBox.Show(Resources.TaskUpdatedMessage, Resources.Information, MessageBoxButton.OK, MessageBoxImage.Information);
            _isChanged = false;

            if (this._user.Status != 1)
            {
                if (_isTitleChanged && !_isDescriptionChanged && !_isDeadlineChanged)
                {
                    new NotificationRepository(new NotificationLogic()).CreateNotificationForTask("NotificationTaskTitleChanged", 0, this._task.IdTask);
                    SendNotificationEmail(" has been updated! Title has changed!");
                }
                else if (!_isTitleChanged && _isDescriptionChanged && !_isDeadlineChanged)
                {
                    new NotificationRepository(new NotificationLogic()).CreateNotificationForTask("NotificationTaskDescriptionChanged", 0, this._task.IdTask);
                    SendNotificationEmail(" has been updated! Description has changed!");
                }
                else if (!_isTitleChanged && !_isDescriptionChanged && _isDeadlineChanged)
                {
                    new NotificationRepository(new NotificationLogic()).CreateNotificationForTask("NotificationTaskDeadlineChanged", 0, this._task.IdTask);
                    SendNotificationEmail(" has been updated! Deadline has changed!");
                }
                else if (_isTitleChanged && _isDescriptionChanged && !_isDeadlineChanged)
                {
                    new NotificationRepository(new NotificationLogic()).CreateNotificationForTask("NotificationTaskTitleDescriptionChanged", 0, this._task.IdTask);
                    SendNotificationEmail(" has been updated! Title and Description has changed!");
                }
                else if (!_isTitleChanged && _isDescriptionChanged && _isDeadlineChanged)
                {
                    new NotificationRepository(new NotificationLogic()).CreateNotificationForTask("NotificationTaskDescriptionDeadlineChanged", 0, this._task.IdTask);
                    SendNotificationEmail(" has been updated! Description and Deadline has changed!");
                }
                else if (_isTitleChanged && !_isDescriptionChanged && _isDeadlineChanged)
                {
                    new NotificationRepository(new NotificationLogic()).CreateNotificationForTask("NotificationTaskTitleDeadlineChanged", 0, this._task.IdTask);
                    SendNotificationEmail(" has been updated! Title and Deadline has changed!");
                }
                else if (_isTitleChanged && _isDescriptionChanged && _isDeadlineChanged)
                {
                    new NotificationRepository(new NotificationLogic()).CreateNotificationForTask("NotificationTaskTitleDescriptionDeadlineChanged", 0, this._task.IdTask);
                    SendNotificationEmail(" has been updated! Title and Description and Deadline has changed!");
                }
                else if (_isStatusChanged && this._task.Status.ToString() == "InProgress")
                {
                    new NotificationRepository(new NotificationLogic()).CreateNotificationForTask("NotificationTaskInProgress", 1, this._task.IdTask);
                    SendNotificationEmail(" is InProgress!");
                }
                else if (_isStatusChanged && this._task.Status.ToString() == "Done")
                {
                    new NotificationRepository(new NotificationLogic()).CreateNotificationForTask("NotificationTaskDone", 1, this._task.IdTask);
                    SendNotificationEmail(" has been Done!");
                }

                _isTitleChanged = false;
                _isDescriptionChanged = false;
                _isDeadlineChanged = false;
            }
        }


        public event Action<object> TaskCanceled;
        public void CancelTask(Object obj)
        {
            TaskCanceled?.Invoke(obj);
        }
        private bool CanCancelTaskView(object arg)
        {
            return true;
        }

        private void CancelTaskView(object obj)
        {
            CancelTask(obj);
        }


        private void SendNotificationEmail(string EmailNotificationMessage)
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
            string EmailMessage = this._task.Title + EmailNotificationMessage;
            MailMessage mm = new MailMessage("wpfszakdoga@gmail.com", this._user.Email, EmailSubject, EmailMessage);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            client.Send(mm);
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
        public string SaveString
        {
            get
            {
                return Resources.Save;
            }
        }
        public string CancelString
        {
            get
            {
                return Resources.Cancel;
            }
        }
    }
}