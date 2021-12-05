using System.Windows.Controls;
using TimeSheet.Model;
using WpfDemo.ViewModel;

namespace WpfDemo.View
{
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : UserControl
    {
        public RegisterView()
        {
            InitializeComponent();
            this.DataContext = new RegisterViewModel(new User(),this);
        }
    }
}
