using Links.Data;
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
        public ObservableCollection<Group> GroupCollection { get; private set; }

        public DeleteGroupParamMultiConverter DeleteGroupParamMultiConverter { get; } = new DeleteGroupParamMultiConverter();
        public IEnumerable<string> GroupIconColors => Enum.GetValues(typeof(GroupIcon.Colors)).Cast<GroupIcon.Colors>().ToStringEnumerable();

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
            MessageBoxResult msgResult = new FastMessage(CurrLocale.LocaleMessages.DeleteGroupQuestion, CurrLocale).GetQuestionResult();

            var paramTuple = parameter as Tuple<object, object>;

            var linkCreatorGroup = paramTuple?.Item1 as Group;
            ICommand resetLinkCreatorGroupIndexCommand = (paramTuple?.Item2 as MainWindowViewModel)?.ResetLinkCreatorGroupIndexCommand;

            Group deletedGroup = SelectedGroup;

            bool isGroupsEqual = linkCreatorGroup == SelectedGroup;
            bool isGroupRemoved = GroupCollection.Remove(SelectedGroup);

            if (isGroupRemoved && isGroupsEqual)
                resetLinkCreatorGroupIndexCommand?.Execute(null);

            if (isGroupRemoved && msgResult == MessageBoxResult.No)
            {
                Group unsortedLinksGroup = GroupCollection.FirstOrDefault(t => t.Name == MainWindowVM.CurrentLocale.Unsorted);

                if (unsortedLinksGroup == null)
                {
                    unsortedLinksGroup = new Group(MainWindowVM.CurrentLocale.Unsorted);
                    GroupCollection.Add(unsortedLinksGroup);
                }

                unsortedLinksGroup.RedefineLinks(deletedGroup.Links.ToArray());
            }

            if (!isGroupRemoved)
                new FastMessage(CurrLocale.LocaleMessages.DeleteGroupError, CurrLocale).ShowError();
        }

        public ICommand ChangeGroupEditorVisibilityCommand { get; }
        public bool CanChangeGroupEditorVisibilityCommandExecute(object parameter)
        {
            return SelectedGroup != null;
        }
        public void OnChangeGroupEditorVisibilityCommandExecuted(object parameter)
        {
            GroupEditorHeight = GroupEditorHeight != 0 ? 0 : 70;
        }

        #endregion

        #region LinkCommands

        public ICommand DeleteLinkCommand { get; }
        public void OnDeleteLinkCommandExecuted(object parameter)
        {
            if (!(parameter is LinkInfo linkInfo))
                return;

            if (MainWindowVM.SettingsVM.IsWarningsEnable)
            {
                MessageBoxResult msgResult = new FastMessage(CurrLocale.LocaleMessages.DeleteLinkQuestion, CurrLocale)
                    .GetWarningResult(MessageBoxButton.YesNo);

                if (msgResult == MessageBoxResult.No)
                    return;
            }

            if (!linkInfo.ParentGroup.Links.Remove(linkInfo))
                new FastMessage(CurrLocale.LocaleMessages.DeleteLinkError, CurrLocale).ShowError();
        }

        public ICommand SetLinkImageCommand { get; }
        public void OnSetLinkImageCommandExecuted(object parameter)
        {
            if (!(parameter is LinkInfo linkInfo))
                return;

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

                new FastMessage(message, CurrLocale).ShowError();
            }
        }

        public ICommand ChangeLinkEditorVisibilityCommand { get; }
        public void OnChangeLinkEditorVisibilityCommandExecuted(object parameter)
        {
            if (!(parameter is LinkInfo linkInfo))
                return;

            linkInfo.PresenterVisibility = linkInfo.PresenterVisibility == Visibility.Hidden
                ? Visibility.Visible
                : Visibility.Hidden;
        }

        public ICommand HideLinkEditorCommand { get; }
        public void OnHideLinkEditorCommandExecuted(object parameter)
        {
            if (!(parameter is LinkInfo linkInfo))
                return;

            if (linkInfo.IsLinkMoved)
            {
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

            linkInfo.PresenterVisibility = Visibility.Visible;
        }

        #endregion

        public LinkCollectionViewModel(MainWindowViewModel mainWindowVM)
        {
            var group1 = new Group("Math");
            var group2 = new Group("Programming");
            var group3 = new Group("Chemistry");
            var group4 = new Group("Empty");

            group1.Links.Add(new LinkInfo("https://www.wolframalpha.com/", "Wolfram", group1));
            group1.Links.Add(new LinkInfo("https://allcalc.ru/", "Allcalc", group1));
            group1.Links.Add(new LinkInfo("https://www.geogebra.org/3d", "GeoGebra", group1));

            group2.Links.Add(new LinkInfo("https://google.com", "Google", group2));
            group2.Links.Add(new LinkInfo("https://yandex.ru", "Yandex", group2));

            group3.Links.Add(new LinkInfo("https://chemiday.com/", "ChemiDay", group3));

            MainWindowVM = mainWindowVM ?? throw new ArgumentNullException();
            GroupCollection = new ObservableCollection<Group>() { group1, group2, group3, group4 };

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
            ChangeGroupEditorVisibilityCommand = new RelayCommand(OnChangeGroupEditorVisibilityCommandExecuted, CanChangeGroupEditorVisibilityCommandExecute);

            DeleteLinkCommand = new RelayCommand(OnDeleteLinkCommandExecuted, t => true);
            SetLinkImageCommand = new RelayCommand(OnSetLinkImageCommandExecuted, t => true);
            FollowLinkCommand = new RelayCommand(OnFollowLinkCommandExecuted, t => true);

            ChangeLinkEditorVisibilityCommand = new RelayCommand(OnChangeLinkEditorVisibilityCommandExecuted, t => true);
            HideLinkEditorCommand = new RelayCommand(OnHideLinkEditorCommandExecuted, t => true);
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
