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
        private Task _selectedTask;
        private ObservableCollection<Task> _taskList = new ObservableCollection<Task>();
        private ObservableCollection<RecordViewModel> _recordList = new ObservableCollection<RecordViewModel>();


        public ObservableCollection<Task> TaskList
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
                                      || new TaskRepository(new TaskLogic()).GetTaskByID(record.Task_idTask).Title.Contains(_searchRecordListValue)
                                      || new TaskRepository(new TaskLogic()).GetTaskByID(record.Task_idTask).Status.ToString().Contains(_searchRecordListValue)).ToList();

                        records.ForEach(record =>
                        {
                            var recordViewModel = new RecordViewModel(record, _taskList.ToList());
                            recordViewModel.Task = _taskList.First(task => task.IdTask == record.Task_idTask);
                            _recordList.Add(recordViewModel);
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
                (Ipage.DataContext as UserProfileTaskViewModel).CurrentTask.Deadline = DateTime.Today.AddDays(1);
                //(Ipage.DataContext as UserProfileTaskViewModel).CurrentUser = CurrentUser;
                Ipage.ShowDialog();
                
                //OnTaskCreated(SelectedTask);
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

                    if (this.CurrentUser.Username != LoginViewModel.LoggedUser.Username)
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
            MailMessage mm = new MailMessage("wpfszakdoga@gmail.com", CurrentUser.Email, EmailSubject, EmailMessage);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            client.Send(mm);
        }


        private void LoadTasks(int userid)
        {
            _taskList.Clear();

            var tasks = new TaskRepository(new TaskLogic()).GetUserTasks(userid);
            tasks.ForEach(task => _taskList.Add(task));
        }

        private void LoadRecords(int userid)
        {
            _recordList.Clear();

            var records = new RecordRepository(new RecordLogic()).GetUserRecords(userid);
            records.ForEach(record =>
            {
                var recordViewModel = new RecordViewModel(record, _taskList.ToList());
                recordViewModel.Task = _taskList.First(task => task.IdTask == record.Task_idTask);
                _recordList.Add(recordViewModel);
            });
        }
    }
}
