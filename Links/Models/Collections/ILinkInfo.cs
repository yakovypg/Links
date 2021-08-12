using System;
using System.Windows.Media.Imaging;

namespace Links.Models.Collections
{
    internal interface ILinkInfo : ILink
    {
        string Title { get; set; }
        DateTime DateCreation { get; }
        BitmapImage BackgroundImage { get; set; }

        Group PrimaryGroup { get; }
        Group ParentGroup { get; set; }
    }
}
