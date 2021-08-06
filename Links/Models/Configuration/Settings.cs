using Links.Models.Localization;
using Links.Models.Themes;
using System.Windows;

namespace Links.Models.Configuration
{
    internal class Settings : ISettings
    {
        public string GroupSortPropertyName { get; set; }
        public string GroupListSortDescriptionParam { get; set; }
        public string LinkSortPropertyName { get; set; }
        public string LinkListSortDescriptionParam { get; set; }
        public string WarningsParam { get; set; }
        public Size PresenterSize { get; set; }
        public IWindowTheme Theme { get; set; }
        public ILocale CurrentLocale { get; set; }
        public string RecycleBinParam { get; set; }
        public string EmptyRecycleBinParam { get; set; }
    }
}
