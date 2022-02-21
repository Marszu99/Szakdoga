using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Media;
using TimeSheet.DataAccess;
using TimeSheet.Logic;
using TimeSheet.Model;
using TimeSheet.Model.Extension;
using TimeSheet.Resource;
using WpfDemo.View;
using WpfDemo.ViewModel.Command;

namespace WpfDemo.ViewModel
{
    public class MyProfileViewModel : ViewModelBase, IDataErrorInfo//IEditableObject,IRevertibleChangeTracking 
    {

        private User _user;
        private MyProfileView _view;
        private bool _isChanged = false;
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
                return CurrentLoggedUser.LastName;
            }
            set
            {
                CurrentLoggedUser.LastName = value;
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
                return CurrentLoggedUser.Telephone;
            }
            set
            {
                CurrentLoggedUser.Telephone = value;
                OnPropertyChanged(nameof(Telephone));

                _isChanged = true;
            }
        }

        private bool NewUserValuesHasBeenAdded
        {
            get
            {
                return (string.IsNullOrWhiteSpace(CurrentLoggedUser.FirstName) || string.IsNullOrWhiteSpace(CurrentLoggedUser.LastName) || string.IsNullOrWhiteSpace(CurrentLoggedUser.Telephone)) ? false : true;              
            }
        }
        public string MyProfileViewWindowStyle
        {
            get
            {               
                return !NewUserValuesHasBeenAdded ? "None" : "SingleBorderWindow";
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


        public RelayCommand ChangeUserValuesCommand { get; private set; }
        public RelayCommand SaveChangedUserValuesCommand { get; private set; }
        public RelayCommand CancelChangeUserValuesCommand { get; private set; }

        public MyProfileViewModel(MyProfileView view)
        {
            _view = view;
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
            return !string.IsNullOrEmpty(CurrentLoggedUser.Password) && !string.IsNullOrEmpty(CurrentLoggedUser.FirstName) && !string.IsNullOrEmpty(CurrentLoggedUser.LastName) &&
                    !string.IsNullOrEmpty(CurrentLoggedUser.Email) && !string.IsNullOrEmpty(CurrentLoggedUser.Telephone) && _isChanged;
        }
        private bool CanExecuteCancel(object arg)
        {
            return true;
        }

        private void ChangeUserValues(object obj)
        {
            //_view.MyProfilePassword.IsReadOnly = false;
            _view.MyProfilePassword.IsEnabled = true;

            _view.MyProfileFirstName.IsReadOnly = false;
            _view.MyProfileFirstName.BorderThickness.Equals(1);
            _view.MyProfileFirstName.BorderBrush = Brushes.Black;

            _view.MyProfileLastName.IsReadOnly = false;
            _view.MyProfileLastName.BorderThickness.Equals(1);
            _view.MyProfileLastName.BorderBrush = Brushes.Black;

            _view.MyProfileEmail.IsReadOnly = false;
            _view.MyProfileEmail.BorderThickness.Equals(1);
            _view.MyProfileEmail.BorderBrush = Brushes.Black;

            _view.MyProfileTelephone.IsReadOnly = false;
            _view.MyProfileTelephone.BorderThickness.Equals(1);
            _view.MyProfileTelephone.BorderBrush = Brushes.Black;

            BrushConverter bc = new BrushConverter();
            _view.MyProfilePassword.Background = (Brush)bc.ConvertFrom("#FFEEEEEE");
            _view.MyProfileFirstName.Background = (Brush)bc.ConvertFrom("#FFEEEEEE");
            _view.MyProfileLastName.Background = (Brush)bc.ConvertFrom("#FFEEEEEE");
            _view.MyProfileEmail.Background = (Brush)bc.ConvertFrom("#FFEEEEEE");
            _view.MyProfileTelephone.Background = (Brush)bc.ConvertFrom("#FFEEEEEE");

            _view.ChangeUserValuesButton.Visibility = Visibility.Hidden;
            _view.SaveChangedUserValuesButton.Visibility = Visibility.Visible;
            _view.CancelChangeUserValuesButton.Visibility = Visibility.Visible;

            //_IEditableObject.BeginEdit();
        }

        private void SaveChangedUserValues(object obj)
        {
            try
            {
                new UserRepository(new UserLogic()).UpdateUser(CurrentLoggedUser);
                MessageBox.Show(Resources.UserUpdatedMessage, Resources.Information, MessageBoxButton.OK, MessageBoxImage.Information);

                // _view.MyProfilePassword.IsReadOnly = true;
                _view.MyProfilePassword.IsEnabled = false;

                _view.MyProfileFirstName.IsReadOnly = true;
                _view.MyProfileFirstName.BorderThickness.Equals(0);
                _view.MyProfileFirstName.BorderBrush = Brushes.DarkGray;


                _view.MyProfileLastName.IsReadOnly = true;
                _view.MyProfileLastName.BorderThickness.Equals(0);
                _view.MyProfileLastName.BorderBrush = Brushes.DarkGray;


                _view.MyProfileEmail.IsReadOnly = true;
                _view.MyProfileEmail.BorderThickness.Equals(0);
                _view.MyProfileEmail.BorderBrush = Brushes.DarkGray;


                _view.MyProfileTelephone.IsReadOnly = true;
                _view.MyProfileTelephone.BorderThickness.Equals(0);
                _view.MyProfileTelephone.BorderBrush = Brushes.DarkGray;



                _view.MyProfilePassword.Background = Brushes.DarkGray;
                _view.MyProfileFirstName.Background = Brushes.DarkGray;
                _view.MyProfileLastName.Background = Brushes.DarkGray;
                _view.MyProfileEmail.Background = Brushes.DarkGray;
                _view.MyProfileTelephone.Background = Brushes.DarkGray;

                _view.ChangeUserValuesButton.Visibility = Visibility.Visible;
                _view.SaveChangedUserValuesButton.Visibility = Visibility.Hidden;
                _view.CancelChangeUserValuesButton.Visibility = Visibility.Hidden;

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

            //_view.MyProfilePassword.IsReadOnly = true;
            _view.MyProfilePassword.IsEnabled = false;

            _view.MyProfileFirstName.IsReadOnly = true;
            _view.MyProfileFirstName.BorderThickness.Equals(0);
            _view.MyProfileFirstName.BorderBrush = Brushes.DarkGray;


            _view.MyProfileLastName.IsReadOnly = true;
            _view.MyProfileLastName.BorderThickness.Equals(0);
            _view.MyProfileLastName.BorderBrush = Brushes.DarkGray;


            _view.MyProfileEmail.IsReadOnly = true;
            _view.MyProfileEmail.BorderThickness.Equals(0);
            _view.MyProfileEmail.BorderBrush = Brushes.DarkGray;


            _view.MyProfileTelephone.IsReadOnly = true;
            _view.MyProfileTelephone.BorderThickness.Equals(0);
            _view.MyProfileTelephone.BorderBrush = Brushes.DarkGray;


            _view.MyProfilePassword.Background = Brushes.DarkGray;
            _view.MyProfileFirstName.Background = Brushes.DarkGray;
            _view.MyProfileLastName.Background = Brushes.DarkGray;
            _view.MyProfileEmail.Background = Brushes.DarkGray;
            _view.MyProfileTelephone.Background = Brushes.DarkGray;

            _view.ChangeUserValuesButton.Visibility = Visibility.Visible;
            _view.SaveChangedUserValuesButton.Visibility = Visibility.Hidden;
            _view.CancelChangeUserValuesButton.Visibility = Visibility.Hidden;
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

        public string UsernameString
        {
            get
            {
                return Resources.Username;
            }
        }
        public string PasswordString
        {
            get
            {
                return Resources.Password;
            }
        }
        public string FirstNameString
        {
            get
            {
                return Resources.FirstName;
            }
        }
        public string LastNameString
        {
            get
            {
                return Resources.LastName;
            }
        }
        public string EmailString
        {
            get
            {
                return Resources.Email;
            }
        }
        public string TelephoneString
        {
            get
            {
                return Resources.Telephone;
            }
        }
        public string MyProfileString
        {
            get
            {
                return Resources.MyProfile;
            }
        }
        public string ChangeString
        {
            get
            {
                return Resources.Change;
            }
        }
        public string SaveString
        {
            get
            {
                return Resources.Save;
            }
        }
        public string CancelString
        {
            get
            {
                return Resources.Cancel;
            }
        }
        public string ToDoTasksString
        {
            get
            {
                return Resources.ToDoTasks;
            }
        }
        public string TitleString
        {
            get
            {
                return Resources.Title;
            }
        }
        public string DescriptionString
        {
            get
            {
                return Resources.Description;
            }
        }
        public string DeadlineString
        {
            get
            {
                return Resources.Deadline;
            }
        }
        public string DoneTasksString
        {
            get
            {
                return Resources.DoneTasks;
            }
        }
    }
}

