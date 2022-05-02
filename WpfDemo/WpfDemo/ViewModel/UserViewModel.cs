using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
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
    public class UserViewModel : ViewModelBase, IDataErrorInfo
    {
        private User _user;
        private bool _isUsernameChanged = false;
        private bool _isEmailChanged = false;

        public int IdUser
        {
            get
            {
                return _user.IdUser;
            }
            set
            {
                _user.IdUser = value;
                OnPropertyChanged(nameof(IdUser));
            }
        }

        public string Username // Felhasznalonev bindolashoz
        {
            get
            {
                return _user.Username;
            }
            set
            {
                _user.Username = value;
                OnPropertyChanged(nameof(Username));
                _isUsernameChanged = true;
                OnPropertyChanged(nameof(UsernameErrorIconVisibility));
            }
        }
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
            }
        }

        public string LastName // Vezeteknev bindolashoz
        {
            get
            {
                return _user.LastName;
            }
            set
            {
                _user.LastName = value;
                OnPropertyChanged(nameof(LastName));
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
                return _user.Telephone;
            }
            set
            {
                _user.Telephone = value;
                OnPropertyChanged(nameof(Telephone));
            }
        }

        public int Status
        {
            get
            {
                return _user.Status;
            }
            set
            {
                _user.IdUser = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public User User // UserProfile miatt
        {
            get
            {
                return _user;
            }
            /*set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }*/
        }

        public bool IsUserViewValuesReadOnly // ha letezo Felhasznalora kattintunk akkor ReadOnlykent jelennek meg az adatai
        {
            get
            {
                return _user.IdUser != 0;
            }
        }

        public bool IsUserViewValuesEnabled // mikor a fonok ujat csinalni tudjon tab-bal lepegetni Username es Email kozott
        {
            get
            {
                return _user.IdUser != 0;
            }
        }

        public string UserViewHeight // Uj Felhasznalo letrehozasa eseten csak a Felhasznalonevet es Emailt adjuk meg igy oda eleg a kisebb magassag
        {
            get
            {
                return _user != null && _user.IdUser != 0 ? "664" : "200";
            }
        }

        public string UserViewRowHeight // Uj Felhaszno letrehozasa eseten a sor magassagat nullazuk
        {
            get
            {
                return _user != null && _user.IdUser != 0 ? "*" : "0";
            }
        }

        public string UserViewEmailGridRow // Uj Felhaszno letrehozasa eseten az Email soranak a nagysagat csokkentjuk
        {
            get
            {
                return _user != null && _user.IdUser != 0 ? "3" : "1";
            }
        }

        public string UserViewFirstNameGridRow // Uj Felhaszno letrehozasa eseten az Keresztnev soranak a nagysagat noveljuk
        {
            get
            {
                return _user != null && _user.IdUser != 0 ? "1" : "3";
            }
        }

        public string UserViewValuesBackground // Uj Felhaszno letrehozasa eseten az Email soranak a magassagat csokkentjuk
        {
            get
            {
                return _user.IdUser != 0 ? "transparent" : "#eee";
            }
        }

        public string UserViewValuesBorderThickness // Uj Felhaszno letrehozasa eseten a szegelyek szelessege no
        {
            get
            {
                return _user.IdUser != 0 ? "0" : "1";
            }
        }

        public string UserViewEmailWidth // Uj Felhaszno letrehozasa eseten az Email TextBoxanak a hossz csokkent (ErrorIcon miatt)
        {
            get
            {
                return _user != null && _user.IdUser != 0 ? "290" : "250";
            }
        }

        public Visibility UserViewSaveButtonVisibility // A mentes gomb csak uj Felhasznalo letrehozasa eseten latszik
        {
            get
            {
                return _user != null && _user.IdUser != 0 ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility UserViewUsernameVisibility // A Username kiiras uj Felhasznalo eseten latszik kulonben meg Hidden
        {
            get
            {
                return _user != null && _user.IdUser == 0 ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public Visibility UserViewUsernameWithColonVisibility // A Username: kiiras eletezo Felhasznalo eseten latszik kulonben meg Hidden
        {
            get
            {
                return _user != null &&  _user.IdUser != 0 ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public Visibility UserViewEmailVisibility // Az Email kiiras uj Felhasznalo eseten latszik kulonben meg Hidden
        {
            get
            {
                return _user != null && _user.IdUser == 0 ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public Visibility UserViewEmailWithColonVisibility // Az Email: kiiras eletezo Felhasznalo eseten latszik kulonben meg Hidden
        {
            get
            {
                return _user != null && _user.IdUser != 0 ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public Visibility UsernameErrorIconVisibility // Ha a Felhasznalonev Exceptiont kap akkor az ErrorIcon megjelenik
        {
            get
            {
                return UserValidationHelper.ValidateUserName(_user.Username) == null || !_isUsernameChanged ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility EmailErrorIconVisibility // Ha az Email Exceptiont kap akkor az ErrorIcon megjelenik
        {
            get
            {
                return UserValidationHelper.ValidateEmail(_user.Email) == null || !_isEmailChanged ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility ListUsersViewContextMenuVisibility // (Delete Header Visibility) Csak Admin eseteben jelenik meg jobb klikkre egy torles lehetoseg
        {
            get
            {
                return LoginViewModel.LoggedUser.Status == 0 || _user.Username == LoginViewModel.LoggedUser.Username ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public bool IsListUsersViewContextMenuOpen // (Delete Header Visibility) Csak Admin eseteben jelenik meg jobb klikkre egy torles lehetoseg es sajat magat nem tudja torolni
        {
            get
            {
                return LoginViewModel.LoggedUser.Status == 0 || _user.Username == LoginViewModel.LoggedUser.Username ? false : true;
            }
        }


        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();
        public string Error { get { return null; } } // IDataErrorInfo-hez kell

        public string this[string propertyName]
        {
            get
            {
                string result = null;

                if (_isUsernameChanged || _isEmailChanged)
                {
                    switch (propertyName)
                    {
                        case nameof(Username):
                            result = UserValidationHelper.ValidateUserName(_user.Username);
                            break;

                        case nameof(Email):
                            result = UserValidationHelper.ValidateEmail(_user.Email);
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
        public RelayCommand CancelUserViewCommand { get; private set; }


        public UserViewModel(User user)
        {
            _user = user;

            SaveCommand = new RelayCommand(Save, CanSave);
            CancelUserViewCommand = new RelayCommand(CancelUserView, CanCancelUserView);
        }

        private bool CanSave(object arg) // mentheto amig nem nullak az ertekek
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Email);
        }

        private void Save(object obj)
        {
            try
            {
                CreateUser();
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (UserValidationException)
            {

            }
        }

        private void CreateUser() // Letrehozza az uj Felhasznalot
        {
            string createdUserRandomPassword = RandomPassword(10); // Letrehozz egy random jelszot(ami 10 karakter hosszusagu)
            this._user.IdUser = new UserRepository(new UserLogic()).CreateUser(this._user, createdUserRandomPassword);

            CreateUserToList(this); // hozzaadja a listahoz

            SendEmail(createdUserRandomPassword); // elkuldi az emailt a Felhasznalonevvel es a random Jelszoval
        }

        public event Action<UserViewModel> UserCreated;
        public void CreateUserToList(UserViewModel userViewModel)
        {
            UserCreated?.Invoke(userViewModel);
        }

        private void SendEmail(string createdUserRandomPassword)
        {
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            //client.Timeout = 10;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("wpfszakdoga@gmail.com", "Marszu99");
            string EmailSubject = "Registration";
            string EmailMessage = "Welcome to Worktime Registry!\n\n" +
                                  "Your profile's data:" +
                                  "\n\t\t\t\t\t\t\t\tUsername: " + this.Username +
                                  "\n\t\t\t\t\t\t\t\tPassword: " + createdUserRandomPassword;
            MailMessage mm = new MailMessage("wpfszakdoga@gmail.com", this.Email, EmailSubject, EmailMessage);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            client.Send(mm);
        }
        public static string RandomPassword(int length)
        {
            Random random = new Random();
            const string chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }


        public event Action<object> UserCanceled;
        public void CancelUser(Object obj)
        {
            UserCanceled?.Invoke(obj);
        }
        private bool CanCancelUserView(object arg)
        {
            return true;
        }

        private void CancelUserView(object obj)
        {
            CancelUser(obj); // Eltunteti a jelenlegi Felhasznalot
        }
    }
}
