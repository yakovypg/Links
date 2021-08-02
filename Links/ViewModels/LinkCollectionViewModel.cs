using Links.Infrastructure.Commands;
using Links.Infrastructure.Extensions;
using Links.Models.Collections;
using Links.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace Links.ViewModels
{
    internal class LinkCollectionViewModel : CustomizedViewModel
    {
        public int LinkPresenterGridWidth => _linkPresenterGridWidth;
        public int LinkPresenterGridHeight => _linkPresenterGridHeight;
        public int LinksFieldWrapPanelItemWidth => _linkPresenterGridWidth + 5;
        public int LinksFieldWrapPanelItemHeight => _linkPresenterGridHeight + 5;

        public IEnumerable<string> GroupIconColors => Enum.GetValues(typeof(GroupIcon.Colors)).Cast<GroupIcon.Colors>().ToStringEnumerable();

        private int _groupEditorHeight = 0;
        public int GroupEditorHeight
        {
            get => _groupEditorHeight;
            private set
            {
                int height = value <= 0 ? 0 : 70;
                _ = SetValue(ref _groupEditorHeight, height);
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
                    _selectedGroupLinks?.View.Refresh();
            }
        }

        #endregion

        public ICommand DeleteGroupCommand { get; }
        public bool CanDeleteGroupCommandExecute(object parameter)
        {
            return SelectedGroup != null;
        }
        public void OnDeleteGroupCommandExecuted(object parameter)
        {
            _ = GroupCollection.Remove(SelectedGroup);
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

        public ICommand DeleteLinkCommand { get; }
        public ICommand ChangeLinkEditorVisibilityCommand { get; }
        public ICommand SetLinkImageCommand { get; }

        public ObservableCollection<Group> GroupCollection { get; private set; } = new ObservableCollection<Group>() { new Group("TestGroup", new ObservableCollection<LinkInfo>() { new LinkInfo(DateTime.Now, "google", "GitHub"), new LinkInfo(DateTime.Now, "yandex", "Gmail") }) };

        public LinkCollectionViewModel()
        {
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

            DeleteLinkCommand = new RelayCommand(delegate { }, t => true);
            ChangeLinkEditorVisibilityCommand = new RelayCommand(delegate { }, t => true);
            SetLinkImageCommand = new RelayCommand(delegate { }, t => true);
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
