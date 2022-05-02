using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows;
using TimeSheet.DataAccess;
using TimeSheet.Logic;
using TimeSheet.Model;
using TimeSheet.Model.Extension;
using WpfDemo.View;
using WpfDemo.ViewModel.Command;
using System.Net.Mail;
using System.Text;
using TimeSheet.Resource;

namespace WpfDemo.ViewModel
{
    public class RegisterViewModel : ViewModelBase, IDataErrorInfo
    {

        private User _user;
        private RegisterView _view;
        private bool _isUsernameChanged = false;
        private bool _isPasswordChanged = false;
        private bool _isPassword2Changed = false;
        private bool _isFirstNameChanged = false;
        private bool _isLastNameChanged = false;
        private bool _isEmailChanged = false;
        private bool _isEmail2Changed = false;
        private bool _isTelephoneChanged = false;
        private bool _isCompanyNameChanged = false;
        private bool _isCompanyName2Changed = false;

        public User User
        {
            get
            {
                return _user;
            }
        }

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
                _isPasswordChanged = true;
                OnPropertyChanged(nameof(PasswordErrorIconVisibility));
            }
        }

        private string _password2;
        public string Password2 // Jelszo2 bindolashoz
        {
            get
            {
                return _password2;
            }
            set
            {
                _password2 = value;
                OnPropertyChanged(nameof(Password2));
                _isPassword2Changed = true;
                OnPropertyChanged(nameof(Password2ErrorIconVisibility));
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
                return _user.LastName;
            }
            set
            {
                _user.LastName = value;
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

        private string _email2;
        public string Email2 // Email2 bindolashoz
        {
            get
            {
                return _email2;
            }
            set
            {
                _email2 = value;
                OnPropertyChanged(nameof(Email2));
                _isEmail2Changed = true;
                OnPropertyChanged(nameof(Email2ErrorIconVisibility));
            }
        }

        public string Telephone // Telephone bindolashoz
        {
            get
            {
                return _user.Telephone;
            }
            set
            {
                _user.Telephone = value;
                OnPropertyChanged(nameof(Telephone));
                _isTelephoneChanged = true;
                OnPropertyChanged(nameof(TelephoneErrorIconVisibility));
            }
        }

        private string _companyName;
        public string CompanyName // Cegnev bindolashoz
        {
            get
            {
                return _companyName;
            }
            set
            {
                _companyName = value;
                OnPropertyChanged(nameof(CompanyName));
                _isCompanyNameChanged = true;
                OnPropertyChanged(nameof(CompanyNameErrorIconVisibility));
            }
        }

        private string _companyName2;
        public string CompanyName2 // Cegnev2 bindolashoz
        {
            get
            {
                return _companyName2;
            }
            set
            {
                _companyName2 = value;
                OnPropertyChanged(nameof(CompanyName2));
                _isCompanyName2Changed = true;
                OnPropertyChanged(nameof(CompanyName2ErrorIconVisibility));
            }
        }


        public Visibility UsernameErrorIconVisibility // Ha a Felhasznalonev Exceptiont kap akkor az ErrorIcon megjelenik
        {
            get
            {
                return UserValidationHelper.ValidateUserName(_user.Username) == null || !_isUsernameChanged ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public Visibility PasswordErrorIconVisibility // Ha a Jelszo Exceptiont kap akkor az ErrorIcon megjelenik
        {
            get
            {
                return UserValidationHelper.ValidatePassword(_user.Password) == null || !_isPasswordChanged ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public Visibility Password2ErrorIconVisibility // Ha a Jelszo2 Exceptiont kap akkor az ErrorIcon megjelenik
        {
            get
            {
                return UserValidationHelper.ValidatePassword2(_user.Password, _password2) == null || !_isPassword2Changed ? Visibility.Hidden : Visibility.Visible;
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

        public Visibility EmailErrorIconVisibility // Ha a Email Exceptiont kap akkor az ErrorIcon megjelenik
        {
            get
            {
                return UserValidationHelper.ValidateEmail(_user.Email) == null || !_isEmailChanged ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public Visibility Email2ErrorIconVisibility // Ha a Email2 Exceptiont kap akkor az ErrorIcon megjelenik
        {
            get
            {
                return UserValidationHelper.ValidateEmail2(_user.Email, _email2) == null || !_isEmail2Changed ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public Visibility TelephoneErrorIconVisibility // Ha a Telefon Exceptiont kap akkor az ErrorIcon megjelenik
        {
            get
            {
                return UserValidationHelper.ValidateTelephone(_user.Telephone) == null || !_isTelephoneChanged ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public Visibility CompanyNameErrorIconVisibility // Ha a Cegnev Exceptiont kap akkor az ErrorIcon megjelenik
        {
            get
            {
                return UserValidationHelper.ValidateCompanyName(_companyName) == null || !_isCompanyNameChanged ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public Visibility CompanyName2ErrorIconVisibility // Ha a Cegnev2 Exceptiont kap akkor az ErrorIcon megjelenik
        {
            get
            {
                return UserValidationHelper.ValidateCompanyName2(_companyName, _companyName2) == null || !_isCompanyName2Changed ? Visibility.Hidden : Visibility.Visible;
            }
        }


        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();
        public string Error { get { return null; } } // IDataError-hoz kell

        public string this[string propertyName]
        {
            get
            {
                string result = null;

                if (_isUsernameChanged || _isPasswordChanged || _isPassword2Changed || _isFirstNameChanged || _isLastNameChanged || _isEmailChanged ||
                    _isEmail2Changed || _isTelephoneChanged || _isCompanyNameChanged || _isCompanyName2Changed)
                {
                    switch (propertyName)
                    {
                        case nameof(Username):
                            result = UserValidationHelper.ValidateUserName(_user.Username);
                            break;

                        case nameof(Password):
                            result = UserValidationHelper.ValidatePassword(_user.Password);
                            break;

                        case nameof(Password2):
                            result = UserValidationHelper.ValidatePassword2(_user.Password, _password2);
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

                        case nameof(Email2):
                            result = UserValidationHelper.ValidateEmail2(_user.Email, _email2);
                            break;

                        case nameof(Telephone):
                            result = UserValidationHelper.ValidateTelephone(_user.Telephone);
                            break;

                        case nameof(CompanyName):
                            result = UserValidationHelper.ValidateCompanyName(_companyName);
                            break;

                        case nameof(CompanyName2):
                            result = UserValidationHelper.ValidateCompanyName2(_companyName, _companyName2);
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


        public RelayCommand RegisterAdminCommand { get; private set; }

        public RegisterViewModel(User user, RegisterView view)
        {
            _user = user;
            _view = view;

            RegisterAdminCommand = new RelayCommand(RegisterAdmin, CanExecuteRegister);
        }


        private bool CanExecuteRegister(object arg) // Ha egyik ertek sem ures akkor regisztralhatunk(illetve ha nincs Exception)
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Password2) && !string.IsNullOrEmpty(FirstName) && 
                   !string.IsNullOrEmpty(LastName) && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Email2) && !string.IsNullOrEmpty(Telephone) &&
                   !string.IsNullOrEmpty(CompanyName) && !string.IsNullOrEmpty(CompanyName2);
        }

        private void RegisterAdmin(object arg)
        {
            try
            {
                new UserRepository(new UserLogic()).RegisterAdmin(_user, _password2, _email2, _companyName, _companyName2);
                MessageBox.Show("Admin has been registrated succesfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                SendEmail(); // Emailt kuld az Admin-nak a regisztralt adatokkal

                // Belep a regisztralt felhasznalo adataival
                LoginViewModel.LoggedUser = new UserRepository(new UserLogic()).GetUserByUsername(this.Username);
                _view.Content = new TabcontrolView(true); // true erteket kuldok h a ToggleButton CheckBox-a True legyen(miutan meg nem lehetett modositani a nyelvet es az alap nyelv az angol)
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
            string EmailSubject = "Registration";
            string EmailMessage = "Congratulation on succesfully registrating to Worktime Registry!\n\n" +
                                  "Your profile's data:" +
                                  "\n\t\t\t\t\t\t\t\tUsername: " + this.Username +
                                  "\n\t\t\t\t\t\t\t\tPassword: " + this.Password +
                                  "\n\t\t\t\t\t\t\t\tFirstName: " + this.FirstName +
                                  "\n\t\t\t\t\t\t\t\tLastName: " + this.LastName +
                                  "\n\t\t\t\t\t\t\t\tEmail: " + this.Email +
                                  "\n\t\t\t\t\t\t\t\tTelephone: " + this.Telephone +
                                  "\n\t\t\t\t\t\t\t\tCompany: " + this.CompanyName;
            MailMessage mm = new MailMessage("wpfszakdoga@gmail.com", this.Email, EmailSubject, EmailMessage);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            client.Send(mm);
        }
    }
}