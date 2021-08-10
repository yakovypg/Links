using Links.Data;
using Links.Data.App;
using Links.Infrastructure.Commands;
using Links.Models.Collections;
using Links.Models.Collections.Comparers;
using Links.Models.Configuration;
using Links.Models.Localization;
using Links.Models.Messages;
using Links.Models.Themes;
using Links.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Links.ViewModels
{
    internal class SettingsViewModel : ViewModel
    {
        private ILocale CurrLocale => MainWindowVM.CurrentLocale;

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

        private int _selectedDeletedLinkIndex;
        public int SelectedDeletedLinkIndex
        {
            get => _selectedDeletedLinkIndex;
            set => SetValue(ref _selectedDeletedLinkIndex, value);
        }

        private LinkInfo _selectedDeletedLink;
        public LinkInfo SelectedDeletedLink
        {
            get => _selectedDeletedLink;
            set => SetValue(ref _selectedDeletedLink, value);
        }

        private GroupViewModel[] _impexGroups;
        public GroupViewModel[] ImpexGroups
        {
            get => _impexGroups;
            private set => SetValue(ref _impexGroups, value);
        }

        public (IEnumerable<Group> NonemptyImpexGroups, IEnumerable<LinkInfo> SelectedImpexGroupsLinks) ImpexLinksTreeData
        {
            get
            {
                var nonemptyGroups = new List<Group>();
                var selectedImpexGroupsLinks = new List<LinkInfo>();

                if (ImpexGroups == null)
                    return (null, null);

                foreach (GroupViewModel group in ImpexGroups)
                {
                    if (group.Links == null)
                        continue;

                    IEnumerable<LinkInfo> links = group.Links.Where(t => t.IsChecked).Select(t => t.Source);

                    if (links == null || links.Count() == 0)
                        continue;

                    var icon = group.Icon.Clone() as GroupIcon;
                    var items = new ObservableCollection<LinkInfo>(links);
                    var newGroup = new Group(group.Name, icon, items);

                    nonemptyGroups.Add(newGroup);
                    selectedImpexGroupsLinks.AddRange(links);
                }

                return (nonemptyGroups, selectedImpexGroupsLinks);
            }
        }

        #region FieldsVisibility

        private Visibility _impexMenuVisibility = Visibility.Hidden;
        public Visibility ImpexMenuVisibility
        {
            get => _impexMenuVisibility;
            set => SetValue(ref _impexMenuVisibility, value);
        }

        private Visibility _exportLinksBottomBarVisibility = Visibility.Hidden;
        public Visibility ExportLinksBottomBarVisibility
        {
            get => _exportLinksBottomBarVisibility;
            set => SetValue(ref _exportLinksBottomBarVisibility, value);
        }

        private Visibility _importLinksBottomBarVisibility = Visibility.Hidden;
        public Visibility ImportLinksBottomBarVisibility
        {
            get => _importLinksBottomBarVisibility;
            set => SetValue(ref _importLinksBottomBarVisibility, value);
        }

        #endregion

        #region SettingFieldsGetters

        public double MaxLinkPresenterGridWidth => PresenterSizes[PresenterSizes.Count - 1].Width;
        public double MaxLinkPresenterGridHeight => PresenterSizes[PresenterSizes.Count - 1].Height;

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

        public double EmptyRecycleBinDifference
        {
            get
            {
                int index = _emptyRecycleBinParam?.LastIndexOf(" ") ?? -1;

                if (index < 0 || index >= _emptyRecycleBinParam.Length - 1)
                    return double.PositiveInfinity;

                string numStr = _emptyRecycleBinParam.Substring(index + 1);

                return !double.TryParse(numStr, out double diff)
                    ? double.PositiveInfinity
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

        public ICommand ImportLinksCommand { get; }
        public void OnImportLinksCommandExecuted(object parameter)
        {
            (IEnumerable<Group> nonemptyImpexGroups, IEnumerable<LinkInfo> selectedImpexGroupsLinks) = ImpexLinksTreeData;

            if (selectedImpexGroupsLinks == null || selectedImpexGroupsLinks.Count() == 0)
            {
                new FastMessage(CurrLocale.LocaleMessages.NoSelectedLinksInfo, CurrLocale).ShowInformation();
                return;
            }

            var groupAnalyser = new GroupAnalyzer();
            nonemptyImpexGroups = groupAnalyser.RemoveExistingLinks(nonemptyImpexGroups, MainWindowVM.LinkCollectionVM.GroupCollection, true);

            if (nonemptyImpexGroups == null || nonemptyImpexGroups.Count() == 0)
            {
                new FastMessage(CurrLocale.LocaleMessages.SuccessfulLinksImportingInfo, CurrLocale).ShowInformation();
                return;
            }

            MessageBoxResult msgResult = new FastMessage(CurrLocale.LocaleMessages.AutoLinksDistributionQuestion, CurrLocale).GetQuestionResult();

            ObservableCollection<Group> groups = MainWindowVM.LinkCollectionVM.GroupCollection;

            if (msgResult == MessageBoxResult.Yes)
            {
                var designComparer = new GroupDesignEqualityComparer();

                foreach (Group group in nonemptyImpexGroups)
                {
                    Group availableGroup = groups.FirstOrDefault(t => designComparer.Equals(t, group));

                    if (availableGroup == null)
                        groups.Add(group);
                    else
                        availableGroup.MergeWith(group);
                }
            }
            else
            {
                var nonIconComparer = new GroupNonIconEqualityComparer();
                var links = new ObservableCollection<LinkInfo>(groupAnalyser.GetAllLinks(nonemptyImpexGroups));
                var groupUnsorted = new Group(MainWindowVM.CurrentLocale.Unsorted, links);

                Group availableGroup = groups.FirstOrDefault(t => nonIconComparer.Equals(t, groupUnsorted));

                if (availableGroup == null)
                    groups.Add(groupUnsorted);
                else
                    availableGroup.MergeWith(groupUnsorted);
            }

            MainWindowVM.LinkCollectionVM.UpdateSelectedGroup();

            new FastMessage(CurrLocale.LocaleMessages.SuccessfulLinksImportingInfo, CurrLocale).ShowInformation();
        }

        public ICommand ExportLinksCommand { get; }
        public void OnExportLinksCommandExecuted(object parameter)
        {
            (IEnumerable<Group> nonemptyImpexGroups, IEnumerable<LinkInfo> selectedImpexGroupsLinks) = ImpexLinksTreeData;

            if (selectedImpexGroupsLinks == null || selectedImpexGroupsLinks.Count() == 0)
            {
                new FastMessage(CurrLocale.LocaleMessages.NoSelectedLinksInfo, CurrLocale).ShowInformation();
                return;
            }

            bool res = DataParser.TryExportGroups(nonemptyImpexGroups, out System.Windows.Forms.DialogResult dialogResult);

            if (res)
            {
                new FastMessage(CurrLocale.LocaleMessages.SuccessfulLinksExportingInfo, CurrLocale).ShowInformation();
            }
            else if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                new FastMessage(CurrLocale.LocaleMessages.LinksExportingError, CurrLocale).ShowError();
            }
        }

        public ICommand CheckAllLinksCommand { get; }
        public void OnCheckAllLinksCommandExecuted(object parameter)
        {
            if (ImpexGroups == null)
                return;

            foreach (GroupViewModel group in ImpexGroups)
            {
                if (group.Links == null)
                    continue;

                foreach (LinkViewModel link in group.Links)
                {
                    link.IsChecked = true;
                }
            }

            OnPropertyChanged(nameof(ImpexGroups));
        }

        public ICommand ChangeGroupsSortingCommand { get; }

        #endregion

        #region VisibilityCommands

        public ICommand ChangeImportLinksBottomBarVisibilityCommand { get; }
        public void OnChangeImportLinksBottomBarVisibilityCommandExecuted(object parameter)
        {
            ImportLinksBottomBarVisibility = ImportLinksBottomBarVisibility == Visibility.Hidden
                ? Visibility.Visible
                : Visibility.Hidden;

            ImpexMenuVisibility = ImportLinksBottomBarVisibility;
        }

        public ICommand ChangeExportLinksBottomBarVisibilityCommand { get; }
        public void OnChangeExportLinksBottomBarVisibilityCommandExecuted(object parameter)
        {
            ExportLinksBottomBarVisibility = ExportLinksBottomBarVisibility == Visibility.Hidden
                ? Visibility.Visible
                : Visibility.Hidden;

            ImpexMenuVisibility = ExportLinksBottomBarVisibility;
        }

        #endregion

        #region ButtonCommands

        public ICommand CloseSettingsPageCommand { get; }
        public void OnCloseSettingsPageCommandExecuted(object parameter)
        {
            MainWindowVM.ChangeSettingsFieldVisibilityCommand?.Execute(null);
        }

        public ICommand ShowImportMenuCommand { get; }
        public void OnShowImportMenuCommandExecuted(object parameter)
        {
            if (ImpexMenuVisibility == Visibility.Visible)
                return;

            bool isImported = DataParser.TryImportGroups(out IEnumerable<Group> groups, out System.Windows.Forms.DialogResult dialogResult);

            if (isImported)
            {
                int groupsCount = groups?.Count() ?? 0;

                if (groupsCount == 0)
                {
                    ImpexGroups = null;
                    ChangeImportLinksBottomBarVisibilityCommand?.Execute(null);
                    return;
                }

                _impexGroups = new GroupViewModel[groupsCount];

                int index = 0;

                foreach (Group group in groups)
                {
                    _impexGroups[index++] = new GroupViewModel(group);
                }

                OnPropertyChanged(nameof(ImpexGroups));
                ChangeImportLinksBottomBarVisibilityCommand?.Execute(null);
            }
            else if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                new FastMessage(CurrLocale.LocaleMessages.LinksImportingError, CurrLocale).ShowError();
            }
            else
            {
                return;
            }
        }

        public ICommand ShowExportMenuCommand { get; }
        public void OnShowExportMenuCommandExecuted(object parameter)
        {
            if (ExportLinksBottomBarVisibility == Visibility.Hidden)
            {
                ObservableCollection<Group> groups = MainWindowVM.LinkCollectionVM.GroupCollection;

                if (groups == null || groups.Count == 0)
                    ImpexGroups = null;

                _impexGroups = new GroupViewModel[groups.Count];

                for (int i = 0; i < _impexGroups.Length; ++i)
                    _impexGroups[i] = new GroupViewModel(groups[i]);

                OnPropertyChanged(nameof(ImpexGroups));
            }

            ChangeExportLinksBottomBarVisibilityCommand?.Execute(null);
        }

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
                new FastMessage(CurrLocale.LocaleMessages.RestoreLinkError, CurrLocale).ShowError();
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

            ImportLinksCommand = new RelayCommand(OnImportLinksCommandExecuted, t => true);
            ExportLinksCommand = new RelayCommand(OnExportLinksCommandExecuted, t => true);
            CheckAllLinksCommand = new RelayCommand(OnCheckAllLinksCommandExecuted, t => true);

            ChangeGroupsSortingCommand = new RelayCommand(delegate { }, t => true);

            ChangeImportLinksBottomBarVisibilityCommand = new RelayCommand(OnChangeImportLinksBottomBarVisibilityCommandExecuted, t => true);
            ChangeExportLinksBottomBarVisibilityCommand = new RelayCommand(OnChangeExportLinksBottomBarVisibilityCommandExecuted, t => true);

            CloseSettingsPageCommand = new RelayCommand(OnCloseSettingsPageCommandExecuted, t => true);
            ShowImportMenuCommand = new RelayCommand(OnShowImportMenuCommandExecuted, t => true);
            ShowExportMenuCommand = new RelayCommand(OnShowExportMenuCommandExecuted, t => true);

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
