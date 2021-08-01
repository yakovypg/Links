using Links.Models.Collections;
using System;
using System.Windows.Media;

namespace Links.Models
{
    internal class LinkCreator
    {
        public string Link { get; set; }
        public string Title { get; set; }
        public Group ParentGroup { get; set; }
        public ImageSource BackgroundImage { get; set; }

        public LinkCreator(Group parentGroup = null)
        {
            ParentGroup = parentGroup;
        }

        public LinkInfo Create()
        {
            return new LinkInfo(DateTime.Now, Link, Title, ParentGroup, BackgroundImage);
        }
    }
}
