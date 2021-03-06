using System.Windows.Controls;
using WpfDemo.ViewModel;

namespace WpfDemo.View
{
    /// <summary>
    /// Interaction logic for TabcontrolView.xaml
    /// </summary>
    public partial class TabcontrolView : UserControl
    {

        public TabcontrolView(bool isLanguageEnglish)
        {
            InitializeComponent();
            this.DataContext = new TabcontrolViewModel(this, isLanguageEnglish);
        }
    }
}
