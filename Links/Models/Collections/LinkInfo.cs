using System;
using System.Windows.Media;

namespace Links.Models.Collections
{
    internal class LinkInfo
    {
        public int ID { get; }
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

        public LinkInfo(int id, DateTime dateCreation,
                        string link = null, string title = null, Group parentGroup = null, ImageSource backgroundImage = null)
        {
            ID = id;
            DateCreation = dateCreation;

            Link = link;
            Title = title;
            ParentGroup = parentGroup;
            BackgroundImage = backgroundImage;

            Presenter = new LinkPresenter();
        }
    }
}
