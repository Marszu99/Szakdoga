using System.Windows;
using TimeSheet.Model;
using WpfDemo.ViewModel;

namespace WpfDemo.View
{
    /// <summary>
    /// Interaction logic for UserProfileTaskView.xaml
    /// </summary>
    public partial class UserProfileTaskView : Window
    {
        public UserProfileTaskView()
        {
            InitializeComponent();
            this.DataContext = new UserProfileTaskViewModel(new Task(), this);
        }
    }
}
