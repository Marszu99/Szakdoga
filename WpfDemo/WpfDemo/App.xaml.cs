using System.Globalization;
using System.Threading;
using System.Windows;

namespace WpfDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjExMjE0QDMyMzAyZTMxMmUzMEF6OWRWdXpyeWl4MGR3NzdEbDFQWjVrSmltdTZvZnZMbU95d1NoNWJ6YWM9"); // https://www.serkanseker.com/syncfusion-community-license-key/
            InitializeComponent();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            CultureInfo cultureInfo = new CultureInfo("en-US");
            cultureInfo.DateTimeFormat.ShortDatePattern = "yyyy.MM.dd";
            cultureInfo.DateTimeFormat.LongDatePattern = "yyyy.MM.dd HH:mm";
            cultureInfo.DateTimeFormat.FullDateTimePattern = "yyyy.MM.dd HH:mm";
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture = cultureInfo;
        }
    }
}
