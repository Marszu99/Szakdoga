using System.Windows;
using WpfDemo.ViewModel;

namespace WpfDemo.View
{
    /// <summary>
    /// Interaction logic for UserProfilView.xaml
    /// </summary>
    public partial class UserProfileView : Window
    {
        public UserProfileView(int UserProfileId)
        {
            InitializeComponent();
            this.DataContext = new UserProfileViewModel(UserProfileId);
        }
    }
}
