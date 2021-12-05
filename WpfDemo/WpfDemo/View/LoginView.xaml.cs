using System.Windows.Controls;
using WpfDemo.ViewModel;

namespace WpfDemo.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
            this.DataContext = new LoginViewModel(LoginUsername.Text, LoginPassword.Password, this);
        }
    }
}
