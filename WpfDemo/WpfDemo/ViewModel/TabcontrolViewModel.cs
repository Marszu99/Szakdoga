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
        private bool _isLanguageEnglish; // megkulonboztetem vele h mely nyelvre allitsa at a szoveget/kiirasokat


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

        public bool IsToggleButtonChecked
        {
            get
            {
                return _isLanguageEnglish;
            }
            set
            {
                _isLanguageEnglish = value;
                OnPropertyChanged(nameof(IsToggleButtonChecked));
            }
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


        public TabcontrolViewModel(TabcontrolView view, bool isLanguageEnglish)
        {
            _view = view;
            _isLanguageEnglish = isLanguageEnglish;

            ShowMyProfileCommand = new RelayCommand(ShowMyProfile, CanShowMyProfile);
            ChangeLanguageCommand = new RelayCommand(ChangeLanguage, CanChangeLanguage);
            LogoutCommand = new RelayCommand(Logout, CanExecuteLogout);
        }

        
        private bool CanShowMyProfile(object arg)
        {
            return true;
        }
        private void ShowMyProfile(object obj)
        {
            try
            {
                MyProfileView Ipage = new MyProfileView();
                (Ipage.DataContext as MyProfileViewModel).CurrentLoggedUser = new UserRepository(new UserLogic()).GetUserByID(LoginViewModel.LoggedUser.IdUser); // igy ha valamit megvaltoztatok a CurrentLoggedUser adatain es ki "X"-szelem a window-t akkor ujratolti az eredeti adatokat
                Ipage.ShowDialog();
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private bool CanChangeLanguage(object arg)
        {
            return true;
        }
        private void ChangeLanguage(object obj)
        {
            if (!_isLanguageEnglish) // ha _isLanguageEnglish erteke false akkor Magyarra valtoztatom ellenkezo esetben pedig vissza Angolra
            {
                CultureInfo cultureInfo = new CultureInfo("hu-HU");
                cultureInfo.DateTimeFormat.ShortDatePattern = "yyyy.MM.dd";
                cultureInfo.DateTimeFormat.LongDatePattern = "yyyy.MM.dd HH:mm";
                cultureInfo.DateTimeFormat.FullDateTimePattern = "yyyy.MM.dd HH:mm";
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = cultureInfo;

                ResxStaticExtension.OnLanguageChanged(); // Ezzel valtozik meg a szovegek/kiirasok nyelve

                //RefreshTaskListForNotificationsLanguageChange(obj); // MASIK MEGOLDASHOZ??
            }
            else
            {
                CultureInfo cultureInfo = new CultureInfo("en-US");
                cultureInfo.DateTimeFormat.ShortDatePattern = "yyyy.MM.dd";
                cultureInfo.DateTimeFormat.LongDatePattern = "yyyy.MM.dd HH:mm";
                cultureInfo.DateTimeFormat.FullDateTimePattern = "yyyy.MM.dd HH:mm";
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = cultureInfo;

                ResxStaticExtension.OnLanguageChanged(); // Ezzel valtozik meg a szovegek/kiirasok nyelve

                //RefreshTaskListForNotificationsLanguageChange(obj); // MASIK MEGOLDASHOZ??
            }
        }
        /*public event Action<object> RefreshTaskList; // MASIK MEGOLDASHOZ??
        public void RefreshTaskListForNotificationsLanguageChange(Object obj) // MASIK MEGOLDASHOZ??
        {
            RefreshTaskList?.Invoke(obj);
        }*/


        private bool CanExecuteLogout(object arg)
        {
            return true;
        }
        private void Logout(object obj) // Kijelentkezes
        {
            MessageBoxResult messageBoxResult = MessageBox.Show(Resources.LogoutMessage, Resources.Logout, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                _view.TabcontrolWindowContent.Content = new LoginView();
            }
        }
    }
}
