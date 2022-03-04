﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
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
        private string _myProfileViewChangeUserValuesButtonVisibility = "Visible";
        private string _myProfileViewSaveAndCancelButtonsVisibility = "Hidden";

        private System.ComponentModel.IEditableObject _IEditableObject;
        private System.ComponentModel.IRevertibleChangeTracking _IRevertibleChangeTracking;


        public User CurrentLoggedUser
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
                _isPasswordChanged = true;
                OnPropertyChanged(nameof(PasswordErrorIconVisibility));
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
                _isFirstNameChanged = true;
                OnPropertyChanged(nameof(FirstNameErrorIconVisibility));
            }
        }

        public string LastName
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

        private bool NewUserValuesHasBeenAdded
        {
            get
            {
                return (string.IsNullOrWhiteSpace(CurrentLoggedUser.FirstName) || string.IsNullOrWhiteSpace(CurrentLoggedUser.LastName) 
                        || string.IsNullOrWhiteSpace(CurrentLoggedUser.Telephone)) ? false : true;              
            }
        }
        public string MyProfileViewWindowStyle
        {
            get
            {               
                return !NewUserValuesHasBeenAdded ? "None" : "SingleBorderWindow";
            }
        }

        public string MyProfileViewUserValuesBackground
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

        public string MyProfileViewUserValuesBorderThickness
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

        public string MyProfileViewUserValuesBorderBrush
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

        public bool MyProfileViewUserValuesIsReadOnly
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

        public bool MyProfileViewUserPasswordIsEnabled
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

        public string MyProfileViewChangeUserValuesButtonVisibility
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

        public string MyProfileViewSaveAndCancelButtonsVisibility
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

        public Visibility PasswordErrorIconVisibility
        {
            get
            {
                return UserValidationHelper.ValidatePassword(_user.Password) == null || !_isPasswordChanged ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public Visibility FirstNameErrorIconVisibility
        {
            get
            {
                return UserValidationHelper.ValidateFirstName(_user.FirstName) == null || !_isFirstNameChanged ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public Visibility LastNameErrorIconVisibility
        {
            get
            {
                return UserValidationHelper.ValidateLastName(_user.LastName) == null || !_isLastNameChanged ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public Visibility EmailErrorIconVisibility
        {
            get
            {
                return UserValidationHelper.ValidateEmail(_user.Email) == null || !_isEmailChanged ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public Visibility TelephoneErrorIconVisibility
        {
            get
            {
                return UserValidationHelper.ValidateTelephone(_user.Telephone) == null || !_isTelephoneChanged ? Visibility.Hidden : Visibility.Visible;
            }
        }


        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();
        public string Error { get { return null; } }

        public string this[string propertyName]
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
            LoadToDoTasks();
            LoadDoneTasks();

            ChangeUserValuesCommand = new RelayCommand(ChangeUserValues, CanExecuteChange);
            SaveChangedUserValuesCommand = new RelayCommand(SaveChangedUserValues, CanExecuteSave);
            CancelChangeUserValuesCommand = new RelayCommand(CancelChangeUserValues, CanExecuteCancel);
        }

        private bool CanExecuteChange(object arg)
        {
            return true;
        }
        private bool CanExecuteSave(object arg)
        {
            return !string.IsNullOrEmpty(CurrentLoggedUser.Password) && !string.IsNullOrEmpty(CurrentLoggedUser.FirstName) && !string.IsNullOrEmpty(CurrentLoggedUser.LastName)
                   && !string.IsNullOrEmpty(CurrentLoggedUser.Email) && !string.IsNullOrEmpty(CurrentLoggedUser.Telephone) && 
                   (_isPasswordChanged || _isFirstNameChanged || _isLastNameChanged || _isEmailChanged || _isTelephoneChanged);
        }
        private bool CanExecuteCancel(object arg)
        {
            return true;
        }

        private void ChangeUserValues(object obj)
        {       
            MyProfileViewUserValuesIsReadOnly = false;
            MyProfileViewUserPasswordIsEnabled = true;
            MyProfileViewUserValuesBackground = "#FFEEEEEE";
            MyProfileViewUserValuesBorderThickness = "1";
            MyProfileViewUserValuesBorderBrush = "Black";
            MyProfileViewChangeUserValuesButtonVisibility = "Hidden";
            MyProfileViewSaveAndCancelButtonsVisibility = "Visible";

            //_IEditableObject.BeginEdit();
        }

        private void SaveChangedUserValues(object obj)
        {
            try
            {
                new UserRepository(new UserLogic()).UpdateUser(CurrentLoggedUser);
                MessageBox.Show(Resources.UserUpdatedMessage, Resources.Information, MessageBoxButton.OK, MessageBoxImage.Information);

                MyProfileViewUserValuesIsReadOnly = true;
                MyProfileViewUserPasswordIsEnabled = false;
                MyProfileViewUserValuesBackground = "DarkGray";
                MyProfileViewUserValuesBorderThickness = "0";
                MyProfileViewUserValuesBorderBrush = "DarkGray";
                MyProfileViewChangeUserValuesButtonVisibility = "Visible";
                MyProfileViewSaveAndCancelButtonsVisibility = "Hidden";

                IsChangedUservaluesToFalse();
                OnPropertyChanged(nameof(MyProfileViewWindowStyle));
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError);
            }
            catch (UserValidationException)
            {

            }
        }

        private void CancelChangeUserValues(object obj)
        {
            //_IRevertibleChangeTracking.RejectChanges();
            //_IEditableObject.CancelEdit();

            User CurrentLoggedUserValues = new UserRepository(new UserLogic()).GetUserByID(CurrentLoggedUser.IdUser);
            CurrentLoggedUser.Password = CurrentLoggedUserValues.Password;
            CurrentLoggedUser.FirstName = CurrentLoggedUserValues.FirstName;
            CurrentLoggedUser.LastName = CurrentLoggedUserValues.LastName;
            CurrentLoggedUser.Email = CurrentLoggedUserValues.Email;
            CurrentLoggedUser.Telephone = CurrentLoggedUserValues.Telephone;
            OnPropertyChanged(nameof(CurrentLoggedUser));

            MyProfileViewUserValuesIsReadOnly = true;
            MyProfileViewUserPasswordIsEnabled = false;
            MyProfileViewUserValuesBackground = "DarkGray";
            MyProfileViewUserValuesBorderThickness = "0";
            MyProfileViewUserValuesBorderBrush = "DarkGray";
            MyProfileViewChangeUserValuesButtonVisibility = "Visible";
            MyProfileViewSaveAndCancelButtonsVisibility = "Hidden";

            IsChangedUservaluesToFalse();
        }


        private ObservableCollection<Task> _myToDoTaskList = new ObservableCollection<Task>();
        private ObservableCollection<Task> _myDoneTaskList = new ObservableCollection<Task>();

        public ObservableCollection<Task> MyToDoTaskList
        {
            get
            {
                return _myToDoTaskList;
            }
        }

        public void LoadToDoTasks()
        {
            _myToDoTaskList.Clear();

            var tasks = new TaskRepository(new TaskLogic()).GetAllActiveTasksFromUser(LoginViewModel.LoggedUser.IdUser);
            tasks.ForEach(task => _myToDoTaskList.Add(task));
        }


        public ObservableCollection<Task> MyDoneTaskList
        {
            get
            {
                return _myDoneTaskList;
            }
        }

        public void LoadDoneTasks()
        {
            _myDoneTaskList.Clear();

            var tasks = new TaskRepository(new TaskLogic()).GetAllDoneTasksFromUser(LoginViewModel.LoggedUser.IdUser);
            tasks.ForEach(task => _myDoneTaskList.Add(task));
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

