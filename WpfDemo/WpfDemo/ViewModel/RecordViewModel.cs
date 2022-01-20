using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using TimeSheet.DataAccess;
using TimeSheet.Logic;
using TimeSheet.Model;
using TimeSheet.Model.Extension;
using WpfDemo.ViewModel.Command;

namespace WpfDemo.ViewModel
{
    public class RecordViewModel : ViewModelBase, IDataErrorInfo
    {
        private Record _record;
        private bool _isChanged = false;


        public Record Record
        {
            get
            {
                return _record;
            }
        }

        public int IdRecord
        {
            get
            {
                return _record.IdRecord;
            }
            set
            {
                _record.IdRecord = value;
                OnPropertyChanged(nameof(IdRecord));
            }
        }

        public DateTime Date
        {
            get
            {
                return _record.Date;
            }
            set
            {
                _record.Date = value;
                OnPropertyChanged(nameof(Date));
                _isChanged = true;
            }
        }
        public string Comment
        {
            get
            {
                return _record.Comment;
            }
            set
            {
                _record.Comment = value;
                OnPropertyChanged(nameof(Comment));
                _isChanged = true;
            }
        }

        public int Duration
        {
            get
            {
                return _record.Duration;
            }
            set
            {
                _record.Duration = value;
                OnPropertyChanged(nameof(Duration));
                OnPropertyChanged(nameof(DurationFormat));
                _isChanged = true;
            }
        }

        public string DurationFormat
        {
            get
            {
                return TimeSpan.FromMinutes(_record.Duration).ToString("hh':'mm");
            }
            set
            {
                try
                {
                    TimeSpan input = TimeSpan.ParseExact(value, "hh':'mm", null);

                    _record.Duration = (int)input.TotalMinutes;
                    OnPropertyChanged(nameof(DurationFormat));
                }
                catch (FormatException)
                {
                    //Do Nothing
                }
            }
        }

        public User User
        {
            get
            {
                return _record.User;
            }
            set
            {
                _record.User = value;
                OnPropertyChanged(nameof(User));
            }
        }

        public Task Task
        {
            get
            {
                return _record.Task;
            }
            set
            {
                _record.Task = value;
                OnPropertyChanged(nameof(Task));
                if (_record.IdRecord == 0)  // kell ez mert kulonbon update eseten a Save gomb nem lennne disabled-d
                {
                    _isChanged = true;
                }
            }
        }

        public List<Task> ActiveTasks
        {
            get;
        }

        public int User_idUser
        {
            get
            {
                return _record.User_idUser;
            }
            set
            {
                _record.User_idUser = value;
                OnPropertyChanged(nameof(User_idUser));
            }
        }

        public string User_Username
        {
            get
            {
                return _record.User_Username;
            }
            set
            {
                _record.User_Username = value;
                OnPropertyChanged(nameof(User_Username));
            }
        }

        public int Task_idTask
        {
            get
            {
                return _record.Task_idTask;
            }
            set
            {
                _record.Task_idTask = value;
                OnPropertyChanged(nameof(Task_idTask));
            }
        }

        public TaskStatus Task_Status
        {
            get
            {
                return _record.Task_Status;
            }
            set
            {
                _record.Task_Status = value;
                OnPropertyChanged(nameof(Task_Status));
            }
        }

        public string Task_Title
        {
            get
            {
                return _record.Task_Title;
            }
            set
            {
                _record.Task_Title = value;
                OnPropertyChanged(nameof(Task_Title));
            }
        }

        public bool IsTaskEnabled
        {
            get
            {
                return _record.IdRecord == 0;
            }
        }

        public bool IsRecordViewValuesReadOnly
        {
            get
            {
                return _record != null && _record.IdRecord != 0 && _record.User_Username != LoginViewModel.LoggedUser.Username ? true : false;
            }
        }

        public bool IsRecordViewDateHitTestVisible
        {
            get
            {
                return _record != null && _record.IdRecord != 0 && _record.User_Username != LoginViewModel.LoggedUser.Username ? false : true;
            }
        }

        public Visibility RecordViewButtonsVisibility
        {
            get
            {
                return _record != null && _record.IdRecord != 0 && _record.User_Username != LoginViewModel.LoggedUser.Username ? Visibility.Hidden : Visibility.Visible;
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
                    case nameof(Task):
                        result = RecordValidationHelper.ValidateTask(_record.Task);
                        break;

                    case nameof(Date):
                        result = RecordValidationHelper.ValidateDate(_record.Date, _record.Task == null ? DateTime.MinValue : _record.Task.CreationDate);
                        break;

                    case nameof(Duration):
                        result = RecordValidationHelper.ValidateDuration(_record.Duration);
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
        public RelayCommand DurationAddCommand { get; private set; }
        public RelayCommand DurationReduceCommand { get; private set; }


        public RecordViewModel(Record record, List<Task> activeTasks)
        {
            _record = record;
            ActiveTasks = activeTasks;

            SaveCommand = new RelayCommand(Save, CanSave);
            DurationAddCommand = new RelayCommand(Add10MinToDuration, CanExecuteAdd);
            DurationReduceCommand = new RelayCommand(Reduce10MinFromDuration, CanExecuteReduce);
        }

        private bool CanExecuteAdd(object arg)
        {
            if (Duration < 720)
            {
                return true;
            }
            else return false;
        }

        private void Add10MinToDuration(object obj)
        {
           Duration += 10;
        }


        private bool CanExecuteReduce(object arg)
        {
            if (Duration > 0)
            {
                return true;
            }
            else return false;
        }

        private void Reduce10MinFromDuration(object obj)
        {
            Duration -= 10;
        }


        private bool CanSave(object arg)
        {
            return Task != null && !string.IsNullOrEmpty(Duration.ToString()) && !string.IsNullOrEmpty(Date.ToString()) && _isChanged;
        }

        private void Save(object obj)
        {
            try
            {
                if (CheckIfNewRecord())
                {
                    CreateRecord();
                }
                else
                {
                    UpdateRecord();
                }
                RecordManagementViewModel.RefreshRecordListCommand.Execute(obj);  // ez igy jo????
            }
            catch (SqlException)
            {
                MessageBox.Show(ResourceHandler.GetResourceString("ServerError"));
            }
            catch (RecordValidationException)
            {

            }
        }

        private bool CheckIfNewRecord()
        {
            return this._record.IdRecord == 0;
        }

        private void CreateRecord()
        {
            this._record.IdRecord = new RecordRepository(new RecordLogic()).CreateRecord(this._record, this._record.Task.User_idUser, this._record.Task.IdTask); 
            MessageBox.Show(ResourceHandler.GetResourceString("RecordCreatedMessage"), ResourceHandler.GetResourceString("Information"), MessageBoxButton.OK, MessageBoxImage.Information);
            RefreshValues();
        }

        private void UpdateRecord()
        {
            new RecordRepository(new RecordLogic()).UpdateRecord(this._record, this._record.IdRecord, this._record.User_idUser, this._record.Task.IdTask);
            MessageBox.Show(ResourceHandler.GetResourceString("RecordUpdatedMessage"), ResourceHandler.GetResourceString("Information"), MessageBoxButton.OK, MessageBoxImage.Information);
            _isChanged = false;
        }

        private void RefreshValues()
        {
            this.IdRecord = 0;
            this.Task = null;
            this.Date = DateTime.Today;
            this.Duration = 210;
            this.Comment = "";
            this.Task_Status = 0;
        }


        public string TaskString
        {
            get
            {
                return ResourceHandler.GetResourceString("Task");
            }
        }
        public string UserString
        {
            get
            {
                return ResourceHandler.GetResourceString("User");
            }
        }
        public string DateString
        {
            get
            {
                return ResourceHandler.GetResourceString("Date");
            }
        }
        public string CommentString
        {
            get
            {
                return ResourceHandler.GetResourceString("Comment");
            }
        }
        public string DurationString
        {
            get
            {
                return ResourceHandler.GetResourceString("Duration");
            }
        }
        public string StatusString
        {
            get
            {
                return ResourceHandler.GetResourceString("Status");
            }
        }
        public string SaveString
        {
            get
            {
                return ResourceHandler.GetResourceString("Save");
            }
        }

    }
}
