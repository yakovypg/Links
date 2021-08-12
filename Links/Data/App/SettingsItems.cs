using Links.Models.Configuration;
using Links.Models.Localization;
using Links.Models.Themes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Links.Data.App
{
    internal class SettingsItems
    {
        public ILocale CurrentLocale { get; set; }

        public SettingsItems(ILocale locale = null)
        {
            CurrentLocale = locale ?? Locale.English;
        }

        public ISettings GetDefaultSettings()
        {
            return new Settings()
            {
                GroupSortPropertyName = CurrentLocale.Name,
                GroupListSortDescriptionParam = CurrentLocale.Ascending,
                LinkSortPropertyName = CurrentLocale.Title,
                LinkListSortDescriptionParam = CurrentLocale.Ascending,
                WarningsParam = CurrentLocale.Off,
                PresenterSize = new Size(150, 230),
                Theme = WindowTheme.Dark,
                CurrentLocale = Locale.English,
                RecycleBinParam = CurrentLocale.On,
                EmptyRecycleBinParam = GetEmptyRecycleBinParams()[0]
            };
        }

        public ObservableCollection<string> GetGroupSortings()
        {
            return new ObservableCollection<string>(new string[]
            {
                CurrentLocale.Name,
                CurrentLocale.Color
            });
        }

        public ObservableCollection<string> GetLinkSortings()
        {
            return new ObservableCollection<string>(new string[]
            {
                CurrentLocale.Title,
                CurrentLocale.Link,
                CurrentLocale.Date
            });
        }

        public ObservableCollection<string> GetSortingMods()
        {
            return new ObservableCollection<string>(new string[]
            {
                CurrentLocale.Ascending,
                CurrentLocale.Descending
            });
        }

        public ObservableCollection<Size> GetPresenterSizes()
        {
            return new ObservableCollection<Size>(new Size[]
            {
                new Size(90, 138),
                new Size(120, 184),
                new Size(150, 230),
                new Size(180, 276),
                new Size(210, 322),
                new Size(240, 368),
                new Size(270, 414)
            });
        }

        public ObservableCollection<IWindowTheme> GetThemes()
        {
            IEnumerable<IWindowTheme> themes = DataParser.GetWindowThemes();

            var defaultThemes = new ObservableCollection<IWindowTheme>(new IWindowTheme[]
            {
                WindowTheme.Blue,
                WindowTheme.Dark,
                WindowTheme.Light
            });

            return themes != null
                ? new ObservableCollection<IWindowTheme>(themes)
                : defaultThemes;
        }

        public ObservableCollection<ILocale> GetLocales()
        {
            IEnumerable<ILocale> locales = DataParser.GetLocales();

            var defaultLocales = new ObservableCollection<ILocale>(new ILocale[]
            {
                Locale.English
            });

            return locales != null
                ? new ObservableCollection<ILocale>(locales)
                : defaultLocales;
        }

        public ObservableCollection<string> GetOnOffParams()
        {
            return new ObservableCollection<string>(new string[]
            {
                CurrentLocale.On,
                CurrentLocale.Off
            });
        }

        public ObservableCollection<string> GetEmptyRecycleBinParams()
        {
            return new ObservableCollection<string>(new string[]
            {
                CurrentLocale.Never,
                $"{CurrentLocale.Every} 3 {CurrentLocale.Days}",
                $"{CurrentLocale.Every} 7 {CurrentLocale.Days}",
                $"{CurrentLocale.Every} 14 {CurrentLocale.Days}",
                $"{CurrentLocale.Every} 30 {CurrentLocale.Days}",
                $"{CurrentLocale.Every} 90 {CurrentLocale.Days}",
                $"{CurrentLocale.Every} 180 {CurrentLocale.Days}",
                $"{CurrentLocale.Every} 365 {CurrentLocale.Days}"
            });
        }
    }
}
