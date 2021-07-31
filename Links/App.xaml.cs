using System.Windows;
//using Links.ViewModels;

namespace Links
{
    public partial class App : Application
    {
        //private readonly MainWindowViewModel mainVM = new MainWindowViewModel();

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
