using Links.Models.Localization;
using Links.Models.Themes;
using System.Linq;
using System.Windows;

namespace Links.Data.App
{
    internal static class AppInfo
    {
        public static string Name { get; }
        public static string Version { get; }
        public static string Developer { get; }

        public static AppDirectories Directories { get; }
        public static AppFilePaths FilePaths { get; }

        static AppInfo()
        {
            Name = "Links";
            Version = "1.2";
            Developer = "Y-Progs";

            Directories = new AppDirectories();
            FilePaths = new AppFilePaths(Directories);

            if (!Directories.IsDirectoriesInOrder)
                Directories.TryRestoreDirectories();

            if (!FilePaths.IsFilesInOrder)
                FilePaths.TryRestoreFiles();

            if (!Directories.IsDirectoriesDefaultFilesInOrder)
                TryRestoreDefaultFiles();
        }

        public static string GenerateString()
        {
            return $"Name: {Name}\n" +
                   $"Version: {Version}\n" +
                   $"Developer: {Developer}";
        }

        public static void ShowInfo()
        {
            string caption = $"About {Name}";
            MessageBox.Show(GenerateString(), caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static bool TryRestoreDefaultFiles()
        {
            try
            {
                var themes = new WindowTheme[] { WindowTheme.Blue, WindowTheme.Dark, WindowTheme.Light };
                var themesNames = themes.Select(t => t.DisplayName);
                var themeSerializer = new Infrastructure.Serialization.WindowThemeSerializer();

                var locales = new Locale[] { Locale.English };
                var localesNames = locales.Select(t => t.DisplayName);
                var localeSerializer = new Infrastructure.Serialization.LocaleSerializer();

                bool isThemesSaved = DataOrganizer.TrySaveItems(Directories.Themes, themes, themesNames, themeSerializer);
                bool isLocalesSaved = DataOrganizer.TrySaveItems(Directories.Locales, locales, localesNames, localeSerializer);

                return isThemesSaved && isLocalesSaved;
            }
            catch
            {
                return false;
            }
        }
    }
}
