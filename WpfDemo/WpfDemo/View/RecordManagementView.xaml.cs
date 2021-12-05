using System.Windows.Controls;
using WpfDemo.ViewModel;

namespace WpfDemo.View
{
    /// <summary>
    /// Interaction logic for RecordManagementView.xaml
    /// </summary>
    public partial class RecordManagementView : UserControl
    {
        public RecordManagementView()
        {
            InitializeComponent();
            this.DataContext = new RecordManagementViewModel(this);
        }
    }
}
