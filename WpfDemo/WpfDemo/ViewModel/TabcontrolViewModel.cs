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
        private bool _isLanguageEnglish = true; // megkulonboztetem vele h mely nyelvre allitsa at a szoveget/kiirasokat


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
                (Ipage.DataContext as MyProfileViewModel).CurrentLoggedUser = new UserRepository(new UserLogic()).GetUserByID(LoginViewModel.LoggedUser.IdUser); // igy ha valamit megvaltoztatok a CurrentLoggedUser adatain es ki "X"-szelem a window-t akkor ujratolti az eredeti adatokat
                Ipage.ShowDialog();
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ChangeLanguage(object obj)
        {
            if (_isLanguageEnglish) // ha Angol a nyelv akkor Magyarra valtoztatom ellenkezo esetben pedig vissza Angolra
            {
                CultureInfo cultureInfo = new CultureInfo("hu-HU");
                cultureInfo.DateTimeFormat.ShortDatePattern = "yyyy.MM.dd";
                cultureInfo.DateTimeFormat.LongDatePattern = "yyyy.MM.dd HH:mm";
                cultureInfo.DateTimeFormat.FullDateTimePattern = "yyyy.MM.dd HH:mm";
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = cultureInfo;

                ResxStaticExtension.OnLanguageChanged(); // Ezzel valtozik meg a szovegek/kiirasok nyelve

                RefreshTaskListForNotificationsLanguageChange(obj);

                _isLanguageEnglish = false; // false-ra allitom h Angolra vissza tudjam allitani a nyelvet
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

                RefreshTaskListForNotificationsLanguageChange(obj);

                _isLanguageEnglish = true; // true-ra allitom h Magyarra tudjam allitani a nyelvet
            }
        }
        public event Action<object> RefreshTaskList;
        public void RefreshTaskListForNotificationsLanguageChange(Object obj)
        {
            RefreshTaskList?.Invoke(obj);
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
