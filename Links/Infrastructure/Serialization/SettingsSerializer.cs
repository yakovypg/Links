using Links.Models.Configuration;

namespace Links.Infrastructure.Serialization
{
    internal class SettingsSerializer : ISerializer<Settings>
    {
        public Settings Deserialize(string data)
        {
            if (string.IsNullOrEmpty(data))
                return null;

            var item = new SerializerItem(data);

            string groupSortPropertyName = item.GetValue("GroupSortPropertyName");
            string groupListSortDescriptionParam = item.GetValue("GroupListSortDescriptionParam");
            string linkSortPropertyName = item.GetValue("LinkSortPropertyName");
            string linkListSortDescriptionParam = item.GetValue("LinkListSortDescriptionParam");
            string warningsParam = item.GetValue("WarningsParam");
            string recycleBinParam = item.GetValue("RecycleBinParam");
            string emptyRecycleBinParam = item.GetValue("EmptyRecycleBinParam");

            string presenterSizeData = item.GetValue("PresenterSize");
            int sepIndex = presenterSizeData.IndexOf("x");
            int width = int.Parse(presenterSizeData.Remove(sepIndex));
            int height = int.Parse(presenterSizeData.Substring(sepIndex + 1));
            var presenterSize = new System.Windows.Size(width, height);

            string themeData = item.GetValue("Theme").Substring(1);
            themeData = themeData.Remove(themeData.Length - 1);
            var theme = new WindowThemeSerializer().Deserialize(themeData);

            string localeData = item.GetValue("CurrentLocale").Substring(1);
            localeData = localeData.Remove(localeData.Length - 1);
            var locale = new LocaleSerializer().Deserialize(localeData);

            return new Settings()
            {
                GroupSortPropertyName = groupSortPropertyName,
                GroupListSortDescriptionParam = groupListSortDescriptionParam,
                LinkSortPropertyName = linkSortPropertyName,
                LinkListSortDescriptionParam = linkListSortDescriptionParam,
                WarningsParam = warningsParam,
                PresenterSize = presenterSize,
                Theme = theme,
                CurrentLocale = locale,
                RecycleBinParam = recycleBinParam,
                EmptyRecycleBinParam = emptyRecycleBinParam
            };
        }

        public string Serialize(Settings settings)
        {
            if (settings == null)
                return null;

            string presenterSizeData = $"{settings.PresenterSize.Width}x{settings.PresenterSize.Height}";
            string themeData = new WindowThemeSerializer().Serialize(settings.Theme as Models.Themes.WindowTheme);
            string localeData = new LocaleSerializer().Serialize(settings.CurrentLocale as Models.Localization.Locale);

            return $"GroupSortPropertyName={settings.GroupSortPropertyName} " +
                   $"GroupListSortDescriptionParam={settings.GroupListSortDescriptionParam} " +
                   $"LinkSortPropertyName={settings.LinkSortPropertyName} " +
                   $"LinkListSortDescriptionParam={settings.LinkListSortDescriptionParam} " +
                   $"WarningsParam={settings.WarningsParam} " +
                   $"PresenterSize={presenterSizeData} " +
                   $"Theme=[{themeData}] " +
                   $"CurrentLocale=[{localeData}] " +
                   $"RecycleBinParam={settings.RecycleBinParam} " +
                   $"EmptyRecycleBinParam={settings.EmptyRecycleBinParam}";
        }
    }
}
