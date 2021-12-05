using System.Windows;
using System.Windows.Controls;

namespace WpfDemo.Components
{
    /// <summary>
    /// Interaction logic for BindablePasswordBox.xaml
    /// </summary>
    public partial class BindablePasswordBox : UserControl
    {
        private bool _isPasswordChanging;
       
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(BindablePasswordBox), new PropertyMetadata(string.Empty, PasswordPropertyChanged));

        private static void PasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is BindablePasswordBox passwordBox)
            {
                passwordBox.UpdatePassowrd();
            }
        }

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        public BindablePasswordBox()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _isPasswordChanging = true;
            Password = passwordBox.Password;
            _isPasswordChanging = false;
        }

        private void UpdatePassowrd()
        {
            if(!_isPasswordChanging)//_isPasswordChanging == false
            {
                passwordBox.Password = Password;
            }
        }
    }
}
