using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using TimeSheet.DataAccess;
using TimeSheet.Logic;
using TimeSheet.Model;
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


        public string Username
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

        public string Password
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
                Refresh();
                if (new UserRepository(new UserLogic()).IsValidLogin(_username, _password))
                {
                    LoggedUser = new UserRepository(new UserLogic()).GetUserByUsername(_username);
                    _view.Content = new TabcontrolView();
                    MessageBox.Show("Random password test: " + RandomPassword(10));
                }
                else
                {
                    throw new LoginUserException();
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Server error!");
            }
            catch (LoginUserException)
            {
                _view.LoginUserErrorMessage.Text = "Invalid username or password!";
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

        public void Refresh()
        {
            _view.LoginUserErrorMessage.Text = "";
        }

        public static string RandomPassword(int length)
        {
            Random random = new Random();
            const string chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
