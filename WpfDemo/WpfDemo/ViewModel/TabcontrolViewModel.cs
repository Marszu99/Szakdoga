using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;
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
        private bool _isLanguageEnglish = true;


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
                MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ChangeLanguage(object obj)
        {
            if (_isLanguageEnglish)
            {
                CultureInfo cultureInfo = new CultureInfo("hu-HU");
                cultureInfo.DateTimeFormat.ShortDatePattern = "yyyy.MM.dd";
                cultureInfo.DateTimeFormat.LongDatePattern = "yyyy.MM.dd HH:mm";
                cultureInfo.DateTimeFormat.FullDateTimePattern = "yyyy.MM.dd HH:mm";
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = cultureInfo;

                ResxStaticExtension.OnLanguageChanged();

                RefreshTaskListForNotificationsLanguageChange(obj);

                _isLanguageEnglish = false;
            }
            else
            {
                CultureInfo cultureInfo = new CultureInfo("en-US");
                cultureInfo.DateTimeFormat.ShortDatePattern = "yyyy.MM.dd";
                cultureInfo.DateTimeFormat.LongDatePattern = "yyyy.MM.dd HH:mm";
                cultureInfo.DateTimeFormat.FullDateTimePattern = "yyyy.MM.dd HH:mm";
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = cultureInfo;

                ResxStaticExtension.OnLanguageChanged();

                RefreshTaskListForNotificationsLanguageChange(obj);

                _isLanguageEnglish = true;
            }
        }
        public event Action<object> RefreshTaskList;
        public void RefreshTaskListForNotificationsLanguageChange(Object obj)
        {
            RefreshTaskList?.Invoke(obj);
        }

        private void Logout(object obj)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show(Resources.LogoutMessage, Resources.Logout, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                _view.TabcontrolWindowContent.Content = new LoginView();
            }
        }
    }
}
