using System.Data.SqlClient;
using System.Windows;
using TimeSheet.DataAccess;
using TimeSheet.Logic;
using WpfDemo.View;
using WpfDemo.ViewModel.Command;

namespace WpfDemo.ViewModel
{
    public class TabcontrolViewModel : ViewModelBase
    {
        private TabcontrolView _view;

        public string LoggedUserUsername
        {
            get
            {
                return LoginViewModel.LoggedUser.Username;
            }
            /*set
            {
                LoginViewModel.LoggedUser.Username = value;
                OnPropertyChanged(nameof(LoggedUsername));
            }*/
        }
        public string CompanyName
        {
            get
            {
                return new CompanyRepository(new CompanyLogic()).GetCompany().CompanyName;
            }
        }


        public RelayCommand LogoutCommand { get; private set; }
        public RelayCommand ShowMyProfileCommand { get; private set; }

        public TabcontrolViewModel(TabcontrolView view)
        {
            _view = view;

            LogoutCommand = new RelayCommand(Logout, CanExecuteLogout);
            ShowMyProfileCommand = new RelayCommand(ShowMyProfile, CanShowMyProfile);
        }


        private bool CanExecuteLogout(object arg)
        {
            return true;
        }
        private bool CanShowMyProfile(object arg)
        {
            return true;
        }

        private void Logout(object obj)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure you want to logout?", "Logout", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                _view.TabcontrolWindowContent.Content = new LoginView();
            }
        }
        private void ShowMyProfile(object obj)
        {
            try
            {
                MyProfileView Ipage = new MyProfileView();
                (Ipage.DataContext as MyProfileViewModel).CurrentLoggedUser = LoginViewModel.LoggedUser; // CurrentLoggedUser kell vagy a MyProfileViewModel-nel is hasznaljak LoggedUser-t?
                Ipage.ShowDialog();
            }
            catch (SqlException)
            {
                MessageBox.Show("Server error!");
            }
        }
    }
}
