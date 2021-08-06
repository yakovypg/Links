using Links.Models.Localization;
using Links.Models.Themes;
using System.Windows;

namespace Links.Models.Configuration
{
    internal interface ISettings
    {
        string GroupSortPropertyName { get; set; }
        string GroupListSortDescriptionParam { get; set; }
        string LinkSortPropertyName { get; set; }
        string LinkListSortDescriptionParam { get; set; }
        string WarningsParam { get; set; }
        Size PresenterSize { get; set; }
        IWindowTheme Theme { get; set; }
        ILocale CurrentLocale { get; set; }
        string RecycleBinParam { get; set; }
        string EmptyRecycleBinParam { get; set; }
    }
}
