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
using WpfDemo.Components;
using WpfDemo.ViewModel.Command;


namespace WpfDemo.ViewModel
{
    public class TaskViewModel : ViewModelBase, IDataErrorInfo
    {
        private Task _task;
        private User _user;
        private bool _isUserChanged = false;
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
                OnPropertyChanged(nameof(TaskStatusString));
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
                    _isUserChanged = true;
                }
                OnPropertyChanged(nameof(UserErrorIconVisibility));
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
                return _user.Username;
            }
        }

        public List<User> UserList // Kell h mikor uj letrehozasa utan kivalasztunk egy taskot akkor ne legyen ures a User
        {
            get;
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

        public string NotificationText // Ertesites szovege
        {
            get
            {
                if (IsNotificationsOn)
                {
                    List<string> notificationList = new List<string>();

                    try
                    {
                        notificationList = LoginViewModel.LoggedUser.Status == 1 ?
                        new NotificationRepository(new NotificationLogic()).GetTaskNotificationsForAdmin(this._task.IdTask) :
                        new NotificationRepository(new NotificationLogic()).GetTaskNotificationsForEmployee(this._task.IdTask);
                    }
                    catch (SqlException)
                    {
                        MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                    string notifications = string.Join(Environment.NewLine, notificationList.Select(notification => ResourceHandler.GetResourceString(notification)));

                    return string.IsNullOrWhiteSpace(notifications) ? null : notifications;
                }
                else
                {
                    return null;
                }
            }
        }

        public string ListTasksBackground // Ertesites eseten a listaban levo feladat hattere megvaltozik
        {
            get
            {
                if (IsNotificationsOn) // try-catch????
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

        public int SumDuration // UserProfileView kordiagramhoz
        {
            get
            {
                int sumDuration = 0;

                foreach (Record record in new RecordRepository(new RecordLogic()).GetTaskRecords(_task.IdTask)) // a kivalasztott feladat rogziteseinek az idotartamat osszeadjuk
                {
                    sumDuration += record.Duration;
                }

                /*foreach (Record record in new RecordRepository(new RecordLogic()).GetTaskRecords(_task.IdTask).Where(record =>
                                        record.Date > DateTime.Today.AddYears(-1)).ToList())
                {
                    sumDuration += record.Duration;
                }

                foreach (Record record in new RecordRepository(new RecordLogic()).GetTaskRecords(_task.IdTask).Where(record =>
                                        record.Date > DateTime.Today.AddMonths(-1)).ToList())
                {
                    sumDuration += record.Duration;
                }

                foreach (Record record in new RecordRepository(new RecordLogic()).GetTaskRecords(_task.IdTask).Where(record =>
                        record.Date > DateTime.Today.AddDays(-7)).ToList())
                {
                    sumDuration += record.Duration;
                }*/


                return sumDuration;
            }
        }
        public string SumDurationFormat
        {
            get
            {
                return TimeSpan.FromMinutes(SumDuration).ToString("hh':'mm");
            }
        }

        public string TaskViewUserRowHeight // Admin eseten latszodik ez a sor(User-t mutato sor) egyebkent meg nem
        {
            get
            {
                return LoginViewModel.LoggedUser.Status == 1 ? "0.8*" : "0";
            }
        }

        public string IsTaskViewDescriptionBackground // Leiras hattere megvaltozik ha nem Admin lepett be 
        {
            get
            {
                return LoginViewModel.LoggedUser.Status == 0 ? "#FFC7C7C7" : "#eee";
            }
        }

        public bool IsSelectedTaskValuesChanged // OnTaskCanceled-hez h a benne levo try-catch csak akkor fusson le ha valtoztak az adatok
        {
            get
            {
                return _isTitleChanged || _isDescriptionChanged || _isDeadlineChanged || _isStatusChanged ? true : false;
            }
        }

        public bool IsTaskViewDescriptionReadOnly // Leiras nem modosithato ha nem Admin lep be 
        {
            get
            {
                return LoginViewModel.LoggedUser.Status == 0;
            }
        }


        public Visibility TaskViewUserComboBoxVisibility // uj feladat letrehozasa eseten latszodik a ComboBox ahol kivalasztjuk a Usert akinek adjuk a feladatot
        {
            get
            {
                return _task.IdTask == 0 ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility TaskViewUserTextBoxVisibility // letezo feladatra kattintva a User neve TextBoxkent lesz lathato
        {
            get
            {
                return _task.IdTask != 0 ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility TaskViewTitleDeadlineComboBoxVisibility // ha nem Admin lepett be akkor a feladat cime,hatarideje ComboBoxkent nem jelenikk meg
        {
            get
            {
                return LoginViewModel.LoggedUser.Status != 0 ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility TaskViewTitleDeadlineTextBoxVisibility // ha nem Admin lepett be akkor a feladat cime,hatarideje TextBoxkent jelenikk meg
        {
            get
            {
                return LoginViewModel.LoggedUser.Status == 0 ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility TaskViewStatusComboboxVisibility // A Statusz ComboBox lathato ha letezo feladatot nezz a belepett felhasznalo(Admin eseten a sajatja lathato mase nem)
        {
            get
            {
                if (_user != null)
                {
                    if (_user.Username == LoginViewModel.LoggedUser.Username && _task.IdTask != 0)
                    {
                        return Visibility.Visible;
                    }
                    else
                    {
                        return Visibility.Collapsed;
                    }
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }

        public Visibility TaskViewStatusTextBoxVisibility // Uj Feladat TextBoxkent irja ki a Statuszt(Admin eseten pedig mas feladatara kattintva is TextBoxkent irja ki)
        {
            get
            {
                if (_user != null)
                {
                    if (_user.Username != LoginViewModel.LoggedUser.Username || _task.IdTask == 0)
                    {
                        return Visibility.Visible;
                    }
                    else
                    {
                        return Visibility.Collapsed;
                    }
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }

        public Visibility UserErrorIconVisibility // Ha a Felhasznalo Exceptiont kap akkor az ErrorIcon megjelenik
        {
            get
            {
                return TaskValidationHelper.ValidateUser(this._user) == null || !_isUserChanged ? Visibility.Hidden : Visibility.Visible;
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

                if (_isUserChanged || _isTitleChanged || _isDeadlineChanged || _isStatusChanged)
                {
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
                }

                return result;
            }
        }


        public ICommand SaveCommand { get; }
        public RelayCommand CancelTaskViewCommand { get; private set; }


        public TaskViewModel(Task task, List<User> userList)
        {
            _task = task;
            UserList = userList; // betolti a felhasznalokat(akiknek lehet feladatot csinalni)

            SaveCommand = new RelayCommand(Save, CanSave);
            CancelTaskViewCommand = new RelayCommand(CancelTaskView, CanCancelTaskView);
        }


        private bool CanSave(object arg) // mentheto amig nem nullak az ertekek(kiveve a Leiras az lehet ures) es amig valamelyik ertek megvaltozott
        {
            return User != null && !string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Deadline.ToString()) && 
                   (_isUserChanged || _isTitleChanged || _isDescriptionChanged || _isDeadlineChanged || _isStatusChanged);
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
            this._task.IdTask = new TaskRepository(new TaskLogic()).CreateTask(this._task, this._user.IdUser);

            CreateTaskToList(this); // hozzaadja a listahoz

            if (this._user.Status != 1) // ha nem Adminnak adta a feladatot akkor kap emailt
            {
                new NotificationRepository(new NotificationLogic()).CreateNotificationForTask("NotificationNewTask", 0, this._task.IdTask);
                SendNotificationEmail(" has been added to your tasks!");
            }

            IsChangedTaskValuesToFalse(); // kell h legkozelebb ha "Save" gombra nyomok akkor ne maradjanak bent a True-s bool ertekek
        }
        public event Action<TaskViewModel> TaskCreated;
        public void CreateTaskToList(TaskViewModel taskViewModel)
        {
            TaskCreated?.Invoke(taskViewModel);
        }

        private void UpdateTask() // Modositja a Feladatot
        {
            new TaskRepository(new TaskLogic()).UpdateTask(this._task, this._task.IdTask, this._task.User_idUser);

            if (LoginViewModel.LoggedUser.Status != 0) // Csak az admin tudja modositani a Feladat Hataridejet
            {
                UpdateTaskToList(this); // Modositja a keresesi ertekeket ha kell
            }

            if (this._user.Status != 1) // ha a Feladat nem az Adminhoz tartozik akkor a Feladathoz keszul ertesites
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
            }

            IsChangedTaskValuesToFalse();
        }
        public event Action<TaskViewModel> TaskUpdated;
        public void UpdateTaskToList(TaskViewModel taskViewModel)
        {
            TaskUpdated?.Invoke(taskViewModel);
        }

        public void IsChangedTaskValuesToFalse()
        {
            _isUserChanged = false;
            _isTitleChanged = false;
            _isDescriptionChanged = false;
            _isDeadlineChanged = false;
            _isStatusChanged = false;
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
            CancelTask(obj); // Eltunteti a jelenlegi Feladatot
            IsChangedTaskValuesToFalse(); // kell h ha megvaltoztattam az ertekeket de a "Cancel" gombra nyomtam igy False-ra allitom a valtoztatasokat(tehat disable lesz a "Save" gomb)
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
    }
}