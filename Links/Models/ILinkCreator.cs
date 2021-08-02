using Links.Models.Collections;
using System.Windows.Media;

namespace Links.Models
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
