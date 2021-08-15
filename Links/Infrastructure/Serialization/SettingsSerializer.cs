using Links.Infrastructure.Extensions;
using Links.Infrastructure.Serialization.Base;
using Links.Models.Configuration;
using System.Collections.Generic;

namespace Links.Infrastructure.Serialization
{
    internal class SettingsSerializer : Serializer<Settings>
    {
        public override Settings Deserialize(string data)
        {
            var item = new SerializeDataParser().ParseData(data).SerializationItem;

            if (item == null)
                return null;

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

            string themeData = item.GetValue("Theme").Extract(1, 1);
            var theme = new WindowThemeSerializer().Deserialize(themeData);

            string localeData = item.GetValue("CurrentLocale").Extract(1, 1);
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

        public override string Serialize(Settings settings)
        {
            if (settings == null)
                return GenerateNullValueDataString();

            string presenterSizeData = $"{settings.PresenterSize.Width}x{settings.PresenterSize.Height}";
            string themeData = new WindowThemeSerializer().Serialize(settings.Theme as Models.Themes.WindowTheme);
            string localeData = new LocaleSerializer().Serialize(settings.CurrentLocale as Models.Localization.Locale);

            var dict = new Dictionary<string, object>()
            {
                {"GroupSortPropertyName", settings.GroupSortPropertyName },
                {"GroupListSortDescriptionParam", settings.GroupListSortDescriptionParam },
                {"LinkSortPropertyName", settings.LinkSortPropertyName },
                {"LinkListSortDescriptionParam", settings.LinkListSortDescriptionParam },
                {"WarningsParam", settings.WarningsParam },
                {"PresenterSize", presenterSizeData },
                {"Theme", themeData.Surround(START_COMPLEX_TYPE, END_COMPLEX_TYPE) },
                {"CurrentLocale", localeData.Surround(START_COMPLEX_TYPE, END_COMPLEX_TYPE) },
                {"RecycleBinParam", settings.RecycleBinParam },
                {"EmptyRecycleBinParam", settings.EmptyRecycleBinParam.Surround(START_IGNORE_SPACE, END_IGNORE_SPACE) }
            };

            string data = ConvertToDataString(dict);
            return GenerateFullDataString(data);
        }
    }
}
