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

        public DateTime Date // Datum bindolashoz
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

        public string Comment // Komment bindaloshoz
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

        public int Duration // Idotartam bindaloshoz
        {
            get
            {
                return _record.Duration;
            }
            set
            {
                _record.Duration = value;
                OnPropertyChanged(nameof(Duration));
                OnPropertyChanged(nameof(DurationFormat)); // Ha valtozik az idotartam akkor frissitse annak a kiirasat is
                _isDurationChanged = true;
                OnPropertyChanged(nameof(DurationErrorIconVisibility));
            }
        }

        public string DurationFormat // Idotartam megfelelo kiirasanak a bindaloshoz
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
                    _user = new UserRepository(new UserLogic()).GetUserByID(_record.User_idUser);//_task.User_idUser??
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
        public string User_Username // Listazashoz kell(Excel miatt mert kulonben ures lesz a mezeje)
        {
            get
            {
                string Username = null;

                try
                {
                    Username = new UserRepository(new UserLogic()).GetUserByID(_record.User_idUser).Username;//_task.User_idUser??
                }
                catch (SqlException)
                {
                    MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                return Username;
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

        public string Task_Title // Listazashoz kell(Excel miatt mert kulonben ures lesz a mezeje)
        {
            get
            {
                return _task.Title;
            }
        }

        public List<Task> ActiveTasks // Kell h mikor uj letrehozasa utan kivalasztunk egy recordot akkor ne legyen ures a Task
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


        public bool IsTaskEnabled // ha record nem uj akkor a Task Disabled lesz
        {
            get
            {
                return _record.IdRecord == 0;
            }
        }

        public bool IsRecordViewValuesReadOnly // Ha az Admin mas Rogziteset nezi akkor azok ertekei ReadOnly-k
        {
            get
            {
                if (User_Username != LoginViewModel.LoggedUser.Username && _record.IdRecord != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public string RecordViewDurationCommentBackground // Ha az Admin mas Rogziteset nezi akkor azok Idotartama es Commentnek a hatterenek a szine megvaltozik
        {
            get
            {
                if (User_Username != LoginViewModel.LoggedUser.Username && _record.IdRecord != 0)
                {
                    return "#FFC7C7C7";
                }
                else
                {
                    return "#eee";
                }
            }
        }

        public Visibility RecordViewTaskComboBoxVisibility // ha uj Rogzites keszul akkor a Combobox Visible egyebkent meg Collapsed
        {
            get
            {
                return _record != null && _record.IdRecord == 0 ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility RecordViewTaskTextboxVisibility // ha nem uj Rogzites keszul akkor TextBoxkent jelenik meg a Task(Title)
        {
            get
            {
                return _record != null && _record.IdRecord != 0 ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility RecordViewDateDatePickerVisibility // Ha az Admin mas Rogziteset nezi akkor azok Datuma nem valtoztathato
        {
            get
            {
                if (User_Username != LoginViewModel.LoggedUser.Username && _record.IdRecord != 0)
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }

        public Visibility RecordViewDateTextBoxVisibility // Ha az Admin mas Rogziteset nezi akkor azok Datuma TextBoxkent jelenik meg
        {
            get
            {
                if (User_Username != LoginViewModel.LoggedUser.Username && _record.IdRecord != 0)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }

        public Visibility RecordViewButtonsVisibility // Ha az Admin mas Rogziteset nezi akkor azoknak a "Cancel" gombon kivul a tobbi gombja nem lathato
        {
            get
            {
                if (User_Username != LoginViewModel.LoggedUser.Username && _record.IdRecord != 0)
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }

        public Visibility TaskErrorIconVisibility // Ha a Feladat Exceptiont kap akkor az ErrorIcon megjelenik
        {
            get
            {
                return RecordValidationHelper.ValidateTask(_task) == null || !_isTaskChanged ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public Visibility DateErrorIconVisibility // Ha a Datum Exceptiont kap akkor az ErrorIcon megjelenik
        {
            get
            {
                return RecordValidationHelper.ValidateDate(_record.Date, _task == null ? DateTime.MinValue : _task.CreationDate) == null || !_isDateChanged 
                       ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public Visibility DurationErrorIconVisibility // Ha az Idotartam Exceptiont kap akkor az ErrorIcon megjelenik
        {
            get
            {
                return RecordValidationHelper.ValidateDuration(_record.Duration) == null || !_isDurationChanged ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public Visibility ListRecordsViewContextMenuVisibility // (Delete Header Visibility) Csak sajat Rogzites eseteben jelenik meg jobb klikkre egy torles lehetoseg
        {
            get
            {
                return User_Username != LoginViewModel.LoggedUser.Username ? Visibility.Collapsed : Visibility.Visible;
            }
        }


        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>(); // ??
        public string Error { get { return null; } } // IDataError-hoz kell

        public string this[string propertyName] // ??
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
            ActiveTasks = activeTasks; // betolti az aktiv feladatokat(amikre lehet Rogzitest csinalni)

            SaveCommand = new RelayCommand(Save, CanSave);
            CancelRecordViewCommand = new RelayCommand(CancelRecordView, CanCancelRecordView);
            DurationAddCommand = new RelayCommand(Add10MinToDuration, CanExecuteAdd);
            DurationReduceCommand = new RelayCommand(Reduce10MinFromDuration, CanExecuteReduce);
        }

        private bool CanExecuteAdd(object arg) // amig nem eri el a 12 orat addig elerheto a gomb
        {
            if (Duration < 720)
            {
                return true;
            }
            else return false;
        }

        private void Add10MinToDuration(object obj) // 10 perccel noveled az Idotartamot
        {
           Duration += 10;
        }


        private bool CanExecuteReduce(object arg) // amig nem megy le 0 oraig addig elerheto a gomb
        {
            if (Duration > 0)
            {
                return true;
            }
            else return false;
        }

        private void Reduce10MinFromDuration(object obj) // 10 perccel csokkented az Idotartamot
        {
            Duration -= 10;
        }


        private bool CanSave(object arg) // mentheto amig nem nullak az ertekek(kiveve a Koemment az lehet ures) es amig valamelyik ertek megvaltozott
        {
            return Task != null && !string.IsNullOrEmpty(Duration.ToString()) && !string.IsNullOrEmpty(Date.ToString()) &&
                   (_isTaskChanged || _isDateChanged || _isCommentChanged || _isDurationChanged);
        }

        private void Save(object obj)
        {
            try
            {
                if (CheckIfNewRecord()) // az Id == 0 akkor uj Rogziteskent menti es ad neki Id-t kulonben meg modositja a meglevo Rogzitest
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

        private void CreateRecord() // Letrehozza az uj Rogzitest
        {
            this._record.IdRecord = new RecordRepository(new RecordLogic()).CreateRecord(this._record, this._task.User_idUser, this._task.IdTask);

            IsChangedRecordValuesToFalse(); // kell h legkozelebb ha "Save" gombra nyomok akkor ne maradjanak bent a True-s bool ertekek

            CreateRecordToList(this); // hozzaadja a listahoz
        }
        public event Action<RecordViewModel> RecordCreated;
        public void CreateRecordToList(RecordViewModel recordViewModel)
        {
            RecordCreated?.Invoke(recordViewModel);
        }

        private void UpdateRecord() // Modositja a Rogzitest
        {
            new RecordRepository(new RecordLogic()).UpdateRecord(this._record, this._record.IdRecord, this._record.User_idUser, this._task.IdTask);

            IsChangedRecordValuesToFalse(); // kell h legkozelebb ha "Save" gombra nyomok akkor ne maradjanak bent a True-s bool ertekek

            UpdateRecordToList(this); // Modositja a keresesi ertekeket ha kell
        }
        public event Action<RecordViewModel> RecordUpdated;
        public void UpdateRecordToList(RecordViewModel recordViewModel)
        {
            RecordUpdated?.Invoke(recordViewModel);
        }


        public void IsChangedRecordValuesToFalse() // "Save" gomb disable-se miatt
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
            CancelRecord(obj); // Eltunteti a jelenlegi Rogzitest
            IsChangedRecordValuesToFalse(); // kell h ha megvaltoztattam az ertekeket de a "Cancel" gombra nyomtam igy False-ra allitom a valtoztatasokat(tehat disable lesz a "Save" gomb)
        }
    }
}
