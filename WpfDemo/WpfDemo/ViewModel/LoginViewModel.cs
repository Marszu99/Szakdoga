using System.Data.SqlClient;
using System.Windows;
using TimeSheet.DataAccess;
using TimeSheet.Logic;
using TimeSheet.Model;
using TimeSheet.Resource;
using WpfDemo.View;
using WpfDemo.ViewModel.Command;

namespace WpfDemo.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private LoginView _view;
        private string _username;
        private string _password;
        public static User LoggedUser;


        public string Username // Felhasznalonev a bindolashoz
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password // Jelszo a bindolashoz
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }


        public RelayCommand LoginCommand { get; private set; } // ICommand vagy RelayCommand??
        public RelayCommand ShowRegisterCommand { get; private set; } // ICommand vagy RelayCommand??

        public LoginViewModel(string username, string password, LoginView view)
        {
            _username = username;
            _password = password;
            _view = view;

            LoginCommand = new RelayCommand(Login, CanExecuteLogin);
            ShowRegisterCommand = new RelayCommand(ShowRegister, CanExecuteShow);
        }

        private bool CanExecuteLogin(object arg)
        {
            return !string.IsNullOrEmpty(_username) && !string.IsNullOrEmpty(_password);
        }
        private bool CanExecuteShow(object arg)
        {
            return true;
        }

        private void Login(object obj)
        {
            try
            {
                if (new UserRepository(new UserLogic()).IsValidLogin(_username, _password))
                {
                    LoggedUser = new UserRepository(new UserLogic()).GetUserByUsername(_username); // Lementem a belepet felhasznalot(amig ki nem lep)

                    if (string.IsNullOrWhiteSpace(LoggedUser.FirstName) && string.IsNullOrWhiteSpace(LoggedUser.LastName) && string.IsNullOrWhiteSpace(LoggedUser.Telephone))  // megnezzuk h uj felhasznalo lep-e be mert annak nincs megadva ezek az adatok
                    {
                        // ha uj felhasznalokent lep be akkor kap uzenetet h ki kell tolteni a hianyzo adatait es MyProfileView-ba iranyitjuk at
                        _view.Content = new TabcontrolView(true); // true erteket kuldok h a ToggleButton CheckBox-a True legyen(miutan meg nem lehetett modositani a nyelvet es az alap nyelv az angol)
                        MyProfileView Ipage = new MyProfileView();
                        (Ipage.DataContext as MyProfileViewModel).CurrentLoggedUser = LoggedUser;
                        MessageBox.Show(Resources.MissingProfileDatasMessage);
                        Ipage.ShowDialog();
                    }
                    else
                    {
                        if (_view.LoginButton.Content.ToString().Equals("BELÉPÉS")) // Mengezem h login eseten a gomb kiirasa magyar-e
                        {
                            _view.Content = new TabcontrolView(false); // ha magyar akkor false erteket kuldok h a ToggleButton CheckBox-a False legyen
                        }
                        else
                        {
                            _view.Content = new TabcontrolView(true); // ha angol akkor true erteket kuldok h a ToggleButton CheckBox-a True legyen
                        }
                    }
                }
                else
                {
                    _view.LoginErrorIcon.Visibility = Visibility.Visible;
                    throw new LoginUserException();
                }
            }
            catch (SqlException)
            {
                MessageBox.Show(Resources.ServerError, Resources.Warning, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (LoginUserException)
            {
                _view.LoginUserErrorMessage.Text = Resources.LoginErrorMessage;
            }
            catch (LoginException ex)
            {
                _view.LoginUserErrorMessage.Text = ex.Message.ToString();
            }
        }

        private void ShowRegister(object obj)
        {
            _view.LoginWindow.Content = new RegisterView();
        }
    }
}
