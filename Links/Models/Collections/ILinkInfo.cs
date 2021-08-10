using System;
using System.Windows.Media.Imaging;

namespace Links.Models.Collections
{
    internal interface ILinkInfo : ILink
    {
        DateTime DateCreation { get; }

        string Title { get; set; }
        Group ParentGroup { get; set; }
        BitmapImage BackgroundImage { get; set; }
    }
}
