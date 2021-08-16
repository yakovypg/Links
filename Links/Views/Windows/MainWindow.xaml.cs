using System.Windows;

namespace Links
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void GroupsListBox_ScrollIntoView(object item)
        {
            GroupsListBox.ScrollIntoView(item);
        }

        public void GroupsListBox_ScrollToSelectedGroup()
        {
            if (!(DataContext is ViewModels.MainWindowViewModel mainVm))
                return;

            var selectedGroup = mainVm.LinkCollectionVM?.SelectedGroup;

            if (selectedGroup == null)
                return;

            GroupsListBox.ScrollIntoView(selectedGroup);
        }

        public static void InvokeMethod(string name, object[] parameters)
        {
            if (!(Application.Current.MainWindow is MainWindow mainWindow))
                return;

            try
            {
                mainWindow.GetType().GetMethod(name)?.Invoke(mainWindow, parameters);
            }
            catch { }
        }

        public static void InvokeMethod(string name, object[] parameters, int timespan)
        {
            if (!(Application.Current?.MainWindow is MainWindow mainWindow))
                return;

            System.Threading.Tasks.Task.Run(delegate
            {
                try
                {
                    System.Threading.Thread.Sleep(timespan);
                    mainWindow.Dispatcher.Invoke(() => InvokeMethod(name, parameters));
                }
                catch { }
            });
        }
    }
}
