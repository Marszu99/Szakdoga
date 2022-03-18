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
                OnPropertyChanged(nameof(ListUsersViewContextMenuVisibility));
            }
        }


        private string _searchValue;
        public string SearchValue // keresesi szoveg bindolashoz
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
                        var users = new UserRepository(new UserLogic()).GetAllUsers().Where(user => user.Username.Contains(_searchValue) 
                                    || user.FirstName.Contains(_searchValue) || user.LastName.Contains(_searchValue)).ToList();
                        users.ForEach(user => UserList.Add(new UserViewModel(user)));
                    }
                    catch (SqlException)
                    {
                        MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }


        public Visibility SelectedUserVisibility // Kivalasztott felhasznalo lathatosaga
        {
            get
            {
                if (SelectedUser != null)
                {
                    SelectedUser.UserCanceled += OnUserCanceled; // UserCanceled Event("Cancel" gomb megnyomasa) eseten a kivalasztott felhasznalo eltunik

                }
                return SelectedUser == null ? Visibility.Collapsed : Visibility.Visible;
            }
        }
        private void OnUserCanceled(Object obj)
        {
            SelectedUser = null;
        }

        public Visibility ListUsersViewContextMenuVisibility // (Delete Header Visibility) Csak Admin eseteben jelenik meg jobb klikkre egy torles lehetoseg
        {
            get
            {
                return LoginViewModel.LoggedUser.Status == 0 || SelectedUser.Username == LoginViewModel.LoggedUser.Username ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public Visibility UserManagementButtonsVisibility // Uj Felhasznalo letrehozasa gomb lathatossaga(Admin eseteben lathato)
        {
            get
            {
                return LoginViewModel.LoggedUser.Status == 0 ? Visibility.Collapsed : Visibility.Visible;
            }
        }


        public RelayCommand DeleteCommand { get; private set; }
        public RelayCommand ShowUserProfilCommand { get; private set; }
        public RelayCommand CreateUserCommand { get; private set; }
        public RelayCommand RefreshUserListCommand { get; private set; }

        public UserManagementViewModel()
        {
            LoadUsers(); // Felhasznalok betoltese

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
            SelectedUser.UserCreated += OnUserCreated; // UserCreated Event ("Save" gomb megnyomasa) eseten hozzaadodik a listahoz a felhasznalo es ujat tudsz letrehozni megint
        }
        private void OnUserCreated(UserViewModel userViewModel)
        {
            UserList.Add(userViewModel);// hozzaadja a listahoz

            // Uj letrehozasahoz
            SelectedUser = new UserViewModel(new User());
            SelectedUser.UserCreated += OnUserCreated;
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
                MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private bool CanDeleteUser(object arg)
        {
            return _selectedUser != null && SelectedUser.Username != LoginViewModel.LoggedUser.Username && LoginViewModel.LoggedUser.Status != 0;
        }
        private void DeleteUser(object obj)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show(Resources.UserDeleteQuestion1 + SelectedUser.Username + Resources.UserDeleteQuestion2, 
                                                Resources.Warning, MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    new UserRepository(new UserLogic()).DeleteUser(SelectedUser.IdUser, SelectedUser.Status);
                    MessageBox.Show(Resources.UserDeletedMessage, Resources.Information, MessageBoxButton.OK, MessageBoxImage.Information);

                    LoadUsers();
                }
                catch (SqlException)
                {
                    MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }


        private bool CanShowUserProfil(object arg)
        {
            return _selectedUser != null;
        }
        private void ShowUserProfil(object obj) // Dupla klikk eseten a UserProfileView-t megnyitja
        {
            UserProfileView Ipage = new UserProfileView(SelectedUser.IdUser);
            (Ipage.DataContext as UserProfileViewModel).CurrentUser = SelectedUser.User;
            Ipage.ShowDialog();
        }
    }
}
