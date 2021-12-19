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
    public class UpdateTaskViewModel : ViewModelBase, IDataErrorInfo
    {
        private Task _task; 
        private UpdateTask _view;
        private bool _isChanged = false;

 
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
        public DateTime Deadline // DataErrorhoz csinaltam(de nem mukodik)
        {
            get
            {
                return _task.Deadline;
            }
            set
            {
                _task.Deadline = value;
                OnPropertyChanged(nameof(Deadline));
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
                .ToDictionary<TaskStatus, TaskStatus, string>(
                item => item,
                item => item.ToString());
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


        public RelayCommand UpdateTaskCommand { get; private set; }

        public RelayCommand ExitWindowCommand { get; private set; }

        public UpdateTaskViewModel(UpdateTask view)
        {
            _view = view;

            UpdateTaskCommand = new RelayCommand(UpdateTask, CanExecuteUpdate);
            ExitWindowCommand = new RelayCommand(ExitWindow, CanExecuteExit);
        }


        private bool CanExecuteExit(object arg)
        {
            return true;
        }
        private void ExitWindow(object obj)
        {
            _view.Close();
        }


        private bool CanExecuteUpdate(object arg)
        {
            return !string.IsNullOrEmpty(CurrentTask.Title) && !string.IsNullOrEmpty(CurrentTask.Description) && !string.IsNullOrEmpty(CurrentTask.Deadline.ToString());
        }

        private void UpdateTask(object obj)
        {
            try
            {
                new TaskRepository(new TaskLogic()).UpdateTask(CurrentTask, CurrentTask.IdTask, CurrentTask.User_idUser);
                MessageBox.Show("Task has been updated succesfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                _view.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Server error!");
            }
            catch (TaskValidationException)
            {

            }
        }
    }
}
