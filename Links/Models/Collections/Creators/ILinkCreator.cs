using System.Windows.Media;

namespace Links.Models.Collections.Creators
{
    internal interface ILinkCreator
    {
        Group ParentGroup { get; set; }

        string Link { get; set; }
        string Title { get; set; }
        ImageSource BackgroundImage { get; set; }

        LinkInfo CreateLink();
    }
}
