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
using TimeSheet.Resource;
using WpfDemo.ViewModel.Command;

namespace WpfDemo.ViewModel
{
    public class RecordViewModel : ViewModelBase, IDataErrorInfo
    {
        private Record _record;
        private Task _task;
        private User _user;
        private bool _isTaskChanged = false;
        private bool _isDateChanged = false;
        private bool _isCommentChanged = false;
        private bool _isDurationChanged = false;


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
                _isDateChanged = true;
                OnPropertyChanged(nameof(DateErrorIconVisibility));
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
                _isCommentChanged = true;
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
                _isDurationChanged = true;
                OnPropertyChanged(nameof(DurationErrorIconVisibility));
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
                    _isDurationChanged = true;
                    OnPropertyChanged(nameof(DurationErrorIconVisibility));
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
                try
                {
                    _user = new UserRepository(new UserLogic()).GetUserByID(_task.User_idUser);//_record.User_idUser
                }
                catch (SqlException)
                {
                    MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                return _user;
            }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

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
                if (_record.IdRecord == 0)  // kell ez mert kulonbon update eseten a Save gomb nem lennne disabled-d
                {
                    _isTaskChanged = true;
                }
                OnPropertyChanged(nameof(TaskErrorIconVisibility));
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

        public bool IsSelectedRecordValuesChanged // OnRecordCanceled-hez h a benne levo try-catch csak akkor fusson le ha valtoztak az adatok
        {
            get
            {
                return _isTaskChanged || _isDateChanged || _isCommentChanged || _isDurationChanged ? true : false;
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
                /*try
                {
                    _user = new UserRepository(new UserLogic()).GetUserByID(_task.User_idUser);
                }
                catch (SqlException)
                {
                    MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                return _record != null && _record.IdRecord != 0 && _user.Username != LoginViewModel.LoggedUser.Username ? true : false;*/
                if (_user != null)
                {
                    if (_user.Username != LoginViewModel.LoggedUser.Username && _record.IdRecord != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        public string RecordViewDurationCommentBackground
        {
            get
            {
                /*try
                {
                    _user = new UserRepository(new UserLogic()).GetUserByID(_task.User_idUser);
                }
                catch (SqlException)
                {
                    MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                return _record != null && _record.IdRecord != 0 && _user.Username != LoginViewModel.LoggedUser.Username ? "#FFC7C7C7" : "#eee";*/
                if (_user != null)
                {
                    if (_user.Username != LoginViewModel.LoggedUser.Username && _record.IdRecord != 0)
                    {
                        return "#FFC7C7C7";
                    }
                    else
                    {
                        return "#eee";
                    }
                }
                else
                {
                    return "#eee";
                }
            }
        }

        public Visibility RecordViewTaskComboBoxVisibility
        {
            get
            {
                return _record != null && _record.IdRecord == 0 ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility RecordViewTaskTextboxVisibility
        {
            get
            {
                return _record != null && _record.IdRecord != 0 ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility RecordViewDateDatePickerVisibility
        {
            get
            {
                /*try
                {                 
                    _user = new UserRepository(new UserLogic()).GetUserByID(_task.User_idUser);
                }
                catch (SqlException)
                {
                    MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                return _record != null && _record.IdRecord != 0 && _user.Username != LoginViewModel.LoggedUser.Username ? Visibility.Collapsed : Visibility.Visible;*/
                if (_user != null)
                {
                    if (_user.Username != LoginViewModel.LoggedUser.Username && _record.IdRecord != 0)
                    {
                        return Visibility.Collapsed;
                    }
                    else
                    {
                        return Visibility.Visible;
                    }
                }
                else
                {
                    return Visibility.Visible;
                }

            }
        }

        public Visibility RecordViewDateTextBoxVisibility
        {
            get
            {
                /*try
                {
                    _user = new UserRepository(new UserLogic()).GetUserByID(_task.User_idUser);
                }
                catch (SqlException)
                {
                    MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                return _record != null && _record.IdRecord != 0 && _user.Username != LoginViewModel.LoggedUser.Username ? Visibility.Visible : Visibility.Collapsed;*/
                if (_user != null)
                {
                    if (_user.Username != LoginViewModel.LoggedUser.Username && _record.IdRecord != 0)
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

        public Visibility RecordViewButtonsVisibility
        {
            get
            {
                /*try
                {
                    _user = new UserRepository(new UserLogic()).GetUserByID(_task.User_idUser);
                }
                catch (SqlException)
                {
                    MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                return _record != null && _record.IdRecord != 0 && _user.Username != LoginViewModel.LoggedUser.Username ? Visibility.Collapsed : Visibility.Visible;*/
                if (_user != null)
                {
                    if (_user.Username != LoginViewModel.LoggedUser.Username && _record.IdRecord != 0)
                    {
                        return Visibility.Collapsed;
                    }
                    else
                    {
                        return Visibility.Visible;
                    }
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }


        public Visibility TaskErrorIconVisibility
        {
            get
            {
                return RecordValidationHelper.ValidateTask(_task) == null || !_isTaskChanged ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public Visibility DateErrorIconVisibility
        {
            get
            {
                return RecordValidationHelper.ValidateDate(_record.Date, _task == null ? DateTime.MinValue : _task.CreationDate) == null || !_isDateChanged 
                       ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public Visibility DurationErrorIconVisibility
        {
            get
            {
                return RecordValidationHelper.ValidateDuration(_record.Duration) == null || !_isDurationChanged ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();
        public string Error { get { return null; } }

        public string this[string propertyName]
        {
            get
            {
                string result = null;

                if (_isTaskChanged || _isDateChanged || _isDurationChanged)
                {
                    switch (propertyName)
                    {
                        case nameof(Task):
                            result = RecordValidationHelper.ValidateTask(_task);
                            break;

                        case nameof(Date):
                            result = RecordValidationHelper.ValidateDate(_record.Date, _task == null ? DateTime.MinValue : _task.CreationDate);
                            break;

                        case nameof(DurationFormat):
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
                }

                return result;
            }
        }


        public ICommand SaveCommand { get; }
        public RelayCommand CancelRecordViewCommand { get; private set; }

        public RelayCommand DurationAddCommand { get; private set; }
        public RelayCommand DurationReduceCommand { get; private set; }


        public RecordViewModel(Record record, List<Task> activeTasks)
        {
            _record = record;
            ActiveTasks = activeTasks;

            SaveCommand = new RelayCommand(Save, CanSave);
            CancelRecordViewCommand = new RelayCommand(CancelRecordView, CanCancelRecordView);
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
            return Task != null && !string.IsNullOrEmpty(Duration.ToString()) && !string.IsNullOrEmpty(Date.ToString()) &&
                   (_isTaskChanged || _isDateChanged || _isCommentChanged || _isDurationChanged);
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
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
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
            this._record.IdRecord = new RecordRepository(new RecordLogic()).CreateRecord(this._record, this._task.User_idUser, this._task.IdTask);
            MessageBox.Show(Resources.RecordCreatedMessage, Resources.Information, MessageBoxButton.OK, MessageBoxImage.Information);

            IsChangedRecordValuesToFalse();

            CreateRecordToList(this);
        }
        public event Action<RecordViewModel> RecordCreated;
        public void CreateRecordToList(RecordViewModel recordViewModel)
        {
            RecordCreated?.Invoke(recordViewModel);
        }

        private void UpdateRecord()
        {
            new RecordRepository(new RecordLogic()).UpdateRecord(this._record, this._record.IdRecord, this._record.User_idUser, this._task.IdTask);
            MessageBox.Show(Resources.RecordUpdatedMessage, Resources.Information, MessageBoxButton.OK, MessageBoxImage.Information);

            IsChangedRecordValuesToFalse();
        }

        public void IsChangedRecordValuesToFalse() // Save gomb enable-se miatt
        {
            _isTaskChanged = false;
            _isDateChanged = false;
            _isCommentChanged = false;
            _isDurationChanged = false;
        }


        public event Action<object> RecordCanceled;
        public void CancelRecord(Object obj)
        {
            RecordCanceled?.Invoke(obj);
        }
        private bool CanCancelRecordView(object arg)
        {
            return true;
        }

        private void CancelRecordView(object obj)
        {
            CancelRecord(obj);
            IsChangedRecordValuesToFalse();
        }
    }
}
