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
                _isUsernameChanged = true;
                OnPropertyChanged(nameof(UsernameErrorIconVisibility));
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
                _isEmailChanged = true;
                OnPropertyChanged(nameof(EmailErrorIconVisibility));
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

        public bool IsUserViewValuesReadOnly
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

        public string UserViewHeight
        {
            get
            {
                return _user != null && _user.IdUser != 0 ? "665" : "200";
            }
        }

        public string UserViewRowHeight
        {
            get
            {
                return _user != null && _user.IdUser != 0 ? "*" : "0";
            }
        }

        public string UserViewEmailGridRow
        {
            get
            {
                return _user != null && _user.IdUser != 0 ? "3" : "1";
            }
        }

        public string UserViewFirstNameGridRow
        {
            get
            {
                return _user != null && _user.IdUser != 0 ? "1" : "3";
            }
        }

        public string UserViewThirdColumnWidth
        {
            get
            {
                return _user.IdUser != 0 ? "0" : "0.2*";
            }
        }

        public string UserViewValuesBackground
        {
            get
            {
                return _user.IdUser != 0 ? "transparent" : "#eee";
            }
        }

        public string UserViewValuesBorderThickness
        {
            get
            {
                return _user.IdUser != 0 ? "0" : "1";
            }
        }

        public Visibility UserViewSaveButtonVisibility
        {
            get
            {
                return _user != null && _user.IdUser != 0 ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility UserViewUsernameVisibility
        {
            get
            {
                return _user != null && _user.IdUser == 0 ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public Visibility UserViewUsernameWithColonVisibility
        {
            get
            {
                return _user != null &&  _user.IdUser != 0 ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public Visibility UserViewEmailVisibility
        {
            get
            {
                return _user != null && _user.IdUser == 0 ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public Visibility UserViewEmailWithColonVisibility
        {
            get
            {
                return _user != null && _user.IdUser != 0 ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public Visibility UsernameErrorIconVisibility
        {
            get
            {
                return UserValidationHelper.ValidateUserName(_user.Username) == null || !_isUsernameChanged ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public Visibility EmailErrorIconVisibility
        {
            get
            {
                return UserValidationHelper.ValidateEmail(_user.Email) == null || !_isEmailChanged ? Visibility.Hidden : Visibility.Visible;
            }
        }


        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();
        public string Error { get { return null; } }

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

        private bool CanSave(object arg)
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Email) && (_isUsernameChanged || _isEmailChanged);
        }

        private void Save(object obj)
        {
            try
            {
                CreateUser();
                /* if (CheckIfNewUser())
                 {
                     CreateUser();
                 }
                 else
                 {
                     UpdateUser();
                 }*/
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError);
            }
            catch (UserValidationException)
            {

            }
        }

        /*private bool CheckIfNewUser()
        {
            return this._user.IdUser == 0;
        }*/


        private void CreateUser()
        {
            string createdUserRandomPassword = RandomPassword(10);
            this._user.IdUser = new UserRepository(new UserLogic()).CreateUser(this._user, createdUserRandomPassword);
            MessageBox.Show(Resources.UserCreatedMessage, Resources.Information, MessageBoxButton.OK, MessageBoxImage.Information);

            CreateUserToList(this);

            SendEmail(createdUserRandomPassword);
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

        /*private void UpdateUser()
        {
            new UserRepository(new UserLogic()).UpdateUser(this._user);
            MessageBox.Show(Resources.UserUpdatedMessage, Resources.Information, MessageBoxButton.OK, MessageBoxImage.Information);
        }*/

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
            CancelUser(obj);
        }
    }
}
