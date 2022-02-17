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

        public string Username
        {
            get
            {
                return _user.Username;
            }
            set
            {
                _user.Username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
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

        private string _password2;
        public string Password2
        {
            get
            {
                return _password2;
            }
            set
            {
                _password2 = value;
                OnPropertyChanged(nameof(Password2));
            }
        }

        public string FirstName
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

        public string LastName
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

        public string Email
        {
            get
            {
                return _user.Email;
            }
            set
            {
                _user.Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string _email2;
        public string Email2
        {
            get
            {
                return _email2;
            }
            set
            {
                _email2 = value;
                OnPropertyChanged(nameof(Email2));
            }
        }

        public string Telephone
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

        private string _companyName;
        public string CompanyName
        {
            get
            {
                return _companyName;
            }
            set
            {
                _companyName = value;
                OnPropertyChanged(nameof(CompanyName));
            }
        }

        private string _companyName2;
        public string CompanyName2
        {
            get
            {
                return _companyName2;
            }
            set
            {
                _companyName2 = value;
                OnPropertyChanged(nameof(CompanyName2));
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

                return result;
            }
        }


        public RegisterViewModel(User user, RegisterView view)
        {
            _user = user;
            _view = view;

            RegisterAdminCommand = new RelayCommand(RegisterAdmin, CanExecuteRegister);
            BackToLoginCommand = new RelayCommand(BackToLogin, CanExecute);
        }


        public RelayCommand RegisterAdminCommand { get; private set; }
        public RelayCommand BackToLoginCommand { get; private set; }

        private bool CanExecuteRegister(object arg)
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
                SendEmail();
                //ExpanderMessage.Text = "User has been created succesfully!";
                LoginViewModel.LoggedUser = new UserRepository(new UserLogic()).GetUserByUsername(this.Username);
                _view.Content = new TabcontrolView();
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError);
            }
            catch (UserValidationException)
            {

            }
        }


        private bool CanExecute(object arg)
        {
            return true;
        }

        private void BackToLogin(object arg)
        {
            _view.RegisterContent.Content = new LoginView();
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