using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Windows;
using TimeSheet.DataAccess;
using TimeSheet.Logic;
using TimeSheet.Model;
using TimeSheet.Model.Extension;
using TimeSheet.Resource;
using WpfDemo.ViewModel.Command;

namespace WpfDemo.ViewModel
{
    public class MyProfileViewModel : ViewModelBase, IDataErrorInfo//IEditableObject,IRevertibleChangeTracking 
    {

        private User _user;
        private bool _isPasswordChanged = false;
        private bool _isFirstNameChanged = false;
        private bool _isLastNameChanged = false;
        private bool _isEmailChanged = false;
        private bool _isTelephoneChanged = false;
        private string _myProfileViewUserValuesBackground = "DarkGray";
        private string _myProfileViewUserValuesBorderThickness = "0";
        private string _myProfileViewUserValuesBorderBrush = "DarkGray";
        private bool _myProfileViewUserValuesIsReadOnly = true;
        private bool _myProfileViewUserPasswordIsEnabled = false;
        private Visibility _myProfileViewChangeUserValuesButtonVisibility = Visibility.Visible;
        private Visibility _myProfileViewSaveAndCancelButtonsVisibility = Visibility.Hidden;

        private ObservableCollection<Task> _myToDoTaskList = new ObservableCollection<Task>();
        private ObservableCollection<Task> _myDoneTaskList = new ObservableCollection<Task>();

        public ObservableCollection<Task> MyToDoTaskList // Elvegzendo feladatok listajanak a bindolashoz
        {
            get
            {
                return _myToDoTaskList;
            }
        }

        public ObservableCollection<Task> MyDoneTaskList // Elvegzett feladatok listajanak a bindolashoz
        {
            get
            {
                return _myDoneTaskList;
            }
        }


        public User CurrentLoggedUser // jelenlegi Felhasznalo bindolashoz
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(CurrentLoggedUser));
            }
        }

        // IDataErrorInfo miatt csinaltam pluszba a Password,FirstName,stb..
        public string Password // Jelszo bindolashoz
        {
            get
            {
                return _user.Password;
            }
            set
            {
                _user.Password = value;
                OnPropertyChanged(nameof(Password));
                _isPasswordChanged = true;
                OnPropertyChanged(nameof(PasswordErrorIconVisibility));
            }
        }

        public string FirstName // Keresztnev bindolashoz
        {
            get
            {
                return _user.FirstName;
            }
            set
            {
                _user.FirstName = value;
                OnPropertyChanged(nameof(FirstName));
                _isFirstNameChanged = true;
                OnPropertyChanged(nameof(FirstNameErrorIconVisibility));
            }
        }

        public string LastName // Vezeteknev bindolashoz
        {
            get
            {
                return CurrentLoggedUser.LastName;
            }
            set
            {
                CurrentLoggedUser.LastName = value;
                OnPropertyChanged(nameof(LastName));
                _isLastNameChanged = true;
                OnPropertyChanged(nameof(LastNameErrorIconVisibility));
            }
        }

        public string Email // Email bindolashoz
        {
            get
            {
                return _user.Email;
            }
            set
            {
                _user.Email = value;
                OnPropertyChanged(nameof(Email));
                _isEmailChanged = true;
                OnPropertyChanged(nameof(EmailErrorIconVisibility));
            }
        }

        public string Telephone // Telefon bindolashoz
        {
            get
            {
                return CurrentLoggedUser.Telephone;
            }
            set
            {
                CurrentLoggedUser.Telephone = value;
                OnPropertyChanged(nameof(Telephone));
                _isTelephoneChanged = true;
                OnPropertyChanged(nameof(TelephoneErrorIconVisibility));
            }
        }

        private bool NewUserValuesHasBeenAdded  // megnezi h a bejelentkezett User uj User-e (uj Userkent nincsenek megadva a FirstName,LastName,Telephone adatok)
        {
            get
            {
                return (string.IsNullOrWhiteSpace(CurrentLoggedUser.FirstName) || string.IsNullOrWhiteSpace(CurrentLoggedUser.LastName) 
                        || string.IsNullOrWhiteSpace(CurrentLoggedUser.Telephone)) ? false : true;              
            }
        }
        public string MyProfileViewWindowStyle  // ha uj User lepett be akkor a folso szegelyt eltunteti (hogy ne lephessen ki es megtudja valtoztatni a hianyzo adatokat)
        {
            get
            {               
                return !NewUserValuesHasBeenAdded ? "None" : "SingleBorderWindow";
            }
        }

        public string MyProfileViewUserValuesBackground // Valtoztatom a hatteret ahhoz kepest h az adatok megvaltoztathatoak vagy nem valtoztathatoak meg
        {
            get
            {
                return _myProfileViewUserValuesBackground;
            }
            set
            {
                _myProfileViewUserValuesBackground = value;
                OnPropertyChanged(nameof(MyProfileViewUserValuesBackground));
            }
        }

        public string MyProfileViewUserValuesBorderThickness // Valtoztatom a szegely szelesseget ahhoz kepest h az adatok megvaltoztathatoak vagy nem valtoztathatoak meg
        {
            get
            {
                return _myProfileViewUserValuesBorderThickness;
            }
            set
            {
                _myProfileViewUserValuesBorderThickness = value;
                OnPropertyChanged(nameof(MyProfileViewUserValuesBorderThickness));
            }
        }

        public string MyProfileViewUserValuesBorderBrush // Valtoztatom a szegely szinet ahhoz kepest h az adatok megvaltoztathatoak vagy nem valtoztathatoak meg
        {
            get
            {
                return _myProfileViewUserValuesBorderBrush;
            }
            set
            {
                _myProfileViewUserValuesBorderBrush = value;
                OnPropertyChanged(nameof(MyProfileViewUserValuesBorderBrush));
            }
        }

        public bool MyProfileViewUserValuesIsReadOnly // Valtoztatom a TextBox modosithatosagat ahhoz kepest h az adatok megvaltoztathatoak vagy nem valtoztathatoak meg
        {
            get
            {
                return _myProfileViewUserValuesIsReadOnly;
            }
            set
            {
                _myProfileViewUserValuesIsReadOnly = value;
                OnPropertyChanged(nameof(MyProfileViewUserValuesIsReadOnly));
            }
        }

        public bool MyProfileViewUserPasswordIsEnabled // Valtoztatom a Password modosithatosagat ahhoz kepest h az adatok megvaltoztathatoak vagy nem valtoztathatoak meg
        {
            get
            {
                return _myProfileViewUserPasswordIsEnabled;
            }
            set
            {
                _myProfileViewUserPasswordIsEnabled = value;
                OnPropertyChanged(nameof(MyProfileViewUserPasswordIsEnabled));
            }
        }

        public Visibility MyProfileViewChangeUserValuesButtonVisibility //Valtoztatom a gomb lathatosagat ahhoz kepest h az adatok megvaltoztathatoak vagy nem valtoztathatoak meg
        {
            get
            {
                return _myProfileViewChangeUserValuesButtonVisibility;
            }
            set
            {
                _myProfileViewChangeUserValuesButtonVisibility = value;
                OnPropertyChanged(nameof(MyProfileViewChangeUserValuesButtonVisibility));
            }
        }

        public Visibility MyProfileViewSaveAndCancelButtonsVisibility // Valtoztatom a gombok lathatosagat ahhoz kepest h az adatok megvaltoztathatoak vagy nem valtoztathatoak meg
        {
            get
            {
                return _myProfileViewSaveAndCancelButtonsVisibility;
            }
            set
            {
                _myProfileViewSaveAndCancelButtonsVisibility = value;
                OnPropertyChanged(nameof(MyProfileViewSaveAndCancelButtonsVisibility));
            }
        }

        public Visibility PasswordErrorIconVisibility // Ha a Jelszo Exceptiont kap akkor az ErrorIcon megjelenik
        {
            get
            {
                return UserValidationHelper.ValidatePassword(_user.Password) == null || !_isPasswordChanged ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public Visibility FirstNameErrorIconVisibility // Ha a Keresztnev Exceptiont kap akkor az ErrorIcon megjelenik
        {
            get
            {
                return UserValidationHelper.ValidateFirstName(_user.FirstName) == null || !_isFirstNameChanged ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public Visibility LastNameErrorIconVisibility // Ha a Vezeteknev Exceptiont kap akkor az ErrorIcon megjelenik
        {
            get
            {
                return UserValidationHelper.ValidateLastName(_user.LastName) == null || !_isLastNameChanged ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public Visibility EmailErrorIconVisibility // Ha az Email Exceptiont kap akkor az ErrorIcon megjelenik
        {
            get
            {
                return UserValidationHelper.ValidateEmail(_user.Email) == null || !_isEmailChanged ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public Visibility TelephoneErrorIconVisibility // Ha a Telefon Exceptiont kap akkor az ErrorIcon megjelenik
        {
            get
            {
                return UserValidationHelper.ValidateTelephone(_user.Telephone) == null || !_isTelephoneChanged ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public Visibility MyProfileViewToDoTaskListMessageVisibility // Ha nincs elvegzendo feladata a felhasznalonak akkor azt kiirja a listaba
        {
            get
            {
                return MyToDoTaskList.Count < 1?  Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility MyProfileViewDoneTaskListMessageVisibility // Ha nincs elvegzett feladata a felhasznalonak akkor azt kiirja a listaba
        {
            get
            {
                return MyDoneTaskList.Count < 1 ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>(); // ??
        public string Error { get { return null; } } // IDataError-hoz kell

        public string this[string propertyName] // ??
        {
            get
            {
                string result = null;

                if (_isPasswordChanged || _isFirstNameChanged || _isLastNameChanged || _isEmailChanged || _isTelephoneChanged)
                {
                    switch (propertyName)
                    {
                        case nameof(Password):
                            result = UserValidationHelper.ValidatePassword(_user.Password);
                            break;

                        case nameof(FirstName):
                            result = UserValidationHelper.ValidateFirstName(_user.FirstName);
                            break;

                        case nameof(LastName):
                            result = UserValidationHelper.ValidateLastName(_user.LastName);
                            break;

                        case nameof(Email):
                            result = UserValidationHelper.ValidateEmail(_user.Email);
                            break;

                        case nameof(Telephone):
                            result = UserValidationHelper.ValidateTelephone(_user.Telephone);
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


        public RelayCommand ChangeUserValuesCommand { get; private set; }
        public RelayCommand SaveChangedUserValuesCommand { get; private set; }
        public RelayCommand CancelChangeUserValuesCommand { get; private set; }

        public MyProfileViewModel()
        {
            LoadToDoTasks(); // Betoltom a bejelentkezett elvegzendo feladatait
            LoadDoneTasks(); // Betoltom a bejelentkezett kesz feladatait

            ChangeUserValuesCommand = new RelayCommand(ChangeUserValues, CanExecuteChange);
            SaveChangedUserValuesCommand = new RelayCommand(SaveChangedUserValues, CanExecuteSave);
            CancelChangeUserValuesCommand = new RelayCommand(CancelChangeUserValues, CanExecuteCancel);
        }


        private void LoadToDoTasks()
        {
            _myToDoTaskList.Clear();

            var tasks = new TaskRepository(new TaskLogic()).GetAllActiveTasksFromUser(LoginViewModel.LoggedUser.IdUser);
            tasks.ForEach(task => _myToDoTaskList.Add(task));

            SortTaskListByDeadline(_myToDoTaskList); // Rendezzuk a listat csokkeno sorrendben a Hataridok szerint
        }


        private void LoadDoneTasks()
        {
            _myDoneTaskList.Clear();

            var tasks = new TaskRepository(new TaskLogic()).GetAllDoneTasksFromUser(LoginViewModel.LoggedUser.IdUser);
            tasks.ForEach(task => _myDoneTaskList.Add(task));

            SortTaskListByDeadline(_myDoneTaskList); // Rendezzuk a listat csokkeno sorrendben a Hataridok szerint
        }

        private ObservableCollection<Task> SortTaskListByDeadline(ObservableCollection<Task> TaskList) // Rendezzuk a listat csokkeno sorrendben a Hataridok szerint
        {
            var sortedTaskListByDeadline = TaskList.OrderByDescending(task => DateTime.Parse(task.Deadline.ToString()));

            sortedTaskListByDeadline.ToList().ForEach(task =>
            {
                TaskList.Remove(task);
                TaskList.Add(task);
            });

            return TaskList;
        }


        private bool CanExecuteChange(object arg)
        {
            return true;
        }
        private bool CanExecuteSave(object arg) // ra tud nyomni a "Mentes" gombra ha egyik ertek sem ures es van megvaltozott adat (illetve ha nincs Exception)
        {
            return !string.IsNullOrEmpty(CurrentLoggedUser.Password) && !string.IsNullOrEmpty(CurrentLoggedUser.FirstName) && !string.IsNullOrEmpty(CurrentLoggedUser.LastName)
                   && !string.IsNullOrEmpty(CurrentLoggedUser.Email) && !string.IsNullOrEmpty(CurrentLoggedUser.Telephone) && 
                   (_isPasswordChanged || _isFirstNameChanged || _isLastNameChanged || _isEmailChanged || _isTelephoneChanged);
        }
        private bool CanExecuteCancel(object arg)
        {
            return true;
        }

        private void ChangeUserValues(object obj) // Atalakitom a TextBox designt illetve h modosithatok legyenek, illetve a kello gombokat lathatova teszem es ami nem kell azt meg Hidden-ne
        {
            MyProfileViewUserValuesIsReadOnly = false;
            MyProfileViewUserPasswordIsEnabled = true;
            MyProfileViewUserValuesBackground = "#FFEEEEEE";
            MyProfileViewUserValuesBorderThickness = "1";
            MyProfileViewUserValuesBorderBrush = "Black";
            MyProfileViewChangeUserValuesButtonVisibility = Visibility.Hidden;
            MyProfileViewSaveAndCancelButtonsVisibility = Visibility.Visible;
        }

        private void SaveChangedUserValues(object obj)
        {
            try
            {
                new UserRepository(new UserLogic()).UpdateUser(CurrentLoggedUser);
                SendEmail(); // Kuldok emailt ha esetleg elfelejtene a megadott jelszot vagy elirna...

                // Visszaalakitom a TextBox designt es nem modosíthatova teszem oket, illetve Change gombot lathatova teszem a tobbit Hidden-ne
                MyProfileViewUserValuesIsReadOnly = true;
                MyProfileViewUserPasswordIsEnabled = false;
                MyProfileViewUserValuesBackground = "DarkGray";
                MyProfileViewUserValuesBorderThickness = "0";
                MyProfileViewUserValuesBorderBrush = "DarkGray";
                MyProfileViewChangeUserValuesButtonVisibility = Visibility.Visible;
                MyProfileViewSaveAndCancelButtonsVisibility = Visibility.Hidden;

                IsChangedUservaluesToFalse(); // kell h legkozelebb ha "Change" gombra nyomok akkor ne maradjanak bent a True-s bool ertekek

                OnPropertyChanged(nameof(MyProfileViewWindowStyle)); // kell h megvaltozzon menteskor a folso szegely (uj User eseten lenyegesebb)
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (UserValidationException)
            {

            }
        }
        private void SendEmail()
        {
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            //client.Timeout = 10;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("wpfszakdoga@gmail.com", "Marszu99");
            string EmailSubject = "Profile data changes";
            string EmailMessage = "Succesfully changed your profile's datas!\n\n" +
                                  "Your profile's data:" +
                                  "\n\t\t\t\t\t\t\t\tUsername: " + this._user.Username +
                                  "\n\t\t\t\t\t\t\t\tPassword: " + this._user.Password +
                                  "\n\t\t\t\t\t\t\t\tFirstName: " + this._user.FirstName +
                                  "\n\t\t\t\t\t\t\t\tLastName: " + this._user.LastName +
                                  "\n\t\t\t\t\t\t\t\tEmail: " + this._user.Email +
                                  "\n\t\t\t\t\t\t\t\tTelephone: " + this._user.Telephone;
            MailMessage mm = new MailMessage("wpfszakdoga@gmail.com", this._user.Email, EmailSubject, EmailMessage);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            client.Send(mm);
        }

        private void CancelChangeUserValues(object obj)
        {
            if (_isPasswordChanged || _isFirstNameChanged || _isLastNameChanged || _isEmailChanged || _isTelephoneChanged) // ha megvaltozott valamelyik adat akkor visszatolti az eredeti adatot ha a "Cancel" gombra megyunk
            {
                try
                {
                    User CurrentLoggedUserValues = new UserRepository(new UserLogic()).GetUserByID(CurrentLoggedUser.IdUser);
                    CurrentLoggedUser.Password = CurrentLoggedUserValues.Password;
                    CurrentLoggedUser.FirstName = CurrentLoggedUserValues.FirstName;
                    CurrentLoggedUser.LastName = CurrentLoggedUserValues.LastName;
                    CurrentLoggedUser.Email = CurrentLoggedUserValues.Email;
                    CurrentLoggedUser.Telephone = CurrentLoggedUserValues.Telephone;

                    // Hogy megvaltozzon a kiirt adatok ezert hasznalom a OnPropertyChanged(nameof());
                    OnPropertyChanged(nameof(Password));
                    OnPropertyChanged(nameof(FirstName));
                    OnPropertyChanged(nameof(LastName));
                    OnPropertyChanged(nameof(Email));
                    OnPropertyChanged(nameof(Telephone));

                    IsChangedUservaluesToFalse(); // kell h legkozelebb ha "Change" gombra nyomok akkor ne maradjanak bent a True-s bool ertekek
                }
                catch (SqlException)
                {
                    MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            // Visszaalakitom a TextBox designt es nem modosíthatova teszem oket, illetve "Change" gombot lathatova teszem a tobbit Hidden-ne
            MyProfileViewUserValuesIsReadOnly = true;
            MyProfileViewUserPasswordIsEnabled = false;
            MyProfileViewUserValuesBackground = "DarkGray";
            MyProfileViewUserValuesBorderThickness = "0";
            MyProfileViewUserValuesBorderBrush = "DarkGray";
            MyProfileViewChangeUserValuesButtonVisibility = Visibility.Visible;
            MyProfileViewSaveAndCancelButtonsVisibility = Visibility.Hidden;
        }


        private void IsChangedUservaluesToFalse()
        {
            _isPasswordChanged = false;
            _isFirstNameChanged = false;
            _isLastNameChanged = false;
            _isEmailChanged = false;
            _isTelephoneChanged = false;
        }
    }
}

