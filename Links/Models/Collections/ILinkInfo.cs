using System;
using System.Windows.Media;

namespace Links.Models.Collections
{
    internal interface ILinkInfo : ILink
    {
        DateTime DateCreation { get; }

        string Title { get; set; }
        Group ParentGroup { get; set; }

        ImageSource ToolTipImage { get; }
        ImageSource BackgroundImage { get; set; }

        LinkPresenter Presenter { get; }
    }
}
