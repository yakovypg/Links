using System.Windows;

namespace Links
{
    public partial class App : Application
    {
        //internal readonly MainWindowViewModel mainVM = new MainWindowViewModel();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var window = new MainWindow()
            {
                //DataContext = mainVM
            };

            window.Show();
        }
    }
}
