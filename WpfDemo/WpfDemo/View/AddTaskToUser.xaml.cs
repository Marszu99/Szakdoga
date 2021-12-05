using System;
using System.Windows;
using TimeSheet.Model;
using WpfDemo.ViewModel;

namespace WpfDemo.View
{
    /// <summary>
    /// Interaction logic for AddTaskToUser.xaml
    /// </summary>
    public partial class AddTaskToUser : Window
    {
        public AddTaskToUser()
        {
            InitializeComponent();
            this.DataContext = new AddTaskToUserViewModel(new Task() { Deadline = DateTime.Today.AddDays(1) }, this);
        }
    }
}
