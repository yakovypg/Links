using System;
using System.Windows.Media;

namespace Links.Models.Collections
{
    internal class LinkInfo : ILinkInfo
    {
        public DateTime DateCreation { get; }

        public string Link { get; set; }
        public string Title { get; set; }
        public Group ParentGroup { get; set; }

        private ImageSource _backgroundImage;
        public ImageSource ToolTipImage { get; private set; }
        public ImageSource BackgroundImage
        {
            get => _backgroundImage;
            set => _backgroundImage = ToolTipImage = value;
        }

        public LinkPresenter Presenter { get; }

        public LinkInfo(DateTime dateCreation, string link = null, string title = null,
            Group parentGroup = null, ImageSource backgroundImage = null)
        {
            DateCreation = dateCreation;

            Link = link;
            Title = title;
            ParentGroup = parentGroup;
            BackgroundImage = backgroundImage;

            Presenter = new LinkPresenter();
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
            return HashCode.Combine(DateCreation, Link, Title, ParentGroup, _backgroundImage, ToolTipImage, BackgroundImage, Presenter);
        }
    }
}
