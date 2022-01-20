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
using WpfDemo.ViewModel.Command;

namespace WpfDemo.ViewModel
{
    public class UserViewModel : ViewModelBase, IDataErrorInfo
    {
        private User _user;
        private bool _isChanged = false;


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
                _isChanged = true;
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
                _isChanged = true;
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
                _isChanged = true;
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
                _isChanged = true;
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
                _isChanged = true;
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
                return _user != null && _user.IdUser != 0 ? "600" : "200";
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

        public Visibility UserViewSaveButtonVisibility
        {
            get
            {
                return _user != null && _user.IdUser != 0 ? Visibility.Hidden : Visibility.Visible;
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

                   /* case nameof(Password):
                        result = UserValidationHelper.ValidatePassword(_user.Password);
                        break;

                    case nameof(FirstName):
                        result = UserValidationHelper.ValidateFirstName(_user.FirstName);
                        break;

                    case nameof(LastName):
                        result = UserValidationHelper.ValidateLastName(_user.LastName);
                        break;*/

                    case nameof(Email):
                        result = UserValidationHelper.ValidateEmail(_user.Email);
                        break;

                    /*case nameof(Telephone):
                        result = UserValidationHelper.ValidateTelephone(_user.Telephone);
                        break;*/
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


        public UserViewModel(User user)
        {
            _user = user;

            SaveCommand = new RelayCommand(Save, CanSave);
        }

        private bool CanSave(object arg)
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Email) && _isChanged;
        }

        private void Save(object obj)
        {
            try
            {
                if (CheckIfNewUser())
                {
                    CreateUser();
                }
                else
                {
                    UpdateUser();
                }
                UserManagementViewModel.RefreshUserListCommand.Execute(obj);
            }
            catch (SqlException)
            {
                MessageBox.Show(ResourceHandler.GetResourceString("ServerError"));
            }
            catch (UserValidationException)
            {

            }
        }

        private bool CheckIfNewUser()
        {
            return this._user.IdUser == 0;
        }


        private void CreateUser()
        {
            string createdUserRandomPassword = RandomPassword(10);
            this._user.IdUser = new UserRepository(new UserLogic()).CreateUser(this._user, createdUserRandomPassword);
            MessageBox.Show(ResourceHandler.GetResourceString("UserCreatedMessage"), ResourceHandler.GetResourceString("Information"), MessageBoxButton.OK, MessageBoxImage.Information);
            SendEmail(createdUserRandomPassword);
            RefreshValues();
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

        private void UpdateUser()
        {
            new UserRepository(new UserLogic()).UpdateUser(this._user);
            MessageBox.Show(ResourceHandler.GetResourceString("UserUpdatedMessage"), ResourceHandler.GetResourceString("Information"), MessageBoxButton.OK, MessageBoxImage.Information);
            _isChanged = false;
        }


        private void RefreshValues()
        {
            this.IdUser = 0;
            this.Username = "";
            this.Password = "";
            this.FirstName = "";
            this.LastName = "";
            this.Email = "";
            this.Telephone = "";
        }


        public string UsernameString
        {
            get
            {
                return ResourceHandler.GetResourceString("Username");
            }
        }
        public string FirstNameString
        {
            get
            {
                return ResourceHandler.GetResourceString("FirstName");
            }
        }
        public string LastNameString
        {
            get
            {
                return ResourceHandler.GetResourceString("LastName");
            }
        }
        public string EmailString
        {
            get
            {
                return ResourceHandler.GetResourceString("Email");
            }
        }
        public string TelephoneString
        {
            get
            {
                return ResourceHandler.GetResourceString("Telephone");
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
