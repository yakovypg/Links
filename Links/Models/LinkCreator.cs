using Links.Models.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace Links.Models
{
    internal sealed class LinkCreator : ILinkCreator, INotifyPropertyChanged
    {
        public Group ParentGroup { get; set; }

        private string _link = string.Empty;
        public string Link
        {
            get => _link;
            set
            {
                _link = value;
                OnPropertyChanged(nameof(Link));
            }
        }

        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        private ImageSource _backgroundImage = null;
        public ImageSource BackgroundImage
        {
            get => _backgroundImage;
            set
            {
                _backgroundImage = value;
                OnPropertyChanged(nameof(BackgroundImage));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public LinkCreator(Group parentGroup = null)
        {
            ParentGroup = parentGroup;
        }

        public LinkInfo CreateLink()
        {
            string link = Link;
            string title = Title;
            ImageSource backgroundImage = BackgroundImage;

            Link = string.Empty;
            Title = string.Empty;
            BackgroundImage = null;

            return new LinkInfo(link, title, ParentGroup, backgroundImage);
        }
    }
}
