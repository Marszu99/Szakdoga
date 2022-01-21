using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using TimeSheet.DataAccess;
using TimeSheet.Logic;
using TimeSheet.Model;
using TimeSheet.Resource;
using WpfDemo.View;
using WpfDemo.ViewModel.Command;

namespace WpfDemo.ViewModel
{
    public class UserManagementViewModel : ViewModelBase
    {
        private UserManagementView _view;

        public ObservableCollection<UserViewModel> UserList { get; } = new ObservableCollection<UserViewModel>();


        private UserViewModel _selectedUser;
        public UserViewModel SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
                OnPropertyChanged(nameof(SelectedUserVisibility));
            }
        }


        private string _searchValue;
        public string SearchValue
        {
            get { return _searchValue; }
            set
            {
                _searchValue = value;
                OnPropertyChanged(nameof(SearchValue));
                if (String.IsNullOrWhiteSpace(_searchValue))
                {
                    LoadUsers();
                }
                else
                {
                    UserList.Clear();

                    try
                    {
                        var users = new UserRepository(new UserLogic()).GetAllUsers().Where(user => user.Username.Contains(_searchValue) || user.FirstName.Contains(_searchValue) || user.LastName.Contains(_searchValue)).ToList();
                        users.ForEach(user => UserList.Add(new UserViewModel(user)));
                    }
                    catch (SqlException)
                    {
                        MessageBox.Show(ResourceHandler.GetResourceString("ServerError"));
                    }
                    
                }
            }
        }

        public string SearchTextMargin
        {
            get
            {
                if (ResourceHandler.isEnglish)
                {
                    return LoginViewModel.LoggedUser.Status == 0 ? "13 0 0 0" : "530 0 0 0"; // 675 a 13 helyett?
                }
                else
                {
                    return LoginViewModel.LoggedUser.Status == 0 ? "13 0 0 0" : "522 0 0 0";
                }
            }
        }

        public Visibility ListUsersViewContextMenuVisibility // Delete Header Visibility
        {
            get
            {
                return LoginViewModel.LoggedUser.Status == 0 ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility UserManagementButtonsVisibility
        {
            get
            {
                return LoginViewModel.LoggedUser.Status == 0 ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility SelectedUserVisibility
        {
            get
            {
                return _selectedUser == null ? Visibility.Hidden : Visibility.Visible;
                /*if (SelectedUser == null)
                {
                    return Visibility.Hidden;
                }
                else
                {
                    if (SelectedUser.Username == LoginViewModel.LoggedUser.Username || SelectedUser.IdUser == 0)
                    {
                        return Visibility.Visible;
                    }
                    else
                    {
                        return Visibility.Hidden;
                    }
                }*/
            }
        }

        public RelayCommand DeleteCommand { get; private set; }
        public RelayCommand ShowUserProfilCommand { get; private set; }
        public RelayCommand CreateUserCommand { get; private set; }
        public static RelayCommand RefreshUserListCommand { get; private set; }

        public UserManagementViewModel(UserManagementView view)
        {
            _view = view;

            LoadUsers();

            CreateUserCommand = new RelayCommand(CreateUser, CanExecuteShow);
            RefreshUserListCommand = new RelayCommand(RefreshUserList, CanExecuteRefresh);

            DeleteCommand = new RelayCommand(DeleteUser, CanDeleteUser);
            ShowUserProfilCommand = new RelayCommand(ShowUserProfil, CanShowUserProfil);
        }


        private bool CanExecuteShow(object arg)
        {
            return true;
        }
        private void CreateUser(object obj)
        {
            SelectedUser = new UserViewModel(new User());
        }

        private bool CanExecuteRefresh(object arg)
        {
            return true;
        }
        private void RefreshUserList(object obj)
        {
            LoadUsers();
            // SelectedUser = null; ezt egy fuggvenybe
        }

        public void LoadUsers()
        {
            UserList.Clear();

            try
            {
                var users = new UserRepository(new UserLogic()).GetAllUsers();
                users.ForEach(user => UserList.Add(new UserViewModel(user)));
            }
            catch (SqlException)
            {
                MessageBox.Show(ResourceHandler.GetResourceString("ServerError"));
            }

        }

        private void DeleteUser(object obj)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show(ResourceHandler.GetResourceString("UserDeleteQuestion1") + SelectedUser.Username + ResourceHandler.GetResourceString("UserDeleteQuestion2"), ResourceHandler.GetResourceString("Warning"), System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    new UserRepository(new UserLogic()).DeleteUser(SelectedUser.IdUser, SelectedUser.Status);
                    MessageBox.Show(ResourceHandler.GetResourceString("UserDeletedMessage"), ResourceHandler.GetResourceString("Information"), MessageBoxButton.OK, MessageBoxImage.Information);

                    LoadUsers();
                }
                catch (SqlException)
                {
                    MessageBox.Show(ResourceHandler.GetResourceString("ServerError"));
                }
            }
        }

        private void ShowUserProfil(object obj)
        {
            UserProfileView Ipage = new UserProfileView(SelectedUser.IdUser);
            (Ipage.DataContext as UserProfileViewModel).CurrentUser = SelectedUser.User;
            Ipage.ShowDialog();
        }

        private bool CanDeleteUser(object arg)
        {
            return _selectedUser != null && SelectedUser.Username != LoginViewModel.LoggedUser.Username && LoginViewModel.LoggedUser.Status != 0;
        }
        private bool CanShowUserProfil(object arg)
        {
            return _selectedUser != null;
        }


        public string NewString
        {
            get
            {
                return ResourceHandler.GetResourceString("New");
            }
        }
        public string RefreshString
        {
            get
            {
                return ResourceHandler.GetResourceString("Refresh");
            }
        }
        public string SearchString
        {
            get
            {
                return ResourceHandler.GetResourceString("Search");
            }
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
    }
}
