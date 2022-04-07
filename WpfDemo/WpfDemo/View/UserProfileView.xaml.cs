using Syncfusion.UI.Xaml.Charts;
using System;
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

        private void NumericalAxis_LabelCreated(object sender, LabelCreatedEventArgs e)
        {
            TimeSpan Value = TimeSpan.Parse(e.AxisLabel.LabelContent as string);
            DateTime date = (new DateTime(2000, 1, 1).AddMilliseconds(Value.TotalMilliseconds));
            e.AxisLabel.LabelContent = String.Format("{0:HH:mm}", date);
        }
    }
}
