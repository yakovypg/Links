using Links.Data.App;
using Links.Models.Collections;
using Links.Models.Configuration;
using Links.Models.Localization;
using Links.Models.Themes;
using System.Collections.Generic;

namespace Links.Data
{
    internal static class DataParser
    {
        public static ISettings GetSettings()
        {
            return null ?? new SettingsItems().GetDefaultSettings();
        }

        public static IEnumerable<LinkInfo> GetRecycleBin()
        {
            return null;
        }

        public static IEnumerable<ILocale> GetLocales()
        {
            return null;
        }

        public static IEnumerable<IWindowTheme> GetWindowThemes()
        {
            return null;
        }
    }
}
