using System.Windows.Controls;
using WpfDemo.ViewModel;

namespace WpfDemo.View
{
    /// <summary>
    /// Interaction logic for TaskManagementView.xaml
    /// </summary>
    public partial class TaskManagementView : UserControl
    {
        public TaskManagementView()
        {
            InitializeComponent();
            this.DataContext = new TaskManagementViewModel();
        }
    }
}
