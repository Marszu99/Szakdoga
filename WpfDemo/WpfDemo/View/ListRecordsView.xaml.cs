using System.Windows.Controls;

namespace WpfDemo.View
{
    /// <summary>
    /// Interaction logic for ListRecordsView.xaml
    /// </summary>
    public partial class ListRecordsView : UserControl
    {
        public ListRecordsView()
        {
            InitializeComponent();
            //DataContext = new ListRecordsViewModel(new Record());
        }
    }
}
