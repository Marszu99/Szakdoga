using System.Windows;
using WpfDemo.ViewModel;

namespace WpfDemo.View
{
    /// <summary>
    /// Interaction logic for UpdateTask.xaml
    /// </summary>
    public partial class UpdateTask : Window
    {
        public UpdateTask()
        {
            InitializeComponent();
            this.DataContext = new UpdateTaskViewModel(this);
        }
    }
}
