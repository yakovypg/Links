using Links.Infrastructure.Commands;
using Links.Models;
using Links.ViewModels.Base;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Links.ViewModels
{
    internal class MainWindowViewModel : CustomizedViewModel
    {
        public SettingsViewModel SettingsVM { get; }
        public LinkCollectionViewModel LinkCollectionVM { get; }

        public GroupCreator GroupCreator { get; }
        public LinkCreator LinkCreator { get; }

        public IEnumerable<string> GroupIconColors => LinkCollectionVM.GroupIconColors;

        private string _title = "Links";
        public string Title
        {
            get => _title;
            private set => SetValue(ref _title, value);
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

        public ICommand ChangeSettingsFieldVisibilityCommand { get; }
        public void OnChangeSettingsFieldVisibilityCommandExecuted(object parameter)
        {
            SettingsFieldVisibility = _settingsFieldVisibility == Visibility.Hidden
                ? Visibility.Visible
                : Visibility.Hidden;
        }

        public ICommand ChangeGroupCreatorMenuVisibilityCommand { get; }
        public void OnChangeGroupCreatorMenuVisibilityCommandExecuted(object parameter)
        {
            if (_groupCreatorMenuVisibility == Visibility.Visible)
                return;

            if (_linkCreatorMenuVisibility == Visibility.Visible)
                LinkCreatorMenuVisibility = Visibility.Hidden;

            GroupCreatorMenuVisibility = Visibility.Visible;
        }

        public ICommand ChangeLinkCreatorMenuVisibilityCommand { get; }
        public void OnChangeLinkCreatorMenuVisibilityCommandExecuted(object parameter)
        {
            if (_linkCreatorMenuVisibility == Visibility.Visible)
                return;

            if (_groupCreatorMenuVisibility == Visibility.Visible)
                GroupCreatorMenuVisibility = Visibility.Hidden;

            LinkCreatorMenuVisibility = Visibility.Visible;
        }

        #endregion

        #region Commands

        public ICommand SetLinkCreatorImageCommand { get; }

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

            GroupCreator = new GroupCreator();
            LinkCreator = new LinkCreator();

            MinimizeWindowCommand = new MinimizeWindowCommand();
            MaximizeWindowCommand = new MaximizeWindowCommand();
            CloseWindowCommand = new CloseWindowCommand();

            ChangeSettingsFieldVisibilityCommand = new RelayCommand(OnChangeSettingsFieldVisibilityCommandExecuted, t => true);
            ChangeGroupCreatorMenuVisibilityCommand = new RelayCommand(OnChangeGroupCreatorMenuVisibilityCommandExecuted, t => true);
            ChangeLinkCreatorMenuVisibilityCommand = new RelayCommand(OnChangeLinkCreatorMenuVisibilityCommandExecuted, t => true);
        }
    }
}
