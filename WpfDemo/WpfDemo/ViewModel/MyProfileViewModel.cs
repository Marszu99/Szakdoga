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
using WpfDemo.View;
using WpfDemo.ViewModel.Command;

namespace WpfDemo.ViewModel
{
    public class MyProfileViewModel: ViewModelBase, IDataErrorInfo
    {

        private User _user;
        private MyProfileView _view;


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


        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();
        public string Error { get { return null; } }

        public string this[string propertyName]
        {
            get
            {
                string result = null;

                switch (propertyName)
                {
                    case nameof(CurrentLoggedUser.Password):
                        result = UserValidationHelper.ValidatePassword(_user.Password);
                        break;

                    case nameof(CurrentLoggedUser.FirstName):
                        result = UserValidationHelper.ValidateFirstName(_user.FirstName);
                        break;

                    case nameof(CurrentLoggedUser.LastName):
                        result = UserValidationHelper.ValidateLastName(_user.LastName);
                        break;

                    case nameof(CurrentLoggedUser.Email):
                        result = UserValidationHelper.ValidateEmail(_user.Email);
                        break;

                    case nameof(CurrentLoggedUser.Telephone):
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
                  !string.IsNullOrEmpty(CurrentLoggedUser.Email) && !string.IsNullOrEmpty(CurrentLoggedUser.Telephone);
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
        }

        private void SaveChangedUserValues(object obj)
        {
            try
            {
                new UserRepository(new UserLogic()).UpdateUser(CurrentLoggedUser);
                MessageBox.Show("User has been updated succesfully!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SqlException)
            {
                MessageBox.Show("Server error!");
            }
            catch (UserValidationException)
            {

            }
            finally
            {
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
            }
        }

        private void CancelChangeUserValues(object obj)
        {
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


        private ObservableCollection<Task> _mytodotaskList = new ObservableCollection<Task>();
        private ObservableCollection<Task> _mydonetaskList = new ObservableCollection<Task>();

        public ObservableCollection<Task> MyToDoTaskList
        {
            get
            {
                return _mytodotaskList;
            }
        }

        void LoadToDoTasks()
        {
            _mytodotaskList.Clear();

            var tasks = new TaskRepository(new TaskLogic()).GetAllActiveTasksFromUser(LoginViewModel.LoggedUser.IdUser);
            tasks.ForEach(task => _mytodotaskList.Add(task));
        }


        public ObservableCollection<Task> MyDoneTaskList
        {
            get
            {
                return _mydonetaskList;
            }
        }

        void LoadDoneTasks()
        {
            _mydonetaskList.Clear();

            var tasks = new TaskRepository(new TaskLogic()).GetAllDoneTasksFromUser(LoginViewModel.LoggedUser.IdUser);
            tasks.ForEach(task => _mydonetaskList.Add(task));
        }
    }
}

