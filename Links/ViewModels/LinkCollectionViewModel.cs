using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.ObjectModel;
using Links.Models.Localization;
using Links.Models.Collections;
using Links.Models.Themes;
using Links.ViewModels.Base;
using Links.Infrastructure.Commands;

namespace Links.ViewModels
{
    internal class LinkCollectionViewModel : ViewModel
    {
        private SolidColorBrush _linkPresenterGridBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAAAAAA"));
        public SolidColorBrush LinkPresenterGridBackground
        {
            get => _linkPresenterGridBackground;
            set => SetValue(ref _linkPresenterGridBackground, value);
        }

        private int _linkPresenterGridWidth = 150;
        public int LinkPresenterGridWidth
        {
            get => _linkPresenterGridWidth;
            set => SetValue(ref _linkPresenterGridWidth, value);
        }

        private int _linkPresenterGridHeight = 230;
        public int LinkPresenterGridHeight
        {
            get => _linkPresenterGridHeight;
            set => SetValue(ref _linkPresenterGridHeight, value);
        }

        public int LinksPageWrapPanelItemWidth => _linkPresenterGridWidth + 5;
        public int LinksPageWrapPanelItemHeight => _linkPresenterGridHeight + 5;

        public ICommand DeleteGroupCommand { get; }
        public ICommand ChangeGroupCommand { get; }
        public ICommand ChangeGroupEditorVisibilityCommand { get; }

        public ICommand DeleteLinkCommand { get; }
        public ICommand ChangeLinkEditorVisibilityCommand { get; }
        public ICommand SetLinkImageCommand { get; }

        public Group SelectedGroup { get; set; }

        public ObservableCollection<Group> GroupCollection { get; private set; } = new ObservableCollection<Group>() { new Group("TestGroup", new ObservableCollection<LinkInfo>() { new LinkInfo(5, System.DateTime.Now, "Link", "Title"), new LinkInfo(55, System.DateTime.Now, "link", "title") }) };
        public ObservableCollection<LinkInfo> LinkCollection { get; private set; } = new ObservableCollection<LinkInfo>() { new LinkInfo(0, System.DateTime.Now, "Link", "Title"), new LinkInfo(1, System.DateTime.Now, "link", "title") };

        public LinkCollectionViewModel()
        {
            DeleteGroupCommand = new RelayCommand(delegate { }, t => true);
            ChangeGroupCommand = new RelayCommand(delegate { }, t => true);
            ChangeGroupEditorVisibilityCommand = new RelayCommand(delegate { }, t => true);

            DeleteLinkCommand = new RelayCommand(delegate { }, t => true);
            ChangeLinkEditorVisibilityCommand = new RelayCommand(delegate { }, t => true);
            SetLinkImageCommand = new RelayCommand(delegate { }, t => true);
        }
    }
}
