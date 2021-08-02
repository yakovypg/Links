using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace Links.Models.Collections
{
    internal class LinkInfo : ILink, ILinkInfo, ILinkPresenter, INotifyPropertyChanged
    {
        public LinkInfo Self => this;

        public bool IsLinkMoved { get; private set; }
        public Group PrimaryGroup { get; private set; }

        public DateTime DateCreation { get; }

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

        private Group _parentGroup = null;
        public Group ParentGroup
        {
            get => _parentGroup;
            set
            {
                if (!IsLinkMoved)
                {
                    PrimaryGroup = _parentGroup;
                    IsLinkMoved = true;
                }

                _parentGroup = value;
                OnPropertyChanged(nameof(ParentGroup));
            }
        }

        private ImageSource _backgroundImage;
        public ImageSource BackgroundImage
        {
            get => _backgroundImage;
            set
            {
                _backgroundImage = value;
                OnPropertyChanged(nameof(BackgroundImage));
            }
        }

        private Visibility _presenterVisibility = Visibility.Visible;
        public Visibility PresenterVisibility
        {
            get => _presenterVisibility;
            set
            {
                _presenterVisibility = value;
                OnPropertyChanged(nameof(PresenterVisibility));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public LinkInfo(string link = null, string title = null, Group parentGroup = null, ImageSource backgroundImage = null)
        {
            DateCreation = DateTime.Now;

            Link = link;
            Title = title;
            _parentGroup = PrimaryGroup = parentGroup;
            BackgroundImage = backgroundImage;
        }

        public void CancelMove()
        {
            IsLinkMoved = false;
            ParentGroup = PrimaryGroup;
        }

        public void ResetPrimaryGroup()
        {
            IsLinkMoved = false;
            PrimaryGroup = ParentGroup;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object obj)
        {
            return obj is LinkInfo other &&
                   DateCreation == other.DateCreation &&
                   Link == other.Link &&
                   Title == other.Title &&
                   (BackgroundImage?.Equals(other.BackgroundImage) ?? true);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(DateCreation, Link, Title, ParentGroup, _backgroundImage, BackgroundImage);
        }
    }
}
