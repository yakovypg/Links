using System.Windows.Media.Imaging;

namespace Links.Models.Collections.Creators
{
    internal interface ILinkCreator
    {
        Group ParentGroup { get; set; }

        string Link { get; set; }
        string Title { get; set; }
        BitmapImage BackgroundImage { get; set; }

        LinkInfo CreateLink();
    }
}
