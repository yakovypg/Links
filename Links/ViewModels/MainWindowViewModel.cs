using Links.Data;
using Links.Data.App;
using Links.Data.Imaging;
using Links.Infrastructure.Commands;
using Links.Models;
using Links.Models.Collections;
using Links.Models.Collections.Creators;
using Links.Models.Configuration;
using Links.Models.Localization;
using Links.Models.Messages;
using Links.Models.Themes;
using Links.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Links.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private bool _closeProgramWithoutSaving = false;

        public static StringSaver StringSaver { get; } = new StringSaver();

        public SettingsViewModel SettingsVM { get; private set; }
        public LinkCollectionViewModel LinkCollectionVM { get; private set; }

        #region Creators

        public GroupCreator GroupCreator { get; } = new GroupCreator();
        public LinkCreator LinkCreator { get; } = new LinkCreator();

        private int _linkCreatorGroupIndex = 0;
        public int LinkCreatorGroupIndex
        {
            get => _linkCreatorGroupIndex;
            set => SetValue(ref _linkCreatorGroupIndex, value);
        }

        #endregion

        #region Customization

        private IWindowTheme _theme = WindowTheme.Dark;
        public IWindowTheme Theme
        {
            get => _theme;
            set => SetValue(ref _theme, value);
        }

        private ILocale _locale = Locale.English;
        public ILocale CurrentLocale
        {
            get => _locale;
            set => SetValue(ref _locale, value);
        }

        private string _title = AppInfo.Name;
        public string Title
        {
            get => _title;
            private set => SetValue(ref _title, value);
        }

        #endregion

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
            DialogProvider.GetFilePath(out string path);

            int width = (int)SettingsVM.MaxLinkPresenterGridWidth - 3 - 2 * 2;
            int height = (int)SettingsVM.MaxLinkPresenterGridHeight - 3 - 20 - 22 - 2 * 2;
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
            LinkCollectionVM.SelectedGroupIndex = -1;
            LinkCollectionVM.SelectedGroup = new Group(null, groupItems);
        }

        #endregion

        #region ProgramStateCommands

        public ICommand LoadProgramStateCommand { get; }
        public void OnLoadProgramStateCommandExecuted(object parameter)
        {
            bool isSettingsLoaded = false;
            bool isGroupsLoaded = false;
            bool isRecycleBinLoaded = false;

            bool isStateLoaded = false;
            bool closeProgram = false;

            Settings settings = null;
            IEnumerable<Group> loadedGroups = null;
            IEnumerable<LinkInfo> loadedRecycleBin = null;

            while (!isStateLoaded && !closeProgram)
            {
                if (!isSettingsLoaded)
                    isSettingsLoaded = DataOrganizer.TryGetSettings(out settings);

                if (!isGroupsLoaded)
                    isGroupsLoaded = DataOrganizer.TryGetGroups(out loadedGroups, out Exception ex) || ex is System.IO.FileNotFoundException;

                if (!isRecycleBinLoaded)
                    isRecycleBinLoaded = DataOrganizer.TryGetRecycleBin(out loadedRecycleBin);

                isStateLoaded = isSettingsLoaded && isGroupsLoaded && isRecycleBinLoaded;

                if (!isStateLoaded)
                {
                    var res = new QuickMessage(CurrentLocale.LocaleMessages.LoadProgramStateQuestion, CurrentLocale).GetErrorResult(MessageBoxButton.YesNo);
                    closeProgram = res == MessageBoxResult.No;
                }
            }

            if (closeProgram)
            {
                _closeProgramWithoutSaving = true;
                Application.Current.Shutdown();
            }

            var groupOrganizer = new GroupOrganizer();
            groupOrganizer.RedefineParentsForLinks(loadedGroups);

            string currYear = DateTime.Now.Year.ToString();

            var groups = loadedGroups != null && loadedGroups.Count() > 0
                ? new ObservableCollection<Group>(loadedGroups)
                : new ObservableCollection<Group>(new Group[] { new Group(currYear) });

            var recycleBin = loadedRecycleBin != null && loadedRecycleBin.Count() > 0
                ? new ObservableCollection<LinkInfo>(loadedRecycleBin)
                : new ObservableCollection<LinkInfo>();

            LinkCollectionVM = new LinkCollectionViewModel(groups, this);
            SettingsVM = new SettingsViewModel(settings, recycleBin, this);

            SettingsVM.CheckLastRecycleBinCleaning(true);

            OnPropertyChanged("LinkCollectionVM");
            OnPropertyChanged("SettingsVM");
        }

        public ICommand SaveProgramStateCommand { get; }
        public void OnSaveProgramStateCommandExecuted(object parameter)
        {
            if (_closeProgramWithoutSaving)
                return;

            bool isSettingsSaved = false;
            bool isGroupsSaved = false;
            bool isRecycleBinSaved = false;
            bool isLastRecycleBinCleaningDateSaved = false;

            bool isStateSaved = false;
            bool exitWithoutSaving = false;

            while (!isStateSaved && !exitWithoutSaving)
            {
                if (!isSettingsSaved)
                    isSettingsSaved = DataOrganizer.TrySaveSettings(SettingsVM.CurrentSettings);

                if (!isGroupsSaved)
                    isGroupsSaved = DataOrganizer.TrySaveGroups(LinkCollectionVM.GroupCollection);

                if (!isRecycleBinSaved)
                    isRecycleBinSaved = DataOrganizer.TrySaveRecycleBin(SettingsVM.RecycleBin);

                if (!isLastRecycleBinCleaningDateSaved)
                    isLastRecycleBinCleaningDateSaved = DataOrganizer.TrySaveLastRecycleBinCleaningDate(SettingsVM.LastRecycleBinCleaningDate);

                isStateSaved = isSettingsSaved && isGroupsSaved && isRecycleBinSaved && isLastRecycleBinCleaningDateSaved;

                if (!isStateSaved)
                {
                    var res = new QuickMessage(CurrentLocale.LocaleMessages.SaveProgramStateQuestion, CurrentLocale).GetErrorResult(MessageBoxButton.YesNo);
                    exitWithoutSaving = res == MessageBoxResult.No;
                }
            }
        }

        #endregion

        #region SystemCommands

        public ICommand MinimizeWindowCommand { get; }
        public ICommand MaximizeWindowCommand { get; }
        public ICommand CloseWindowCommand { get; }

        #endregion

        public MainWindowViewModel()
        {
            MinimizeWindowCommand = new MinimizeWindowCommand();
            MaximizeWindowCommand = new MaximizeWindowCommand();
            CloseWindowCommand = new CloseWindowCommand();

            LoadProgramStateCommand = new RelayCommand(OnLoadProgramStateCommandExecuted, t => true);
            SaveProgramStateCommand = new RelayCommand(OnSaveProgramStateCommandExecuted, t => true);

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
