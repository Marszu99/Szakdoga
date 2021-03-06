using TimeSheet.DataAccess;
using TimeSheet.Logic;
using WpfDemo.View;

namespace WpfDemo.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private MainView _view;

        public MainViewModel(MainView view)
        {
            _view = view;
            MainWindowContent();
        }

        public void MainWindowContent()
        {
            if (new UserRepository(new UserLogic()).GetAdmin().IdUser == 0) // Ha letezik admin akkor Login-ba dob ha pedig nem akkor regisztralunk admint
            {
                _view.MainWindow.Content = new RegisterView();
            }
            else
            {
                _view.MainWindow.Content = new LoginView();
            }

        }
    }
}
