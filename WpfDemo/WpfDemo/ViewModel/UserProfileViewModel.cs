using System;
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
    public class UserProfileViewModel : ViewModelBase
    {
        private User _currentUser;
        private TaskViewModel _selectedTask;
        public ObservableCollection<User> UserListForTaskList { get; } = new ObservableCollection<User>();

        private ObservableCollection<TaskViewModel> _taskList = new ObservableCollection<TaskViewModel>();
        public ObservableCollection<Task> TaskListForRecordList { get; } = new ObservableCollection<Task>();
        private ObservableCollection<RecordViewModel> _recordList = new ObservableCollection<RecordViewModel>();


        public ObservableCollection<TaskViewModel> TaskList
        {
            get
            {
                return _taskList;
            }
        }
        public ObservableCollection<RecordViewModel> RecordList
        {
            get
            {
                return _recordList;
            }
        }

        public User CurrentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }


        public TaskViewModel SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
            }
        }


        private string _searchTaskListValue;
        public string SearchTaskListValue // Feladatok listajaban valo keresesi szoveg bindolashoz
        {
            get { return _searchTaskListValue; }
            set
            {
                _searchTaskListValue = value;
                OnPropertyChanged(nameof(SearchTaskListValue));
                if (String.IsNullOrWhiteSpace(_searchTaskListValue))
                {
                    LoadTasks(CurrentUser.IdUser);
                }
                else
                {
                    _taskList.Clear();

                    try
                    {
                        var tasks = new TaskRepository(new TaskLogic()).GetUserTasks(CurrentUser.IdUser).Where(task => task.Title.Contains(_searchTaskListValue)
                                    || task.Description.Contains(_searchTaskListValue) || task.Deadline.ToShortDateString().Contains(_searchTaskListValue) 
                                    || task.Status.ToString().Contains(_searchTaskListValue)).ToList();
                        tasks.ForEach(task =>
                        {
                            var taskViewModel = new TaskViewModel(task, UserListForTaskList.ToList());
                            taskViewModel.User = UserListForTaskList.First(user => user.IdUser == task.User_idUser);
                            TaskList.Add(taskViewModel);
                        });
                    }
                    catch (SqlException)
                    {
                        MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }

        private string _searchRecordListValue;
        public string SearchRecordListValue // Rogzitesi listaban valo keresesi szoveg bindolashoz
        {
            get { return _searchRecordListValue; }
            set
            {
                _searchRecordListValue = value;
                OnPropertyChanged(nameof(SearchRecordListValue));

                if (String.IsNullOrWhiteSpace(_searchRecordListValue))
                {
                    LoadRecords(CurrentUser.IdUser);
                }
                else
                {
                    _recordList.Clear();

                    try
                    {
                        var records = new RecordRepository(new RecordLogic()).GetUserRecords(CurrentUser.IdUser).Where(record => 
                                      record.Date.ToShortDateString().Contains(_searchRecordListValue) || record.Comment.Contains(_searchRecordListValue) 
                                      || record.Duration.ToString().Contains(_searchRecordListValue) 
                                      || new TaskRepository(new TaskLogic()).GetTaskByID(record.Task_idTask).Title.Contains(_searchRecordListValue)).ToList();

                        records.ForEach(record =>
                        {
                            var recordViewModel = new RecordViewModel(record, TaskListForRecordList.ToList());
                            recordViewModel.Task = TaskListForRecordList.First(task => task.IdTask == record.Task_idTask);
                            _recordList.Add(recordViewModel);
                        });
                    }
                    catch (SqlException)
                    {
                        MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }

        public Visibility AddTaskButtonVisibility // uj Feladat hozzaadas gomb lathatosaga(Admin eseteben lathato)
        {
            get
            {
                return LoginViewModel.LoggedUser.Status == 0 ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public Visibility UserProfileViewTasksContextMenuVisibility // (Delete Header Visibility) Csak Admin eseteben jelenik meg jobb klikkre egy torles lehetoseg
        {
            get
            {
                return LoginViewModel.LoggedUser.Status == 0 ? Visibility.Collapsed : Visibility.Visible;
            }
        }


        public RelayCommand DeleteCommand { get; private set; }
        public RelayCommand ShowTaskCommand { get; private set; }

        public UserProfileViewModel(int userid)
        {
            LoadTasks(userid); // Betolti a Felhasznalo Feladatait
            LoadRecords(userid); // Betolti a Felhasznalo Rogziteseit

            DeleteCommand = new RelayCommand(DeleteTask, CanDeleteTask);
            ShowTaskCommand = new RelayCommand(ShowTask, CanShowTask);
        }

        private bool CanShowTask(object arg) // Csak Admin lathatja egy masik ablakban a feladatok adatait
        {
            return LoginViewModel.LoggedUser.Status != 0;
        }

        private void ShowTask(object obj)
        {
            if(_selectedTask == null) // Ha a valasztott feladat null akkor ujkent jon letre ellenkezo esetben pedig betolti a letezo feladat adatait
            {
                UserProfileTaskView Ipage = new UserProfileTaskView();
                (Ipage.DataContext as UserProfileTaskViewModel).CurrentTask.User_idUser = CurrentUser.IdUser;
                (Ipage.DataContext as UserProfileTaskViewModel).CurrentTask.Deadline = DateTime.Today.AddDays(1);
                Ipage.ShowDialog();

                //OnTaskCreated(SelectedTask);
            }
            else
            {
                UserProfileTaskView Ipage = new UserProfileTaskView();
                (Ipage.DataContext as UserProfileTaskViewModel).CurrentTask = SelectedTask.Task;
                Ipage.ShowDialog();
            }

            LoadTasks(CurrentUser.IdUser);
        }
        /*private void OnTaskCreated(Task task) NEEEEM JOOOO!!!!
        {
            TaskList.Add(task);
            //SelectedTask = new UserProfileTaskViewModel(new Task() { Deadline = DateTime.Today.AddDays(1) });
            //SelectedTask.TaskCreated += OnTaskCreated;
        }*/


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

                    if (this.CurrentUser.Username != LoginViewModel.LoggedUser.Username) // ha a User nem admin(Miutan erre csak az Admin kepes)
                    {
                        SendNotificationEmail(SelectedTask.Title); // kuld emailt h toroltek a feladatat
                    }
                    LoadTasks(SelectedTask.User_idUser); // Frissiti a listat torles eseten
                }
                catch (SqlException)
                {
                    MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
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
            MailMessage mm = new MailMessage("wpfszakdoga@gmail.com", CurrentUser.Email, EmailSubject, EmailMessage);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            client.Send(mm);
        }


        private void LoadTasks(int userid)
        {
            _taskList.Clear();
            UserListForTaskList.Clear();

            try
            {
                var user = new UserRepository(new UserLogic()).GetUserByID(userid);
                UserListForTaskList.Add(user);
                var tasks = new TaskRepository(new TaskLogic()).GetUserTasks(userid);

                tasks.ForEach(task =>
                {
                    var taskViewModel = new TaskViewModel(task, UserListForTaskList.ToList());
                    taskViewModel.User = UserListForTaskList.First(user => user.IdUser == task.User_idUser);
                    TaskList.Add(taskViewModel);
                });
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void LoadRecords(int userid)
        {
            _recordList.Clear();
            TaskListForRecordList.Clear();

            try
            {
                var tasks = new TaskRepository(new TaskLogic()).GetUserTasks(userid);
                tasks.ForEach(task => TaskListForRecordList.Add(task));

                var records = new RecordRepository(new RecordLogic()).GetUserRecords(userid);
                records.ForEach(record =>
                {
                    var recordViewModel = new RecordViewModel(record, TaskListForRecordList.ToList());
                    recordViewModel.Task = TaskListForRecordList.First(task => task.IdTask == record.Task_idTask);
                    _recordList.Add(recordViewModel);
                });
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
