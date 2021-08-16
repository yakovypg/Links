using Links.Data;
using Links.Data.Imaging;
using Links.Infrastructure.Commands;
using Links.Infrastructure.Converters;
using Links.Infrastructure.Extensions;
using Links.Models.Collections;
using Links.Models.Localization;
using Links.Models.Messages;
using Links.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Links.ViewModels
{
    internal class LinkCollectionViewModel : ViewModel
    {
        private ILocale CurrLocale => MainWindowVM.CurrentLocale;

        public MainWindowViewModel MainWindowVM { get; }
        public ObservableCollection<Group> GroupCollection { get; set; }

        public DeleteGroupParamMultiConverter DeleteGroupParamMultiConverter { get; } = new DeleteGroupParamMultiConverter();
        public IEnumerable<string> GroupIconColors => Enum.GetValues(typeof(GroupIcon.Colors)).Cast<GroupIcon.Colors>().ToStringEnumerable();

        private Group _grouoToMoveLinks;
        public Group GroupToMoveLinks
        {
            get => _grouoToMoveLinks;
            set => SetValue(ref _grouoToMoveLinks, value);
        }

        #region SizeParameters

        private double _linkPresenterGridWidth;
        public double LinkPresenterGridWidth
        {
            get => _linkPresenterGridWidth;
            set => SetValue(ref _linkPresenterGridWidth, value);
        }

        private double _linkPresenterGridHeight;
        public double LinkPresenterGridHeight
        {
            get => _linkPresenterGridHeight;
            set => SetValue(ref _linkPresenterGridHeight, value);
        }

        private double _linksFieldWrapPanelItemWidth;
        public double LinksFieldWrapPanelItemWidth
        {
            get => _linksFieldWrapPanelItemWidth;
            set => SetValue(ref _linksFieldWrapPanelItemWidth, value);
        }

        private double _linksFieldWrapPanelItemHeight;
        public double LinksFieldWrapPanelItemHeight
        {
            get => _linksFieldWrapPanelItemHeight;
            set => SetValue(ref _linksFieldWrapPanelItemHeight, value);
        }

        private int _groupEditorHeight = 0;
        public int GroupEditorHeight
        {
            get => _groupEditorHeight;
            private set
            {
                int height = value <= 0 ? 0 : 70;
                SetValue(ref _groupEditorHeight, height);
            }
        }

        #endregion

        #region FieldsVisibility

        private Visibility _linksMoveMenuVisibility = Visibility.Hidden;
        public Visibility LinksMoveMenuVisibility
        {
            get => _linksMoveMenuVisibility;
            set => SetValue(ref _linksMoveMenuVisibility, value);
        }

        #endregion

        #region Groups&LinksFiltering

        private readonly CollectionViewSource _groups;
        public ICollectionView Groups => _groups?.View;

        private readonly CollectionViewSource _selectedGroupLinks;
        public ICollectionView SelectedGroupLinks => _selectedGroupLinks?.View;

        private Group _selectedGroup;
        public Group SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                if (!SetValue(ref _selectedGroup, value))
                    return;

                _selectedGroupLinks.Source = value?.Links;
                OnPropertyChanged(nameof(SelectedGroupLinks));
            }
        }

        private int _selectedGroupIndex;
        public int SelectedGroupIndex
        {
            get => _selectedGroupIndex;
            set => SetValue(ref _selectedGroupIndex, value);
        }

        private string _groupFilterText = string.Empty;
        public string GroupFilterText
        {
            get => _groupFilterText;
            set
            {
                if (SetValue(ref _groupFilterText, value))
                    _groups?.View.Refresh();
            }
        }

        private string _linkFilterText = string.Empty;
        public string LinkFilterText
        {
            get => _linkFilterText;
            set
            {
                if (SetValue(ref _linkFilterText, value))
                    _selectedGroupLinks?.View?.Refresh();
            }
        }

        #endregion

        #region GroupCommands

        public ICommand DeleteGroupCommand { get; }
        public bool CanDeleteGroupCommandExecute(object parameter)
        {
            return SelectedGroup != null;
        }
        public void OnDeleteGroupCommandExecuted(object parameter)
        {
            if (MainWindowVM.SettingsVM.IsWarningsEnable)
            {
                MessageBoxResult msgResult = new QuickMessage(CurrLocale.LocaleMessages.DeleteGroupQuestion, CurrLocale)
                    .GetWarningResult(MessageBoxButton.YesNo);

                if (msgResult == MessageBoxResult.No)
                    return;
            }

            var paramTuple = parameter as Tuple<object, object>;
            var linkCreatorGroup = paramTuple?.Item1 as Group;
            var resetLinkCreatorGroupIndexCommand = (paramTuple?.Item2 as MainWindowViewModel)?.ResetLinkCreatorGroupIndexCommand;

            Group deletedGroup = SelectedGroup;

            bool isGroupsEqual = linkCreatorGroup?.Equals(SelectedGroup) ?? false;
            bool isGroupRemoved = GroupCollection.Remove(SelectedGroup);

            if (isGroupRemoved && MainWindowVM.SettingsVM.IsRecycleBinEnable)
                MainWindowVM.SettingsVM.AddLinksToRecycleBin(deletedGroup.Links);

            if (isGroupRemoved && isGroupsEqual)
                resetLinkCreatorGroupIndexCommand?.Execute(null);

            if (!isGroupRemoved)
                new QuickMessage(CurrLocale.LocaleMessages.DeleteGroupError, CurrLocale).ShowError();
        }

        #endregion

        #region LinkCommands

        public ICommand DeleteLinkCommand { get; }
        public bool CanDeleteLinkCommandExecuted(object parameter)
        {
            return parameter is LinkInfo;
        }
        public void OnDeleteLinkCommandExecuted(object parameter)
        {
            if (!CanDeleteGroupCommandExecute(parameter))
                return;

            var linkInfo = parameter as LinkInfo;

            if (MainWindowVM.SettingsVM.IsWarningsEnable)
            {
                MessageBoxResult msgResult = new QuickMessage(CurrLocale.LocaleMessages.DeleteLinkQuestion, CurrLocale)
                    .GetWarningResult(MessageBoxButton.YesNo);

                if (msgResult == MessageBoxResult.No)
                    return;
            }

            bool isLinkRemoved = linkInfo.ParentGroup.Links.Remove(linkInfo);

            if (isLinkRemoved && MainWindowVM.SettingsVM.IsRecycleBinEnable)
                MainWindowVM.SettingsVM.RecycleBin.Add(linkInfo);

            if (isLinkRemoved && (SelectedGroup?.Contains(linkInfo) ?? false))
                SelectedGroup.Remove(linkInfo);

            if (!isLinkRemoved)
                new QuickMessage(CurrLocale.LocaleMessages.DeleteLinkError, CurrLocale).ShowError();
        }

        public ICommand DeleteSelectedLinksCommand { get; }
        public void OnDeleteSelectedLinksCommandExecuted(object parameter)
        {
            var selectedLinks = new GroupAnalyzer().GetAllSelectedLinks(GroupCollection);

            if (selectedLinks.IsNullOrEmpty())
                return;

            if (MainWindowVM.SettingsVM.IsWarningsEnable)
            {
                MessageBoxResult msgResult = new QuickMessage(CurrLocale.LocaleMessages.DeleteLinkQuestion, CurrLocale)
                    .GetWarningResult(MessageBoxButton.YesNo);

                if (msgResult == MessageBoxResult.No)
                    return;
            }

            var warningParam = MainWindowVM.SettingsVM.WarningsParam;
            MainWindowVM.SettingsVM.WarningsParam = CurrLocale.Off;

            foreach (var link in selectedLinks)
            {
                link.IsSelected = false;
                DeleteLinkCommand?.Execute(link);
            }

            MainWindowVM.SettingsVM.WarningsParam = warningParam;
        }

        public ICommand MoveLinkCommand { get; }
        public bool CanMoveLinkCommandExecuted(object parameter)
        {
            //change the parent group of the link and then call this method
            return parameter is LinkInfo linkInfo && linkInfo.IsLinkMoved;
        }
        public void OnMoveLinkCommandExecuted(object parameter)
        {
            if (!CanMoveLinkCommandExecuted(parameter))
                return;

            var linkInfo = parameter as LinkInfo;

            if (linkInfo.PrimaryGroup.Links.Remove(linkInfo))
            {
                linkInfo.ParentGroup.Links.Add(linkInfo);
                linkInfo.ResetPrimaryGroup();
            }
            else
            {
                linkInfo.CancelMove();
            }
        }

        public ICommand MoveSelectedLinksCommand { get; }
        public void OnMoveSelectedLinksCommandExecuted(object parameter)
        {
            var selectedLinks = new GroupAnalyzer().GetAllSelectedLinks(GroupCollection);

            if (selectedLinks.IsNullOrEmpty())
                return;

            var newParentGroup = GroupToMoveLinks;

            if (newParentGroup == null)
            {
                new QuickMessage(CurrLocale.LocaleMessages.NoSelectedGroupToMoveInfo, CurrLocale).ShowWarning();
                return;
            }

            foreach (var link in selectedLinks)
            {
                link.IsSelected = false;
                link.ParentGroup = newParentGroup;
                MoveLinkCommand?.Execute(link);
            }

            ChangeLinksMoveMenuVisibilityCommand?.Execute(null);
        }

        public ICommand CheckAllLinksCommand { get; }
        public void OnCheckAllLinksCommandExecuted(object parameter)
        {
            var uncheckedLinks = SelectedGroup?.Links?.Where(t => !t.IsSelected)?.ToArray();

            if (uncheckedLinks.IsNullOrEmpty())
                return;

            foreach (var link in uncheckedLinks)
                link.IsSelected = true;
        }

        public ICommand UncheckAllLinksCommand { get; }
        public void OnUncheckAllLinksCommandExecuted(object parameter)
        {
            var checkedLinks = SelectedGroup?.Links?.Where(t => t.IsSelected)?.ToArray();

            if (checkedLinks.IsNullOrEmpty())
                return;

            foreach (var link in checkedLinks)
                link.IsSelected = false;
        }

        public ICommand SetLinkImageCommand { get; }
        public bool CanSetLinkImageCommandExecute(object parameter)
        {
            return parameter is LinkInfo;
        }
        public void OnSetLinkImageCommandExecuted(object parameter)
        {
            if (!CanSetLinkImageCommandExecute(parameter))
                return;

            var linkInfo = parameter as LinkInfo;

            DialogProvider.GetFilePath(out string path);

            int width = (int)MainWindowVM.SettingsVM.MaxLinkPresenterGridWidth - 3 - 2 * 2;
            int height = (int)MainWindowVM.SettingsVM.MaxLinkPresenterGridHeight - 3 - 20 - 22 - 2 * 2;
            var size = new System.Drawing.Size(width, height);

            if (ImageTransformer.TryGetBitmapImage(path, size, out System.Windows.Media.Imaging.BitmapImage newImage))
                linkInfo.BackgroundImage = newImage;
        }

        public ICommand FollowLinkCommand { get; }
        public void OnFollowLinkCommandExecuted(object parameter)
        {
            try
            {
                System.Diagnostics.Process.Start(parameter as string);
            }
            catch (Exception ex)
            {
                string message = $"{MainWindowVM.CurrentLocale.Error}: {MainWindowVM.CurrentLocale.LocaleMessages.FollowLinkError}\n\n" +
                                 $"{MainWindowVM.CurrentLocale.Comment}: {ex.Message}";

                new QuickMessage(message, CurrLocale).ShowError();
            }
        }

        #endregion

        #region VisibilityCommands

        public ICommand ChangeGroupEditorVisibilityCommand { get; }
        public bool CanChangeGroupEditorVisibilityCommandExecute(object parameter)
        {
            return SelectedGroup != null;
        }
        public void OnChangeGroupEditorVisibilityCommandExecuted(object parameter)
        {
            GroupEditorHeight = GroupEditorHeight != 0 ? 0 : 70;
        }

        public ICommand ChangeLinkEditorVisibilityCommand { get; }
        public bool CanChangeLinkEditorVisibilityCommandExecute(object parameter)
        {
            return parameter is LinkInfo;
        }
        public void OnChangeLinkEditorVisibilityCommandExecuted(object parameter)
        {
            if (!CanChangeLinkEditorVisibilityCommandExecute(parameter))
                return;

            var linkInfo = parameter as LinkInfo;

            linkInfo.PresenterVisibility = linkInfo.PresenterVisibility == Visibility.Hidden
                ? Visibility.Visible
                : Visibility.Hidden;
        }

        public ICommand HideLinkEditorCommand { get; }
        public bool CanHideLinkEditorCommandExecuted(object parameter)
        {
            return parameter is LinkInfo;
        }
        public void OnHideLinkEditorCommandExecuted(object parameter)
        {
            if (!CanHideLinkEditorCommandExecuted(parameter))
                return;

            MoveLinkCommand?.Execute(parameter);
            (parameter as LinkInfo).PresenterVisibility = Visibility.Visible;
        }

        public ICommand ChangeLinksMoveMenuVisibilityCommand { get; }
        public void OnChangeLinksMoveMenuVisibilityCommandExecuted(object parameter)
        {
            bool isLinksMoveMenuHidden = LinksMoveMenuVisibility == Visibility.Hidden;

            if (isLinksMoveMenuHidden)
            {
                var selectedLinks = SelectedGroup?.Links?.Where(t => t.IsSelected)?.ToArray();

                if (selectedLinks.IsNullOrEmpty())
                    return;

                LinksMoveMenuVisibility = Visibility.Visible;
            }
            else
            {
                LinksMoveMenuVisibility = Visibility.Hidden;
            }
        }

        #endregion

        #region Commands

        public ICommand SetFocusCommand { get; }
        public void OnSetFocusCommandExecuted(object parameter)
        {
            if (!(parameter is UIElement element))
                return;

            element.Focus();
        }

        #endregion

        public LinkCollectionViewModel(ObservableCollection<Group> groups, MainWindowViewModel mainWindowVM)
        {
            MainWindowVM = mainWindowVM ?? throw new ArgumentNullException(nameof(mainWindowVM));
            GroupCollection = groups ?? new ObservableCollection<Group>();

            GroupToMoveLinks = GroupCollection.Count > 0 ? GroupCollection[0] : null;

            _groups = new CollectionViewSource()
            {
                IsLiveSortingRequested = true,
                IsLiveFilteringRequested = true
            };

            _groups.Source = GroupCollection;
            _groups.Filter += OnGroupFiltered;

            _selectedGroupLinks = new CollectionViewSource()
            {
                IsLiveSortingRequested = true,
                IsLiveFilteringRequested = true
            };

            _selectedGroupLinks.Filter += OnLinkFiltered;

            DeleteGroupCommand = new RelayCommand(OnDeleteGroupCommandExecuted, CanDeleteGroupCommandExecute);

            DeleteLinkCommand = new RelayCommand(OnDeleteLinkCommandExecuted, CanDeleteLinkCommandExecuted);
            DeleteSelectedLinksCommand = new RelayCommand(OnDeleteSelectedLinksCommandExecuted, t => true);
            MoveLinkCommand = new RelayCommand(OnMoveLinkCommandExecuted, CanMoveLinkCommandExecuted);
            MoveSelectedLinksCommand = new RelayCommand(OnMoveSelectedLinksCommandExecuted, t => true);
            CheckAllLinksCommand = new RelayCommand(OnCheckAllLinksCommandExecuted, t => true);
            UncheckAllLinksCommand = new RelayCommand(OnUncheckAllLinksCommandExecuted, t => true);
            SetLinkImageCommand = new RelayCommand(OnSetLinkImageCommandExecuted, CanSetLinkImageCommandExecute);
            FollowLinkCommand = new RelayCommand(OnFollowLinkCommandExecuted, t => true);

            ChangeGroupEditorVisibilityCommand = new RelayCommand(OnChangeGroupEditorVisibilityCommandExecuted, CanChangeGroupEditorVisibilityCommandExecute);
            ChangeLinkEditorVisibilityCommand = new RelayCommand(OnChangeLinkEditorVisibilityCommandExecuted, CanChangeLinkEditorVisibilityCommandExecute);
            HideLinkEditorCommand = new RelayCommand(OnHideLinkEditorCommandExecuted, CanHideLinkEditorCommandExecuted);
            ChangeLinksMoveMenuVisibilityCommand = new RelayCommand(OnChangeLinksMoveMenuVisibilityCommandExecuted, t => true);

            SetFocusCommand = new RelayCommand(OnSetFocusCommandExecuted, t => true);
        }

        public void UpdateSelectedGroup()
        {
            Group tmp = SelectedGroup;
            SelectedGroup = null;
            SelectedGroup = tmp;
        }

        public void SetGroupsSortDescriptions(SortDescription sortDescription)
        {
            _groups.SortDescriptions.Clear();
            _groups.SortDescriptions.Add(sortDescription);
        }

        public void SetLinksSortDescriptions(SortDescription sortDescription)
        {
            _selectedGroupLinks.SortDescriptions.Clear();
            _selectedGroupLinks.SortDescriptions.Add(sortDescription);
        }

        private void OnGroupFiltered(object sender, FilterEventArgs e)
        {
            if (!(e.Item is Group group))
            {
                e.Accepted = false;
                return;
            }

            string filter = GroupFilterText;

            if (string.IsNullOrEmpty(filter))
                return;

            if (group.Name.Contains(filter, StringComparison.OrdinalIgnoreCase))
                return;

            e.Accepted = false;
        }

        private void OnLinkFiltered(object sender, FilterEventArgs e)
        {
            if (!(e.Item is LinkInfo linkInfo))
            {
                e.Accepted = false;
                return;
            }

            string filter = LinkFilterText;

            if (string.IsNullOrEmpty(filter))
                return;

            if (linkInfo.Title.Contains(filter, StringComparison.OrdinalIgnoreCase))
                return;

            if (linkInfo.Link.Contains(filter, StringComparison.OrdinalIgnoreCase))
                return;

            e.Accepted = false;
        }
    }
}
