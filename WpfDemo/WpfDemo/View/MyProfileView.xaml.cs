using System.Windows;
using TimeSheet.DataAccess;
using TimeSheet.Logic;
using TimeSheet.Model;
using WpfDemo.ViewModel;

namespace WpfDemo.View
{
    /// <summary>
    /// Interaction logic for MyProfilView.xaml
    /// </summary>
    public partial class MyProfileView : Window
    {
        public MyProfileView()
        {
            InitializeComponent();
            this.DataContext = new MyProfileViewModel();
        }
    }
}
