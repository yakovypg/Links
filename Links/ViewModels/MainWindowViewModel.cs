using Links.Data;
using Links.Data.App;
using Links.Infrastructure.Commands;
using Links.Models;
using Links.Models.Collections;
using Links.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Links.ViewModels
{
    internal class MainWindowViewModel : CustomizedViewModel
    {
        public static StringSaver StringSaver { get; } = new StringSaver();

        public SettingsViewModel SettingsVM { get; }
        public LinkCollectionViewModel LinkCollectionVM { get; }

        public GroupCreator GroupCreator { get; } = new GroupCreator();
        public LinkCreator LinkCreator { get; } = new LinkCreator();

        private string _title = AppInfo.Name;
        public string Title
        {
            get => _title;
            private set => SetValue(ref _title, value);
        }

        private int _linkCreatorGroupIndex = 0;
        public int LinkCreatorGroupIndex
        {
            get => _linkCreatorGroupIndex;
            set => SetValue(ref _linkCreatorGroupIndex, value);
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
            if (_linkCreatorMenuVisibility == Visibility.Visible)
                LinkCreatorMenuVisibility = Visibility.Hidden;

            GroupCreatorMenuVisibility = _groupCreatorMenuVisibility == Visibility.Hidden
                ? Visibility.Visible
                : Visibility.Hidden;
        }

        public ICommand ChangeLinkCreatorMenuVisibilityCommand { get; }
        public void OnChangeLinkCreatorMenuVisibilityCommandExecuted(object parameter)
        {
            if (_groupCreatorMenuVisibility == Visibility.Visible)
                GroupCreatorMenuVisibility = Visibility.Hidden;

            LinkCreatorMenuVisibility = _linkCreatorMenuVisibility == Visibility.Hidden
                ? Visibility.Visible
                : Visibility.Hidden;
        }

        #endregion

        #region Commands

        public ICommand SetLinkCreatorImageCommand { get; }
        public void OnSetLinkCreatorImageCommandExecuted(object parameter)
        {
            string path = DialogProvider.GetFilePath();

            int width = _maxLinkPresenterGridWidth - 3 - 2 * 2;
            int height = _maxLinkPresenterGridHeight - 3 - 20 - 22 - 2 * 2;
            var size = new System.Drawing.Size(width, height);

            if (ImageTransformer.TryGetBitmapImage(path, size, out System.Windows.Media.Imaging.BitmapImage newImage))
                LinkCreator.BackgroundImage = newImage;
        }

        public ICommand AddGroupCommand { get; }
        public void OnAddGroupCommandExecuted(object parameter)
        {
            Group group = GroupCreator.CreateGroup();
            LinkCollectionVM.GroupCollection.Add(group);

            ChangeGroupCreatorMenuVisibilityCommand.Execute(null);
        }

        public ICommand AddLinkCommand { get; }
        public void OnAddLinkCommandExecuted(object parameter)
        {
            LinkInfo linkInfo = LinkCreator.CreateLink();
            linkInfo.ParentGroup.Links.Add(linkInfo);

            ChangeLinkCreatorMenuVisibilityCommand.Execute(null);
        }

        public ICommand ResetLinkCreatorGroupIndexCommand { get; }
        public void OnResetLinkCreatorGroupIndexCommandExecuted(object parameter)
        {
            LinkCreatorGroupIndex = 0;
        }

        public ICommand FindAllLinksCommand { get; }
        public void OnFindAllLinksCommandExecuted(object parameter)
        {
            if (!(parameter is string data))
                return;

            if (!string.IsNullOrEmpty(data))
                StringSaver.Add(data);

            var linkFinder = new LinkFinder(data);
            var foundLinksList = linkFinder.FindAmong(LinkCollectionVM.GroupCollection);
            var groupItems = new ObservableCollection<LinkInfo>(foundLinksList);

            LinkCollectionVM.LinkFilterText = string.Empty;
            LinkCollectionVM.SelectedGroup = new Group(null, groupItems);
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

            SetLinkCreatorImageCommand = new RelayCommand(OnSetLinkCreatorImageCommandExecuted, t => true);
            AddGroupCommand = new RelayCommand(OnAddGroupCommandExecuted, t => true);
            AddLinkCommand = new RelayCommand(OnAddLinkCommandExecuted, t => true);
            ResetLinkCreatorGroupIndexCommand = new RelayCommand(OnResetLinkCreatorGroupIndexCommandExecuted, t => true);
            FindAllLinksCommand = new RelayCommand(OnFindAllLinksCommandExecuted, t => true);

            ChangeSettingsFieldVisibilityCommand = new RelayCommand(OnChangeSettingsFieldVisibilityCommandExecuted, t => true);
            ChangeGroupCreatorMenuVisibilityCommand = new RelayCommand(OnChangeGroupCreatorMenuVisibilityCommandExecuted, t => true);
            ChangeLinkCreatorMenuVisibilityCommand = new RelayCommand(OnChangeLinkCreatorMenuVisibilityCommandExecuted, t => true);
        }
    }
}
