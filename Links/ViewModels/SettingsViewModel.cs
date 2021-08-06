using Links.Data;
using Links.Data.App;
using Links.Infrastructure.Commands;
using Links.Models.Collections;
using Links.Models.Configuration;
using Links.Models.Localization;
using Links.Models.Themes;
using Links.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Links.ViewModels
{
    internal class SettingsViewModel : ViewModel
    {
        public MainWindowViewModel MainWindowVM { get; }
        public ObservableCollection<LinkInfo> RecycleBin { get; }

        public ObservableCollection<string> GroupSortings { get; private set; }
        public ObservableCollection<string> LinkSortings { get; private set; }
        public ObservableCollection<string> SortingMods { get; private set; }
        public ObservableCollection<Size> PresenterSizes { get; private set; }
        public ObservableCollection<IWindowTheme> Themes { get; private set; }
        public ObservableCollection<ILocale> Locales { get; private set; }
        public ObservableCollection<string> OnOffParams { get; private set; }
        public ObservableCollection<string> EmptyRecycleBinParams { get; private set; }

        private LinkInfo _selectedDeletedLink;
        public LinkInfo SelectedDeletedLink
        {
            get => _selectedDeletedLink;
            set => SetValue(ref _selectedDeletedLink, value);
        }

        private int _selectedDeletedLinkIndex;
        public int SelectedDeletedLinkIndex
        {
            get => _selectedDeletedLinkIndex;
            set => SetValue(ref _selectedDeletedLinkIndex, value);
        }

        #region SettingFieldsGetters

        public int MaxLinkPresenterGridWidth => 270;
        public int MaxLinkPresenterGridHeight => 414;

        public double LinkPresenterGridWidth => PresenterSize.Width;
        public double LinkPresenterGridHeight => PresenterSize.Height;

        public bool IsWarningsEnable => _warningsParam == MainWindowVM.CurrentLocale.On;
        public bool IsRecycleBinEnable => _recycleBinParam == MainWindowVM.CurrentLocale.On;

        public SortDescription GroupSortDescription => new SortDescription(GroupSortPropertyName, GroupListSortDescription);
        public SortDescription LinkSortDescription => new SortDescription(LinkSortPropertyName, LinkListSortDescription);

        public ListSortDirection GroupListSortDescription => _groupListSortDescriptionParam == MainWindowVM.CurrentLocale.Ascending
            ? ListSortDirection.Ascending : ListSortDirection.Descending;

        public ListSortDirection LinkListSortDescription => _groupListSortDescriptionParam == MainWindowVM.CurrentLocale.Ascending
            ? ListSortDirection.Ascending : ListSortDirection.Descending;

        public int EmptyRecycleBinDifference
        {
            get
            {
                int index = _emptyRecycleBinParam?.LastIndexOf(" ") ?? -1;

                if (index < 0 || index >= _emptyRecycleBinParam.Length - 1)
                    return int.MaxValue;

                string numStr = _emptyRecycleBinParam.Substring(index + 1);

                return !int.TryParse(numStr, out int diff)
                    ? int.MaxValue
                    : diff;
            }
        }

        #endregion

        #region SettingFields

        private string _groupSortPropertyName;
        public string GroupSortPropertyName
        {
            get => _groupSortPropertyName;
            set => SetValue(ref _groupSortPropertyName, value);
        }

        private string _groupListSortDescriptionParam;
        public string GroupListSortDescriptionParam
        {
            get => _groupListSortDescriptionParam;
            set => SetValue(ref _groupListSortDescriptionParam, value);
        }

        private string _linkSortPropertyName;
        public string LinkSortPropertyName
        {
            get => _linkSortPropertyName;
            set => SetValue(ref _linkSortPropertyName, value);
        }

        private string _linkListSortDescriptionParam;
        public string LinkListSortDescriptionParam
        {
            get => _linkListSortDescriptionParam;
            set => SetValue(ref _linkListSortDescriptionParam, value);
        }

        private string _warningsParam;
        public string WarningsParam
        {
            get => _warningsParam;
            set => SetValue(ref _warningsParam, value);
        }

        private Size _presenterSize;
        public Size PresenterSize
        {
            get => _presenterSize;
            set => SetValue(ref _presenterSize, value);
        }

        private string _recycleBinParam;
        public string RecycleBinParam
        {
            get => _recycleBinParam;
            set => SetValue(ref _recycleBinParam, value);
        }

        private string _emptyRecycleBinParam;
        public string EmptyRecycleBinParam
        {
            get => _emptyRecycleBinParam;
            set => SetValue(ref _emptyRecycleBinParam, value);
        }

        #endregion

        #region Commands

        public ICommand ChangeGroupsSortingCommand { get; }

        #endregion

        #region ButtonCommands

        public ICommand CloseSettingsPageCommand { get; }
        public void OnCloseSettingsPageCommandExecuted(object parameter)
        {
            MainWindowVM.ChangeSettingsFieldVisibilityCommand?.Execute(null);
        }

        public ICommand ImportLinksCommand { get; }
        public ICommand ExportLinksCommand { get; }

        public ICommand RestoreRecycleBinItemCommand { get; }
        public bool CanRestoreRecycleBinItemCommandExecute(object parameter)
        {
            return SelectedDeletedLink != null;
        }
        public void OnRestoreRecycleBinItemCommandExecuted(object parameter)
        {
            LinkInfo selectedLink = SelectedDeletedLink;

            if (!RecycleBin.Remove(SelectedDeletedLink))
            {
                _ = MessageBox.Show(MainWindowVM.CurrentLocale.LocaleMessages.RestoreLinkError, MainWindowVM.CurrentLocale.Error,
                        MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            ObservableCollection<Group> groupCollection = MainWindowVM.LinkCollectionVM.GroupCollection;

            if (groupCollection.Contains(selectedLink.ParentGroup))
            {
                selectedLink.ParentGroup.Links.Add(selectedLink);
            }
            else
            {
                selectedLink.ParentGroup.Links.Clear();
                selectedLink.ParentGroup.Links.Add(selectedLink);
                groupCollection.Add(selectedLink.ParentGroup);
            }
        }

        public ICommand RemoveRecycleBinItemCommand { get; }
        public bool CanRemoveRecycleBinItemCommandExecute(object parameter)
        {
            return SelectedDeletedLink != null;
        }
        public void OnRemoveRecycleBinItemCommandExecuted(object parameter)
        {
            _ = RecycleBin.Remove(SelectedDeletedLink);
        }

        public ICommand EmptyRecycleBinCommand { get; }
        public bool CanEmptyRecycleBinCommandExecute(object parameter)
        {
            return RecycleBin.Count > 0;
        }
        public void OnEmptyRecycleBinCommandExecuted(object parameter)
        {
            RecycleBin.Clear();
        }

        #endregion

        public SettingsViewModel(MainWindowViewModel mainWindowVM)
        {
            MainWindowVM = mainWindowVM ?? throw new ArgumentNullException();

            IEnumerable<LinkInfo> recBinItems = DataParser.GetRecycleBin();

            RecycleBin = recBinItems != null
                ? new ObservableCollection<LinkInfo>(recBinItems)
                : new ObservableCollection<LinkInfo>();

            ChangeGroupsSortingCommand = new RelayCommand(delegate { }, t => true);

            CloseSettingsPageCommand = new RelayCommand(OnCloseSettingsPageCommandExecuted, t => true);
            ImportLinksCommand = new RelayCommand(delegate { }, t => true);
            ExportLinksCommand = new RelayCommand(delegate { }, t => true);

            RestoreRecycleBinItemCommand = new RelayCommand(OnRestoreRecycleBinItemCommandExecuted, CanRestoreRecycleBinItemCommandExecute);
            RemoveRecycleBinItemCommand = new RelayCommand(OnRemoveRecycleBinItemCommandExecuted, CanRemoveRecycleBinItemCommandExecute);
            EmptyRecycleBinCommand = new RelayCommand(OnEmptyRecycleBinCommandExecuted, CanEmptyRecycleBinCommandExecute);

            SetSettingsItems();
            RestoreSettings();
        }

        private void RestoreSettings()
        {
            ISettings settings = DataParser.GetSettings();

            GroupSortPropertyName = settings.GroupSortPropertyName;
            GroupListSortDescriptionParam = settings.GroupListSortDescriptionParam;
            LinkSortPropertyName = settings.LinkSortPropertyName;
            LinkListSortDescriptionParam = settings.LinkListSortDescriptionParam;
            WarningsParam = settings.WarningsParam;
            PresenterSize = settings.PresenterSize;
            MainWindowVM.Theme = settings.Theme;
            MainWindowVM.CurrentLocale = settings.CurrentLocale;
            RecycleBinParam = settings.RecycleBinParam;
            EmptyRecycleBinParam = settings.EmptyRecycleBinParam;
        }

        private void SetSettingsItems()
        {
            string groupSortPropertyName = GroupSortPropertyName;
            string groupListSortDescriptionParam = GroupListSortDescriptionParam;
            string linkSortPropertyName = LinkSortPropertyName;
            string linkListSortDescriptionParam = LinkListSortDescriptionParam;
            string warningsParam = WarningsParam;
            Size presenterSize = PresenterSize;
            IWindowTheme theme = MainWindowVM.Theme;
            ILocale locale = MainWindowVM.CurrentLocale;
            string recycleBinParam = RecycleBinParam;
            string emptyRecycleBinParam = EmptyRecycleBinParam;

            var settingsItems = new SettingsItems(MainWindowVM.CurrentLocale);

            GroupSortings = settingsItems.GetGroupSortings();
            LinkSortings = settingsItems.GetLinkSortings();
            SortingMods = settingsItems.GetSortingMods();
            PresenterSizes ??= settingsItems.GetPresenterSizes();
            Themes ??= settingsItems.GetThemes();
            Locales ??= settingsItems.GetLocales();
            OnOffParams = settingsItems.GetOnOffParams();
            EmptyRecycleBinParams = settingsItems.GetEmptyRecycleBinParams();

            GroupSortPropertyName = groupSortPropertyName;
            GroupListSortDescriptionParam = groupListSortDescriptionParam;
            LinkSortPropertyName = linkSortPropertyName;
            LinkListSortDescriptionParam = linkListSortDescriptionParam;
            WarningsParam = warningsParam;
            PresenterSize = presenterSize;
            MainWindowVM.Theme = theme;
            MainWindowVM.CurrentLocale = locale;
            RecycleBinParam = recycleBinParam;
            EmptyRecycleBinParam = emptyRecycleBinParam;
        }
    }
}
