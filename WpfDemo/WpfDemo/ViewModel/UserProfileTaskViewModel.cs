using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
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
        private UserProfileTaskView _view;
        private bool _isChanged = false;
        private bool _isTitleChanged = false;
        private bool _isDescriptionChanged = false;
        private bool _isDeadlineChanged = false;

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
                return Enum.GetValues(typeof(TaskStatus)).Cast<TaskStatus>()
                .Where(item => item == TaskStatus.Created)
                .ToDictionary<TaskStatus, TaskStatus, string>(
                item => item,
                item => item.ToString());
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


        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();
        public string Error { get { return null; } }

        public string this[string propertyName]
        {
            get
            {
                string result = null;

                switch (propertyName)
                {
                    case nameof(Title):
                        result = TaskValidationHelper.ValidateTitle(_task.Title);
                        break;

                    case nameof(Deadline):
                        result = TaskValidationHelper.ValidateDeadline(_task.Deadline);
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
        private void ExitWindow(object obj)
        {
            this._view.Close();
        }


        private bool CanSave(object arg)
        {
            return !string.IsNullOrEmpty(CurrentTask.Title) && !string.IsNullOrEmpty(CurrentTask.Deadline.ToString()) && _isChanged;
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
            this._task.IdTask = new TaskRepository(new TaskLogic()).CreateTask(this._task, this._task.User.IdUser);
            MessageBox.Show(ResourceHandler.GetResourceString("TaskCreatedMessage"), ResourceHandler.GetResourceString("Information"), MessageBoxButton.OK, MessageBoxImage.Information);

            if (this._task.User.Status != 1)
            {
                new NotificationRepository(new NotificationLogic()).CreateNotificationForTask("New task!", this._task.IdTask);
            }
            RefreshValues();
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

        public void RefreshValues()
        {
            this.IdTask = 0;
            this.Title = "";
            this.Description = "";
            this.Deadline = DateTime.Today.AddDays(1);
        }
    }
}
