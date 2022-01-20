using System.Data.SqlClient;
using System.Windows;
using TimeSheet.DataAccess;
using TimeSheet.Logic;
using TimeSheet.Resource;
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


        public RelayCommand ShowMyProfileCommand { get; private set; }
        public RelayCommand ChangeLanguageCommand { get; private set; }
        public RelayCommand LogoutCommand { get; private set; }


        public TabcontrolViewModel(TabcontrolView view)
        {
            _view = view;

            ShowMyProfileCommand = new RelayCommand(ShowMyProfile, CanShowMyProfile);
            ChangeLanguageCommand = new RelayCommand(ChangeLanguage, CanChangeLanguage);
            LogoutCommand = new RelayCommand(Logout, CanExecuteLogout);

        }

        
        private bool CanShowMyProfile(object arg)
        {
            return true;
        }
        private bool CanChangeLanguage(object arg)
        {
            return true;
        }
        private bool CanExecuteLogout(object arg)
        {
            return true;
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
                MessageBox.Show(ResourceHandler.GetResourceString("ServerError"));
            }
        }
        private void ChangeLanguage(object obj)
        {
            if (ResourceHandler.isEnglish == true)
            {
                ResourceHandler.isEnglish = false;
                _view.TabcontrolWindowContent.Content = new TabcontrolView();
            }
            else
            {
                ResourceHandler.isEnglish = true;
                _view.TabcontrolWindowContent.Content = new TabcontrolView();
            }
        }
        private void Logout(object obj)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show(ResourceHandler.GetResourceString("LogoutMessage"), ResourceHandler.GetResourceString("Logout"), System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                _view.TabcontrolWindowContent.Content = new LoginView();
            }
        }

        public string ProfileString
        {
            get
            {
                return ResourceHandler.GetResourceString("Profile");
            }
        }
        public string LanuageString
        {
            get
            {
                return ResourceHandler.GetResourceString("Language");
            }
        }
        public string LogoutString
        {
            get
            {
                return ResourceHandler.GetResourceString("Logout");
            }
        }
        public string RecordString
        {
            get
            {
                return ResourceHandler.GetResourceString("Record");
            }
        }
        public string TaskString
        {
            get
            {
                return ResourceHandler.GetResourceString("Task");
            }
        }
        public string UserString
        {
            get
            {
                return ResourceHandler.GetResourceString("User");
            }
        }
    }
}
