using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using TimeSheet.DataAccess;
using TimeSheet.Logic;
using TimeSheet.Model;
using TimeSheet.Model.Extension;
using WpfDemo.View;
using WpfDemo.ViewModel.Command;

namespace WpfDemo.ViewModel
{
    public class UserProfileTaskViewModel : ViewModelBase, IDataErrorInfo
    {
        private Task _task;
        private UserProfileTaskView _view;
        private bool _isChanged = false;

        public Task Task
        {
            get
            {
                return _task;
            }
            set
            {
                _task = value;
                OnPropertyChanged(nameof(Task));
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


        public RelayCommand AddTaskToUserCommand { get; private set; }
        public RelayCommand ExitWindowCommand { get; private set; }

        public UserProfileTaskViewModel(Task task)
        {
            _task = task;
           // _view = view;

            AddTaskToUserCommand = new RelayCommand(AddTaskToUser, CanExecuteAdd);
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


        private bool CanExecuteAdd(object arg)
        {
            return !string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Deadline.ToString()) && _isChanged == true;
        }

        /*private void AddTaskToUser(object obj)
        {
            try
            {
                new TaskRepository(new TaskLogic()).CreateTask(_task, _task.User_idUser);
                MessageBox.Show("Task has been created for user succesfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                Refresh();
            }
            catch (SqlException)
            {
                MessageBox.Show("Server error!");
            }
        }*/


        private void AddTaskToUser(object obj)
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
                MessageBox.Show("Server error!");
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
            MessageBox.Show("Task has been created succesfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            Refresh();
        }


        private void UpdateTask()
        {
            new TaskRepository(new TaskLogic()).UpdateTask(this._task, this._task.IdTask, this._task.User_idUser);
            MessageBox.Show("Task has been updated succesfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            _isChanged = false;
        }

        public void Refresh()
        {
            this.IdTask = 0;
            this.Title = "";
            this.Description = "";
            this.Deadline = DateTime.Today.AddDays(1);
        }
    }
}
