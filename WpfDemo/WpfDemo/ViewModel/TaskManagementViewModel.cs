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
using WpfDemo.ViewModel.Command;

namespace WpfDemo.ViewModel
{
    public class TaskManagementViewModel : ViewModelBase
    {
        public ObservableCollection<User> UserList { get; } = new ObservableCollection<User>();
        public ObservableCollection<TaskViewModel> TaskList { get; } = new ObservableCollection<TaskViewModel>();
        private int _lastSelectedTaskID = 0; // utoljara valasztott Task ID-ja
        private int _lastSelectedTaskCount = 0; // utoljara valasztott elem indexe a TaskList-bol


        private TaskViewModel _selectedTask;
        public TaskViewModel SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;

                if (_selectedTask != null)
                {
                    if (_lastSelectedTaskID != 0 && _lastSelectedTaskID != _selectedTask.Task.IdTask) // ha nem az utoljara valasztott Task id-ja nem egyezik a mostanival
                    {
                        for (int i = 0; i < TaskList.Count(); i++)
                        {
                            if (TaskList[i].IdTask == _lastSelectedTaskID) // ha egyezik az utoljara valasztott Task Id-ja a Listaban talalhato i. IdTask-dal es lementem a megfelelo indexet
                            {
                                _lastSelectedTaskCount = i;
                            }
                        }

                        if (TaskList[_lastSelectedTaskCount].IsSelectedTaskValuesChanged) // visszatoltom az eredeti adatokat ha tortent valtozas
                        {
                            try
                            {
                                Task SelectedTaskValues = new TaskRepository(new TaskLogic()).GetTaskByID(_lastSelectedTaskID);
                                TaskList[_lastSelectedTaskCount].Title = SelectedTaskValues.Title;
                                TaskList[_lastSelectedTaskCount].Description = SelectedTaskValues.Description;
                                TaskList[_lastSelectedTaskCount].Deadline = SelectedTaskValues.Deadline;
                                TaskList[_lastSelectedTaskCount].Status = SelectedTaskValues.Status;

                                TaskList[_lastSelectedTaskCount].IsChangedTaskValuesToFalse(); // igy a Save gomb disabled lesz ha visszakattintok
                            }
                            catch (SqlException)
                            {
                                MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                    }
                    _lastSelectedTaskID = _selectedTask.Task.IdTask;
                }

                OnPropertyChanged(nameof(SelectedTask));
                OnPropertyChanged(nameof(SelectedTaskVisibility));
            }
        }


        private string _searchValue;
        public string SearchValue // keresesi szoveg bindolashoz
        {
            get { return _searchValue; }
            set
            {
                _searchValue = value;
                OnPropertyChanged(nameof(SearchValue));
                //SortingByCheckBox(_searchValue);
            }
        }

        private DateTime _deadlineFrom = DateTime.Today.AddYears(100);
        private int _deadlineFromHelper = 0; // hogy 1x fusson le csak a foreach amivel megkapja a listaban levo legkorabbi Hataridot belepeskor
        public DateTime DeadlineFrom
        {
            get
            {
                if (_deadlineFromHelper < 1) // Belepeskor beallitom a DeadlineFrom datumat a legkorabbira
                {
                    foreach (TaskViewModel taskViewModel in TaskList)
                    {
                        if (taskViewModel.Task.Deadline < _deadlineFrom)
                        {
                            _deadlineFrom = taskViewModel.Task.Deadline;
                        }
                    }
                    _deadlineFromHelper++;
                }
                return _deadlineFrom;
            }
            set
            {
                _deadlineFrom = value;
                OnPropertyChanged(nameof(DeadlineFrom));
            }
        }

        private DateTime _deadlineTo = DateTime.Parse("0001.01.01");
        private int _deadlineToHelper = 0; // hogy 1x fusson le csak a foreach amivel megkapja a listaban levo legkesobbi datumot belepeskor
        public DateTime DeadlineTo
        {
            get
            {
                if (_deadlineToHelper < 1) // Belepeskor beallitom a DeadlineTo datumat a legkesobbire
                {
                    foreach (TaskViewModel taskViewModel in TaskList)
                    {
                        if (taskViewModel.Task.Deadline > _deadlineTo)
                        {
                            _deadlineTo = taskViewModel.Task.Deadline;
                        }
                    }
                    _deadlineToHelper++;
                }
                return _deadlineTo;
            }
            set
            {
                _deadlineTo = value;
                OnPropertyChanged(nameof(DeadlineTo));
            }
        }


        private bool _isMyTasksCheckBoxChecked = true;
        public bool IsMyTasksCheckBoxChecked // Sajat feladatokat mutato checkbox pipajahoz a bindolashoz
        {
            get
            {
                return _isMyTasksCheckBoxChecked;
            }
            set
            {
                _isMyTasksCheckBoxChecked = value;
                OnPropertyChanged(nameof(IsMyTasksCheckBoxChecked));
            }
        }


        private bool _isActiveTasksCheckBoxChecked = true;
        public bool IsActiveTasksCheckBoxChecked // aktiv feladatokat mutato checkbox pipajahoz a bindolashoz
        {
            get
            {
                return _isActiveTasksCheckBoxChecked;
            }
            set
            {
                _isActiveTasksCheckBoxChecked = value;
                OnPropertyChanged(nameof(IsActiveTasksCheckBoxChecked));
            }
        }


        private bool _isNotificationsCheckBoxChecked = true;
        public bool IsNotificationsCheckBoxChecked // ertesiteseket mutato checkbox pipajahoz a bindolashoz
        {
            get
            {
                return _isNotificationsCheckBoxChecked;
            }
            set
            {
                _isNotificationsCheckBoxChecked = value;
                OnPropertyChanged(nameof(IsNotificationsCheckBoxChecked));
            }
        }


        public Visibility SelectedTaskVisibility // Kivalasztott feladat lathatosaga
        {
            get
            {
                if (SelectedTask != null)
                {
                    SelectedTask.TaskCanceled += OnTaskCanceled; // TaskCanceled Event("Cancel" gomb megnyomasa) eseten a kivalasztott feladat eltunik
                }
                return SelectedTask == null ? Visibility.Collapsed : Visibility.Visible;
            }
        }
        private void OnTaskCanceled(Object obj)
        {
            if (SelectedTask != null) // Ha 2x megnyitottam ugyanazt egymas utan akkor valamiert lefutt tobbszor ezert van ez az if
            {
                if (SelectedTask.IsSelectedTaskValuesChanged) // Cancel eseten visszatoltom az eredti adatokat ha megvaltoztattuk oket
                {
                    try
                    {
                        Task SelectedTaskValues = new TaskRepository(new TaskLogic()).GetTaskByID(SelectedTask.IdTask);
                        SelectedTask.Title = SelectedTaskValues.Title;
                        SelectedTask.Description = SelectedTaskValues.Description;
                        SelectedTask.Deadline = SelectedTaskValues.Deadline;
                        SelectedTask.Status = SelectedTaskValues.Status;
                    }
                    catch (SqlException)
                    {
                        MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }

                SelectedTask = null; // null-ozom h a SelectedTask Visibility-je Collapsed legyen
            }
        }

        public Visibility NewTaskButtonVisibility // uj feladat letrehozasa gomb lathatosaganak bindaloasahoz (ha admin lep be akkor lathato kulonben meg nem)
        {
            get
            {
                return LoginViewModel.LoggedUser.Status == 0 ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility TaskCheckBoxAndTextVisibility // Sajat feladatokat mutato checkbox es szoveg lathatosaganak a bindolasahoz (ha admin lep be akkor lathato kulonben meg nem)
        {
            get
            {
                return LoginViewModel.LoggedUser.Status == 0 ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility ListTasksViewUserVisibility // A feladatok listaban a User oszlop lathatosaganak a bindolasahoz (ha admin lep be akkor lathato kulonben meg nem)
        {
            get
            {
                return LoginViewModel.LoggedUser.Status == 0 ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility ListTasksViewContextMenuVisibility // (Delete Header Visibility) Csak admin eseteben jelenik meg jobb klikkre egy torles lehetoseg
        {
            get
            {
                return LoginViewModel.LoggedUser.Status == 0 ? Visibility.Collapsed : Visibility.Visible;
            }
        }


        public RelayCommand CreateTaskCommand { get; private set; }
        public RelayCommand RefreshTaskListCommand { get; private set; }
        public RelayCommand SortingByCheckBoxCommand { get; private set; }
        public RelayCommand SearchingCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }
        public RelayCommand HasReadCommand { get; private set; }
        public RelayCommand NotificationsSwitchOnOffCommand { get; private set; }



        public TaskManagementViewModel()
        {
            LoadUsers(); // Felhasznalok betoltese(ha esetleg ujat hozna letre)
            RefreshTaskList(_searchValue); // Feladatok frissitese(Lista frissitese/betoltese)

            CreateTaskCommand = new RelayCommand(CreateTask, CanExecuteShow);
            RefreshTaskListCommand = new RelayCommand(RefreshTaskList, CanExecuteRefresh);
            SortingByCheckBoxCommand = new RelayCommand(SortingByCheckBox, CanExecuteSort);
            SearchingCommand = new RelayCommand(Search, CanExecuteSearch);
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
            RefreshTaskList(obj);  //KELL?????????????
            LoadUsers();

            SelectedTask = new TaskViewModel(new Task() { Deadline = DateTime.Today.AddDays(1) }, UserList.ToList());
            SelectedTask.TaskCreated += OnTaskCreated; // TaskCreated Event ("Save" gomb megnyomasa) eseten hozzaadodik a listahoz a feladat es ujat tudsz letrehozni megint
        }
        private void OnTaskCreated(TaskViewModel taskViewModel)
        {
            TaskList.Add(taskViewModel); // hozzaadja a listahoz

            if (taskViewModel.Task.Deadline > _deadlineTo) // Megnezem h az uj Hatarido kesobbi-e mint ami a DeadlineTo-ba van es ha igen akkor kicserelem
            {
                _deadlineTo = taskViewModel.Task.Deadline;
                OnPropertyChanged(nameof(DeadlineTo)); // megvaltozzon a kiiras
            }

            if (taskViewModel.Task.Deadline < _deadlineFrom) // Megnezem h az uj Hatarido korabbi-e mint ami a DeadlineFrom-ba van es ha igen akkor kicserelem
            {
                _deadlineFrom = taskViewModel.Task.Deadline;
                OnPropertyChanged(nameof(DeadlineFrom)); // megvaltozzon a kiiras
            }

            // Uj letrehozasahoz
            SelectedTask = new TaskViewModel(new Task() { Deadline = DateTime.Today.AddDays(1) }, UserList.ToList());
            SelectedTask.TaskCreated += OnTaskCreated;
        }


        private bool CanExecuteRefresh(object arg)
        {
            return true;
        }
        private void RefreshTaskList(object obj)
        {
            LoadTasks(); // Feladatok betoltese

            // A keresesi szoveget uresse teszem
            _searchValue = "";
            OnPropertyChanged(nameof(SearchValue));

            SortingByCheckBox(obj); // Lista frissitese/szurese
        }

        public void LoadTasks() // Feladatok betoltese
        {
            TaskList.Clear();

            try
            {
                var tasks = new TaskRepository(new TaskLogic()).GetAllTasks();

                tasks.ForEach(task =>
                {
                    var taskViewModel = new TaskViewModel(task, UserList.ToList());
                    taskViewModel.User = UserList.First(user => user.IdUser == task.User_idUser);
                    TaskList.Add(taskViewModel);
                });
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        public void LoadUsers() // Felhasznalok betoltese
        {
            UserList.Clear();

            try
            {
                var users = new UserRepository(new UserLogic()).GetAllUsers();
                users.ForEach(user => UserList.Add(user));
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private bool CanExecuteSort(object arg)
        {
            return true;
        }
        private void SortingByCheckBox(object obj) // Lista szurese/frissitese
        {
            try
            {
                if (IsMyTasksCheckBoxChecked && !IsActiveTasksCheckBoxChecked)
                {
                    TaskList.Clear();

                    var tasks = new TaskRepository(new TaskLogic()).GetUserTasks(LoginViewModel.LoggedUser.IdUser);

                    tasks.ForEach(task =>
                    {
                        var taskViewModel = new TaskViewModel(task, UserList.ToList());
                        taskViewModel.User = UserList.First(user => user.IdUser == task.User_idUser);
                        TaskList.Add(taskViewModel);
                    });
                }
                else if (IsMyTasksCheckBoxChecked && IsActiveTasksCheckBoxChecked)
                {
                    TaskList.Clear();

                    var tasks = new TaskRepository(new TaskLogic()).GetAllActiveTasksFromUser(LoginViewModel.LoggedUser.IdUser);

                    tasks.ForEach(task =>
                    {
                        var taskViewModel = new TaskViewModel(task, UserList.ToList());
                        taskViewModel.User = UserList.First(user => user.IdUser == task.User_idUser);
                        TaskList.Add(taskViewModel);
                    });
                }
                else if (!IsMyTasksCheckBoxChecked && IsActiveTasksCheckBoxChecked)
                {
                    TaskList.Clear();

                    var tasks = new TaskRepository(new TaskLogic()).GetAllActiveTasks();

                    tasks.ForEach(task =>
                    {
                        var taskViewModel = new TaskViewModel(task, UserList.ToList());
                        taskViewModel.User = UserList.First(user => user.IdUser == task.User_idUser);
                        TaskList.Add(taskViewModel);
                    });
                }
                else
                {
                    TaskList.Clear();

                    var tasks = new TaskRepository(new TaskLogic()).GetAllTasks();

                    tasks.ForEach(task =>
                    {
                        var taskViewModel = new TaskViewModel(task, UserList.ToList());
                        taskViewModel.User = UserList.First(user => user.IdUser == task.User_idUser);
                        TaskList.Add(taskViewModel);
                    });
                }

                _deadlineFrom = DateTime.Today.AddYears(1);
                if (TaskList.Count == 0) // ha nincs task akkor a mai datumot kapja meg
                {
                    _deadlineTo = DateTime.Today;
                }
                else
                {
                    _deadlineTo = DateTime.Parse("0001.01.01");
                }
                foreach (TaskViewModel taskViewModel in TaskList)
                {
                    if (taskViewModel.Task.Deadline < _deadlineFrom)
                    {
                        _deadlineFrom = taskViewModel.Task.Deadline;
                    }
                    if (taskViewModel.Task.Deadline > _deadlineTo)
                    {
                        _deadlineTo = taskViewModel.Task.Deadline;
                    }
                }

                OnPropertyChanged(nameof(DeadlineFrom)); // kell h megvaltozzon a DatePickerben a datum
                OnPropertyChanged(nameof(DeadlineTo));  // kell h megvaltozzon a DatePickerben a datum
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private bool CanExecuteSearch(object arg)
        {
            return true;
        }
        private void Search(object obj) // Lista szurese/frissitese
        {
            try
            {
                if (String.IsNullOrWhiteSpace(_searchValue))
                {
                    if (IsMyTasksCheckBoxChecked && !IsActiveTasksCheckBoxChecked)
                    {
                        TaskList.Clear();

                        var tasks = new TaskRepository(new TaskLogic()).GetUserTasks(LoginViewModel.LoggedUser.IdUser).Where(task =>
                                    task.Deadline >= _deadlineFrom && task.Deadline <= _deadlineTo).ToList();

                        tasks.ForEach(task =>
                        {
                            var taskViewModel = new TaskViewModel(task, UserList.ToList());
                            taskViewModel.User = UserList.First(user => user.IdUser == task.User_idUser);
                            TaskList.Add(taskViewModel);
                        });
                    }
                    else if (IsMyTasksCheckBoxChecked && IsActiveTasksCheckBoxChecked)
                    {
                        TaskList.Clear();

                        var tasks = new TaskRepository(new TaskLogic()).GetAllActiveTasksFromUser(LoginViewModel.LoggedUser.IdUser).Where(task =>
                                    task.Deadline >= _deadlineFrom && task.Deadline <= _deadlineTo).ToList();

                        tasks.ForEach(task =>
                        {
                            var taskViewModel = new TaskViewModel(task, UserList.ToList());
                            taskViewModel.User = UserList.First(user => user.IdUser == task.User_idUser);
                            TaskList.Add(taskViewModel);
                        });
                    }
                    else if (!IsMyTasksCheckBoxChecked && IsActiveTasksCheckBoxChecked)
                    {
                        TaskList.Clear();

                        var tasks = new TaskRepository(new TaskLogic()).GetAllActiveTasks().Where(task =>
                                    task.Deadline >= _deadlineFrom && task.Deadline <= _deadlineTo).ToList();

                        tasks.ForEach(task =>
                        {
                            var taskViewModel = new TaskViewModel(task, UserList.ToList());
                            taskViewModel.User = UserList.First(user => user.IdUser == task.User_idUser);
                            TaskList.Add(taskViewModel);
                        });
                    }
                    else
                    {
                        TaskList.Clear();

                        var tasks = new TaskRepository(new TaskLogic()).GetAllTasks().Where(task =>
                                    task.Deadline >= _deadlineFrom && task.Deadline <= _deadlineTo).ToList();

                        tasks.ForEach(task =>
                        {
                            var taskViewModel = new TaskViewModel(task, UserList.ToList());
                            taskViewModel.User = UserList.First(user => user.IdUser == task.User_idUser);
                            TaskList.Add(taskViewModel);
                        });
                    }
                }
                else
                {
                    if (IsMyTasksCheckBoxChecked && !IsActiveTasksCheckBoxChecked)
                    {
                        TaskList.Clear();

                        var tasks = new TaskRepository(new TaskLogic()).GetUserTasks(LoginViewModel.LoggedUser.IdUser).Where(task =>
                                    (task.Deadline >= _deadlineFrom && task.Deadline <= _deadlineTo) 
                                    && (task.Title.Contains(_searchValue) || task.Description.Contains(_searchValue) || task.Status.ToString().Contains(_searchValue))).ToList();

                        tasks.ForEach(task =>
                        {
                            var taskViewModel = new TaskViewModel(task, UserList.ToList());
                            taskViewModel.User = UserList.First(user => user.IdUser == task.User_idUser);
                            TaskList.Add(taskViewModel);
                        });
                    }
                    else if (IsMyTasksCheckBoxChecked && IsActiveTasksCheckBoxChecked)
                    {
                        TaskList.Clear();

                        var tasks = new TaskRepository(new TaskLogic()).GetAllActiveTasksFromUser(LoginViewModel.LoggedUser.IdUser).Where(task =>
                                    (task.Deadline >= _deadlineFrom && task.Deadline <= _deadlineTo)
                                    && (task.Title.Contains(_searchValue) || task.Description.Contains(_searchValue) || task.Status.ToString().Contains(_searchValue))).ToList();

                        tasks.ForEach(task =>
                        {
                            var taskViewModel = new TaskViewModel(task, UserList.ToList());
                            taskViewModel.User = UserList.First(user => user.IdUser == task.User_idUser);
                            TaskList.Add(taskViewModel);
                        });
                    }
                    else if (!IsMyTasksCheckBoxChecked && IsActiveTasksCheckBoxChecked)
                    {
                        TaskList.Clear();

                        var tasks = new TaskRepository(new TaskLogic()).GetAllActiveTasks().Where(task =>
                                    (task.Deadline >= _deadlineFrom && task.Deadline <= _deadlineTo)
                                    && (task.Title.Contains(_searchValue) || task.Description.Contains(_searchValue) || task.Status.ToString().Contains(_searchValue)
                                    || new UserRepository(new UserLogic()).GetUserByID(task.User_idUser).Username.Contains(_searchValue))).ToList();

                        tasks.ForEach(task =>
                        {
                            var taskViewModel = new TaskViewModel(task, UserList.ToList());
                            taskViewModel.User = UserList.First(user => user.IdUser == task.User_idUser);
                            TaskList.Add(taskViewModel);
                        });
                    }
                    else
                    {
                        TaskList.Clear();

                        var tasks = new TaskRepository(new TaskLogic()).GetAllTasks().Where(task =>
                                    (task.Deadline >= _deadlineFrom && task.Deadline <= _deadlineTo)
                                    && (task.Title.Contains(_searchValue) || task.Description.Contains(_searchValue) || task.Status.ToString().Contains(_searchValue)
                                    || new UserRepository(new UserLogic()).GetUserByID(task.User_idUser).Username.Contains(_searchValue))).ToList();
                        tasks.ForEach(task =>
                        {
                            var taskViewModel = new TaskViewModel(task, UserList.ToList());
                            taskViewModel.User = UserList.First(user => user.IdUser == task.User_idUser);
                            TaskList.Add(taskViewModel);
                        });
                    }
                }
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool CanDeleteTask(object arg)
        {
            return _selectedTask != null && LoginViewModel.LoggedUser.Status != 0;
        }

        private void DeleteTask(object obj)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show(Resources.TaskDeleteQuestion1 + SelectedTask.Title + Resources.TaskDeleteQuestion2,
                                                Resources.Warning, MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    new TaskRepository(new TaskLogic()).DeleteTask(SelectedTask.IdTask);
                    MessageBox.Show(Resources.TaskDeletedMessage, Resources.Information, MessageBoxButton.OK, MessageBoxImage.Information);

                    if(this.SelectedTask.User.Username != LoginViewModel.LoggedUser.Username) // ha a User nem admin(Miutan erre csak az Admin kepes)
                    {
                        SendNotificationEmail(SelectedTask.Title); // kuld emailt h toroltek a feladatat
                    }
                    RefreshTaskList(obj); // Frissiti a listat torles eseten
                }
                catch (SqlException)
                {
                    MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private bool CanExecuteReadTaskNotifications(object arg)
        {
            if (TaskViewModel.IsNotificationsOn) // ha lathatoak az ertesitesek
            {
                if (LoginViewModel.LoggedUser.Status == 1) // megkapja az Adminhoz tartozo ertesiteseket a megfelelo feladathoz
                {
                    return _selectedTask != null && new NotificationRepository(new NotificationLogic()).GetTaskNotificationsForAdmin(SelectedTask.IdTask) != null;
                }
                else // megkapja a sima Userhez tartozo ertesiteseket a megfelelo feladathoz
                {
                    return _selectedTask != null && new NotificationRepository(new NotificationLogic()).GetTaskNotificationsForEmployee(SelectedTask.IdTask) != null;
                }
            }
            else
            {
                return false;
            }
        }

        private void HasRead(object obj) // elolvassa az ertesitest
        {
            try
            {
                new NotificationRepository(new NotificationLogic()).HasReadNotification(SelectedTask.IdTask, LoginViewModel.LoggedUser.Status);

                RefreshTaskList(obj); // frissiti a listat h eltunjon az ertesites
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool CanExecuteSwitch(object arg)
        {
            return true;
        }
        private void NotificationSwitchOnOff(object obj)
        {
            if (IsNotificationsCheckBoxChecked) // ha ki van pipalva a CheckBox
            {
                TaskViewModel.IsNotificationsOn = true; // ertesitesek be vannak kapcsolva
                RefreshTaskList(obj);
            }
            else
            {
                TaskViewModel.IsNotificationsOn = false; // ertesitesek ki vannak kapcsolva
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
    }
}
