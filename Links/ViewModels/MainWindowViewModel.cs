using Links.Infrastructure.Commands;
using Links.ViewModels.Base;
using System.Windows;
using System.Windows.Input;

namespace Links.ViewModels
{
    internal class MainWindowViewModel : CustomizedViewModel
    {
        public SettingsViewModel SettingsVM { get; }
        public LinkCollectionViewModel LinkCollectionVM { get; }

        private string _title = "Links";
        public string Title
        {
            get => _title;
            set => SetValue(ref _title, value);
        }

        #region FieldsVisibility

        private Visibility _settingsFieldVisibility = Visibility.Hidden;
        public Visibility SettingsFieldVisibility
        {
            get => _settingsFieldVisibility;
            set => SetValue(ref _settingsFieldVisibility, value);
        }

        private Visibility _groupCreatorMenuVisibility = Visibility.Hidden;
        public Visibility GroupCreatorMenuVisibility
        {
            get => _groupCreatorMenuVisibility;
            set => SetValue(ref _groupCreatorMenuVisibility, value);
        }

        private Visibility _linkCreatorMenuVisibility = Visibility.Hidden;
        public Visibility LinkCreatorMenuVisibility
        {
            get => _linkCreatorMenuVisibility;
            set => SetValue(ref _linkCreatorMenuVisibility, value);
        }

        #endregion

        #region VisibilityCommands

        public ICommand ShowSettingsPageCommand { get; }
        public void OnShowSettingsPageCommandExecuted(object parameter)
        {
            SettingsFieldVisibility = _settingsFieldVisibility == Visibility.Hidden
                ? Visibility.Visible
                : Visibility.Hidden;
        }

        public ICommand ShowGroupCreatorCommand { get; }
        public void OnShowGroupCreatorCommandExecuted(object parameter)
        {
            if (_groupCreatorMenuVisibility == Visibility.Visible)
                return;

            if (_linkCreatorMenuVisibility == Visibility.Visible)
                LinkCreatorMenuVisibility = Visibility.Hidden;

            GroupCreatorMenuVisibility = Visibility.Visible;
        }

        public ICommand ShowLinkCreatorCommand { get; }
        public void OnShowLinkCreatorCommandExecuted(object parameter)
        {
            if (_linkCreatorMenuVisibility == Visibility.Visible)
                return;

            if (_groupCreatorMenuVisibility == Visibility.Visible)
                GroupCreatorMenuVisibility = Visibility.Hidden;

            LinkCreatorMenuVisibility = Visibility.Visible;
        }

        #endregion

        #region SystemCommands

        public ICommand MinimizeWindowCommand { get; }
        public ICommand MaximizeWindowCommand { get; }
        public ICommand CloseWindowCommand { get; }

        #endregion

        public MainWindowViewModel()
        {
            SettingsVM = new SettingsViewModel();
            LinkCollectionVM = new LinkCollectionViewModel();

            MinimizeWindowCommand = new MinimizeWindowCommand();
            MaximizeWindowCommand = new MaximizeWindowCommand();
            CloseWindowCommand = new CloseWindowCommand();

            ShowSettingsPageCommand = new RelayCommand(OnShowSettingsPageCommandExecuted, t => true);
            ShowGroupCreatorCommand = new RelayCommand(OnShowGroupCreatorCommandExecuted, t => true);
            ShowLinkCreatorCommand = new RelayCommand(OnShowLinkCreatorCommandExecuted, t => true);
        }
    }
}
