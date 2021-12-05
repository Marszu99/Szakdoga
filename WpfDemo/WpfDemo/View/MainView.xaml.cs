using System.Windows;

namespace WpfDemo.View
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            MainWindow.Content = new LoginView();
        }
    }
}
