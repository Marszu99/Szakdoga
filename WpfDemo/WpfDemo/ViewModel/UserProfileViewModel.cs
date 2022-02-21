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
        private User _user;
        private Task _selectedTask;

        private ObservableCollection<RecordViewModel> _recordList = new ObservableCollection<RecordViewModel>();
        private ObservableCollection<Task> _taskList = new ObservableCollection<Task>();

        public ObservableCollection<RecordViewModel> RecordList
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

        private int _currentUserIdUser;
        public int CurrentUserIdUser
        {
            get
            {
                return _currentUserIdUser;
            }
            set
            {
                _currentUserIdUser = value;
                OnPropertyChanged(nameof(CurrentUserIdUser));
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


        private string _searchTaskListValue;
        public string SearchTaskListValue
        {
            get { return _searchTaskListValue; }
            set
            {
                _searchTaskListValue = value;
                OnPropertyChanged(nameof(SearchTaskListValue));
                if (String.IsNullOrWhiteSpace(_searchTaskListValue))
                {
                    LoadTasks(CurrentUserIdUser);
                }
                else
                {
                    _taskList.Clear();

                    try
                    {
                        var tasks = new TaskRepository(new TaskLogic()).GetUserTasks(CurrentUserIdUser).Where(task => task.Title.Contains(_searchTaskListValue) || task.Description.Contains(_searchTaskListValue) || task.Deadline.ToShortDateString().Contains(_searchTaskListValue) || task.Status.ToString().Contains(_searchTaskListValue)).ToList();
                        tasks.ForEach(task => _taskList.Add(task));
                    }
                    catch (SqlException)
                    {
                        MessageBox.Show(Resources.ServerError);
                    }
                }
            }
        }

        private string _searchRecordListValue;
        public string SearchRecordListValue
        {
            get { return _searchRecordListValue; }
            set
            {
                _searchRecordListValue = value;
                OnPropertyChanged(nameof(SearchRecordListValue));
                if (String.IsNullOrWhiteSpace(_searchRecordListValue))
                {
                    LoadRecords(CurrentUserIdUser);
                }
                else
                {
                    _recordList.Clear();

                    try
                    {

                        var records = new RecordRepository(new RecordLogic()).GetUserRecords(CurrentUserIdUser).Where(record => record.Task_Title.Contains(_searchRecordListValue)
                        || record.User_Username.Contains(_searchRecordListValue) || record.Date.ToShortDateString().Contains(_searchRecordListValue)
                        || record.Comment.Contains(_searchRecordListValue) || record.Duration.ToString().Contains(_searchRecordListValue)
                        || record.Task_Status.ToString().Contains(_searchRecordListValue)).ToList();
                        records.ForEach(record =>
                        {
                            var recordViewModel = new RecordViewModel(record, TaskList.ToList());
                            recordViewModel.Task = TaskList.First(task => task.IdTask == record.Task_idTask);
                            RecordList.Add(recordViewModel);

                            //recordViewModel.RecordCreated += OnRecordCreated;
                        });
                    }
                    catch (SqlException)
                    {
                        MessageBox.Show(Resources.ServerError);
                    }
                }
            }
        }

        public Visibility AddTaskButtonVisibility
        {
            get
            {
                return LoginViewModel.LoggedUser.Status == 0 ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public Visibility UserProfileViewTasksContextMenuVisibility // Delete Header Visibility
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
            _currentUserIdUser = userid;
            LoadTasks(userid);
            LoadRecords(userid);
            
            DeleteCommand = new RelayCommand(DeleteTask, CanDeleteTask);
            ShowTaskCommand = new RelayCommand(ShowTask, CanShowTask);
        }

        private bool CanShowTask(object arg)
        {
            return LoginViewModel.LoggedUser.Status != 0;
        }

        private void ShowTask(object obj)
        {
            if(_selectedTask == null)
            {
                UserProfileTaskView Ipage = new UserProfileTaskView();
                (Ipage.DataContext as UserProfileTaskViewModel).CurrentTask.User_idUser = CurrentUser.IdUser;
                (Ipage.DataContext as UserProfileTaskViewModel).CurrentTask.User_Username = CurrentUser.Username;
                (Ipage.DataContext as UserProfileTaskViewModel).CurrentTask.Deadline = DateTime.Today.AddDays(1);
                //(Ipage.DataContext as UserProfileTaskViewModel).CurrentUser = CurrentUser;
                Ipage.ShowDialog();
            }
            else
            {
                UserProfileTaskView Ipage = new UserProfileTaskView();
                (Ipage.DataContext as UserProfileTaskViewModel).CurrentTask = SelectedTask;
                //(Ipage.DataContext as UserProfileTaskViewModel).CurrentUser = CurrentUser;
                Ipage.ShowDialog();
            }

            LoadTasks(CurrentUser.IdUser);
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

                    if (this.SelectedTask.User_Username != LoginViewModel.LoggedUser.Username)
                    {
                        SendNotificationEmail(SelectedTask.Title);
                    }
                    LoadTasks(SelectedTask.User_idUser);
                }
                catch (SqlException)
                {
                    MessageBox.Show(Resources.ServerError);
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
            MailMessage mm = new MailMessage("wpfszakdoga@gmail.com", SelectedTask.User.Email, EmailSubject, EmailMessage);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            client.Send(mm);
        }

        private void LoadRecords(int userid)
        {
            _recordList.Clear();

            var records = new RecordRepository(new RecordLogic()).GetUserRecords(CurrentUserIdUser);
            records.ForEach(record =>
            {
                var recordViewModel = new RecordViewModel(record, TaskList.ToList());
                recordViewModel.Task = TaskList.First(task => task.IdTask == record.Task_idTask);
                RecordList.Add(recordViewModel);

            });
        }
        private void LoadTasks(int userid)
        {
            _taskList.Clear();

            var tasks = new TaskRepository(new TaskLogic()).GetUserTasks(userid);
            tasks.ForEach(task => _taskList.Add(task));
        }

        public string UsernameString
        {
            get
            {
                return Resources.Username;
            }
        }
        public string FirstNameString
        {
            get
            {
                return Resources.FirstName;
            }
        }
        public string LastNameString
        {
            get
            {
                return Resources.LastName;
            }
        }
        public string EmailString
        {
            get
            {
                return Resources.Email;
            }
        }
        public string TelephoneString
        {
            get
            {
                return Resources.Telephone;
            }
        }
        public string TasksString
        {
            get
            {
                return Resources.Tasks;
            }
        }
        public string TitleString
        {
            get
            {
                return Resources.Title;
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
        public string NewTaskString
        {
            get
            {
                return Resources.NewTask;
            }
        }
        public string DeleteString
        {
            get
            {
                return Resources.Delete;
            }
        }
        public string RecordsString
        {
            get
            {
                return Resources.Records;
            }
        }
        public string TaskString
        {
            get
            {
                return Resources.Tasks;
            }
        }
        public string DateString
        {
            get
            {
                return Resources.Date;
            }
        }
        public string CommentString
        {
            get
            {
                return Resources.Comment;
            }
        }
        public string DurationString
        {
            get
            {
                return Resources.Duration;
            }
        }
        public string SearchString
        {
            get
            {
                return Resources.Search;
            }
        }
    }
}
