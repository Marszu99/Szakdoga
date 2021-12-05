using System.Windows;
using System.Windows.Controls;
using WpfDemo.ViewModel;

namespace WpfDemo.View
{
    /// <summary>
    /// Interaction logic for TabcontrolView.xaml
    /// </summary>
    public partial class TabcontrolView : UserControl
    {

        public TabcontrolView()
        {
            InitializeComponent();
            this.DataContext = new TabcontrolViewModel(this);
        }

        private void NextTabButton_Click(object sender, RoutedEventArgs e)
        {
            if(tabControl.SelectedIndex == 2)
            {
                return;
            }
            else
            {
                tabControl.SelectedIndex += 1;
            }
        }

        private void PreviousTabButton_Click(object sender, RoutedEventArgs e)
        {
            if (tabControl.SelectedIndex == 0)
            {
                return;
            }
            else
            {
                tabControl.SelectedIndex -= 1;
            }
        }
    }
}
