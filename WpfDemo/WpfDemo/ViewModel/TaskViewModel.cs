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
using WpfDemo.ViewModel.Command;


namespace WpfDemo.ViewModel
{
    public class TaskViewModel : ViewModelBase, IDataErrorInfo
    {
        private Task _task;
        private bool _isChanged = false;


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

        public bool IsUserEnabled
        {
            get
            {
                return _task.IdTask == 0;
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

            SaveCommand = new RelayCommand(Save, CanSave);
        }


        private bool CanSave(object arg)
        {
            return User != null && !string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Deadline.ToString()) && _isChanged == true;
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
            RefreshValues();
        }


        private void UpdateTask()
        {
            new TaskRepository(new TaskLogic()).UpdateTask(this._task, this._task.IdTask, this._task.User_idUser);
            MessageBox.Show("Task has been updated succesfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            _isChanged = false;
        }


        private void RefreshValues()
        {
            this.IdTask = 0;
            this.User = null;
            this.Title = "";
            this.Description = "";
            this.Deadline = DateTime.Today.AddDays(1);
        }
    }
}
