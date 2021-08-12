using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Links.Models.Collections
{
    //Do not override GetHashCode

    internal class LinkInfo : ILinkInfo, ILinkPresenter, INotifyPropertyChanged
    {
        public LinkInfo Self => this;

        public DateTime DateCreation { get; }
        public bool IsLinkMoved { get; private set; }
        public Group PrimaryGroup { get; private set; }

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

        private BitmapImage _backgroundImage;
        public BitmapImage BackgroundImage
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

        public LinkInfo() : this(null, null, null, null)
        {
        }

        public LinkInfo(string link, string title, Group parentGroup = null, BitmapImage backgroundImage = null)
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
    }
}
