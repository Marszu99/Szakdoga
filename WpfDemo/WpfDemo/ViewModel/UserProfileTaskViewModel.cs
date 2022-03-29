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
using WpfDemo.View;
using WpfDemo.ViewModel.Command;

namespace WpfDemo.ViewModel
{
    public class UserProfileTaskViewModel : ViewModelBase, IDataErrorInfo
    {
        private Task _task;
        private User _currentUser;
        private UserProfileTaskView _view;
        private bool _isTitleChanged = false;
        private bool _isDescriptionChanged = false;
        private bool _isDeadlineChanged = false;
        private bool _isStatusChanged = false;

        public Task CurrentTask
        {
            get
            {
                return _task;
            }
            set
            {
                _task = value;
                OnPropertyChanged(nameof(CurrentTask));
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

        public string Title // Cim bindolashoz
        {
            get
            {
                return _task.Title;
            }
            set
            {
                _task.Title = value;
                OnPropertyChanged(nameof(Title));
                _isTitleChanged = true;
                OnPropertyChanged(nameof(TitleErrorIconVisibility));
            }
        }
        public string Description // Leiras bindolashoz
        {
            get
            {
                return _task.Description;
            }
            set
            {
                _task.Description = value;
                OnPropertyChanged(nameof(Description));
                _isDescriptionChanged = true;
            }
        }

        public DateTime Deadline // Hatarido bindolashoz
        {
            get
            {
                return _task.Deadline;
            }
            set
            {
                _task.Deadline = value;
                OnPropertyChanged(nameof(Deadline));
                _isDeadlineChanged = true;
                OnPropertyChanged(nameof(DeadlineErrorIconVisibility));
            }
        }

        public TaskStatus Status // Statusz bindolashoz
        {
            get
            {
                return _task.Status;
            }
            set
            {
                _task.Status = value;
                OnPropertyChanged(nameof(Status));
                _isStatusChanged = true;
                OnPropertyChanged(nameof(StatusErrorIconVisibility));
            }
        }
        public string TaskStatusString // kell h a listaban valtozzon nyelvvaltas eseten a kiiras
        {
            get 
            { 
                return ResourceHandler.GetResourceString(_task.Status.ToString());
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
                    item => ResourceHandler.GetResourceString(item.ToString())); // item => item.ToString());
                }
                else
                {
                    return Enum.GetValues(typeof(TaskStatus)).Cast<TaskStatus>()
                    .Where(item => item == TaskStatus.Created)
                    .ToDictionary<TaskStatus, TaskStatus, string>(
                    item => item,
                    item => ResourceHandler.GetResourceString(item.ToString())); // item => item.ToString());
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

        public User CurrentUser
        {
            get
            {
                try
                {
                    _currentUser = new UserRepository(new UserLogic()).GetUserByID(User_idUser);
                }
                catch (SqlException)
                {
                    MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                return _currentUser;
            }
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }

        public DateTime CreationDate // letrehozasanak a rogzitese kell h a Rogzites datumahoz tudjunk Exception irni(h ne allitsa korabbra a CreationDate-nel)
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

        public bool IsUserProfileTaskViewStatusEnabled // Ha az Admin a sajatjat modositja akkor annak a Statuszat tudja modositani ellenkezo esetben pedig egy TextBoxkent jelenik meg
        {
            get
            {
                return CurrentUser.Username == LoginViewModel.LoggedUser.Username && _task.IdTask != 0;
            }
        }

        public Visibility UserProfileTaskViewStatusVisibility // Ha az Admin a sajatjat nezi akkor Stausz szoveg lathato
        {
            get
            {
                return CurrentUser.Username == LoginViewModel.LoggedUser.Username && _task.IdTask != 0  ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public Visibility UserProfileTaskViewStatusWithColonVisibility // Ha az Admin nem a sajatjat nezi akkor Stausz: szoveg lathato
        {
            get
            {
                return CurrentUser.Username != LoginViewModel.LoggedUser.Username || _task.IdTask == 0  ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public Visibility UserProfileTaskViewStatusComboboxVisibility // Ha az Admin a sajatjat modositja akkor annak a Statusza egy ComboBoxkent jelenik meg
        {
            get
            {
                return CurrentUser.Username == LoginViewModel.LoggedUser.Username && _task.IdTask != 0 ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility UserProfileTaskViewStatusTextBoxVisibility // Ha az Admin nem a sajatjat modositja akkor annak a Statusza egy TextBoxkent jelenik meg(illetve uj letrehozasa eseten is)
        {
            get
            {
                return CurrentUser.Username != LoginViewModel.LoggedUser.Username || _task.IdTask == 0 ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility TitleErrorIconVisibility // Ha a Cim Exceptiont kap akkor az ErrorIcon megjelenik
        {
            get
            {
                return TaskValidationHelper.ValidateTitle(_task.Title) == null || !_isTitleChanged ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public Visibility DeadlineErrorIconVisibility // Ha a Hatarido Exceptiont kap akkor az ErrorIcon megjelenik
        {
            get
            {
                return TaskValidationHelper.ValidateDeadline(_task.Deadline) == null || !_isDeadlineChanged ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public Visibility StatusErrorIconVisibility // Ha a Statusz Exceptiont kap akkor az ErrorIcon megjelenik
        {
            get
            {
                return TaskValidationHelper.ValidateStatus(_task.Status, _task.IdTask) == null || !_isStatusChanged ? Visibility.Hidden : Visibility.Visible;
            }
        }


        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>(); // ??
        public string Error { get { return null; } } // IDataErrorInfo-hez kell

        public string this[string propertyName] // ??
        {
            get
            {
                string result = null;

                if (_isTitleChanged || _isDeadlineChanged || _isStatusChanged)
                {
                    switch (propertyName)
                    {
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
                }

                return result;
            }
        }

        public ICommand SaveCommand { get; }
        public RelayCommand ExitWindowCommand { get; private set; }

        public UserProfileTaskViewModel(Task task, UserProfileTaskView view)
        {
            _task = task;
            _view = view;

            SaveCommand = new RelayCommand(Save, CanSave);
            ExitWindowCommand = new RelayCommand(ExitWindow, CanExecuteExit);
        }


        private bool CanExecuteExit(object arg)
        {
            return true;
        }
        private void ExitWindow(object obj) // Bezarja az ablakot
        {
            this._view.Close();
        }


        private bool CanSave(object arg) // mentheto amig nem nullak az ertekek(kiveve a Leiras az lehet ures) es amig valamelyik ertek megvaltozott
        {
            return !string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Deadline.ToString()) && 
                   (_isTitleChanged || _isDescriptionChanged || _isDeadlineChanged || _isStatusChanged);
        }

        private void Save(object obj)
        {
            try
            {
                if (CheckIfNewTask()) // az Id == 0 akkor uj Feladatkent menti es ad neki Id-t kulonben meg modositja a meglevo Feladatot
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
                MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (TaskValidationException)
            {

            }
        }


        private bool CheckIfNewTask()
        {
            return this._task.IdTask == 0;
        }


        private void CreateTask() // Letrehozza az uj Feladatot
        {
            this._task.IdTask = new TaskRepository(new TaskLogic()).CreateTask(this._task, this._task.User_idUser);

            //CreateTaskToList(this._task);

            if (this.CurrentUser.Username != LoginViewModel.LoggedUser.Username) // ha nem Adminnak adta a feladatot akkor kap emailt
            {
                new NotificationRepository(new NotificationLogic()).CreateNotificationForTask("NotificationNewTask", 0, this._task.IdTask);
                SendNotificationEmail(" has been added to your tasks!");
            }
            RefreshValues();
        }
        /*public event Action<Task> TaskCreated; NEM JOOOO!!!!
        public void CreateTaskToList(Task task)
        {
            TaskCreated?.Invoke(task);
        }*/


        private void UpdateTask() // Modositja a Feladatot
        {
            new TaskRepository(new TaskLogic()).UpdateTask(this._task, this._task.IdTask, this._task.User_idUser);

            if (this.CurrentUser.Username != LoginViewModel.LoggedUser.Username)
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
            }

            //Disabled legyen mentes utan a Save gomb es az ertesitesek miatt is kell
            _isTitleChanged = false;
            _isDescriptionChanged = false;
            _isDeadlineChanged = false;
            _isStatusChanged = false;
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
            MailMessage mm = new MailMessage("wpfszakdoga@gmail.com", this.CurrentUser.Email, EmailSubject, EmailMessage);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            client.Send(mm);
        }

        public void RefreshValues()
        {
            this.IdTask = 0;
            this.Title = "";
            this.Description = "";
            this.Deadline = DateTime.Today.AddDays(1);
        }
    }
}
