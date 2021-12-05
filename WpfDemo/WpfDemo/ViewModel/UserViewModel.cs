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

        public bool IsUserNameReadOnly
        {
            get
            {
                return _user.IdUser != 0;
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
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName) &&
                   !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Telephone) && _isChanged == true ;
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
            }
            catch (SqlException)
            {
                MessageBox.Show("Server error!");
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
            this._user.IdUser = new UserRepository(new UserLogic()).CreateUser(this._user);
            MessageBox.Show("User has been created succesfully!", "User created", MessageBoxButton.OK, MessageBoxImage.Information);
            RefreshValues();
        }

        private void UpdateUser()
        {
            new UserRepository(new UserLogic()).UpdateUser(this._user);
            MessageBox.Show("User has been updated succesfully!", "Update user", MessageBoxButton.OK, MessageBoxImage.Information);
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
    }
}
