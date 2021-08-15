using Links.Data.App;
using Links.Infrastructure.Serialization;
using Links.Infrastructure.Serialization.Base;
using Links.Models.Collections;
using Links.Models.Configuration;
using Links.Models.Localization;
using Links.Models.Themes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Links.Data
{
    internal static class DataOrganizer
    {
        public static bool TrySaveSettings(Settings settings)
        {
            if (settings == null)
                return false;

            try
            {
                string data = new SettingsSerializer().Serialize(settings);
                File.WriteAllText(AppInfo.FilePaths.Settings, data);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool TryGetSettings(out Settings settings)
        {
            try
            {
                string data = File.ReadAllText(AppInfo.FilePaths.Settings);
                settings = new SettingsSerializer().Deserialize(data);
                return true;
            }
            catch (FileNotFoundException)
            {
                settings = new SettingsControlsItems().GetDefaultSettings();
                return true;
            }
            catch
            {
                settings = new SettingsControlsItems().GetDefaultSettings();
                return false;
            }
        }

        public static bool TrySaveRecycleBin(IEnumerable<LinkInfo> deletedLinks)
        {
            if (deletedLinks == null || deletedLinks.Count() == 0)
            {
                return TrySaveGroups(AppInfo.FilePaths.RecycleBin, null);
            }

            var groupOrganiser = new GroupOrganizer();
            IEnumerable<Group> groups = groupOrganiser.DistributeLinks(deletedLinks);

            return TrySaveGroups(AppInfo.FilePaths.RecycleBin, groups);
        }

        public static bool TryGetRecycleBin(out IEnumerable<LinkInfo> recycleBin)
        {
            if (!TryGetGroups(AppInfo.FilePaths.RecycleBin, out IEnumerable<Group> groups, out Exception exception))
            {
                recycleBin = null;
                return exception is FileNotFoundException;
            }

            var groupOrganizer = new GroupOrganizer();
            groupOrganizer.RedefineParentsForLinks(groups);

            var groupAnalyzer = new GroupAnalyzer();
            recycleBin = groupAnalyzer.GetAllLinks(groups);

            return true;
        }

        public static bool TryGetLocales(out IEnumerable<Locale> locales)
        {
            var serializer = new LocaleSerializer();
            return TryGetItems(AppInfo.Directories.Locales, serializer, out locales);
        }

        public static bool TryGetWindowThemes(out IEnumerable<WindowTheme> themes)
        {
            var serializer = new WindowThemeSerializer();
            return TryGetItems(AppInfo.Directories.Themes, serializer, out themes);
        }

        public static bool TrySaveItems<T>(string directory, IEnumerable<T> items, IEnumerable<string> names, ISerializer<T> serializer)
        {
            if (items == null || names == null)
                return false;

            if (items.Count() != names.Count())
                throw new ArgumentException("The number of items does not match the number of names.");

            int index = 0;
            bool result = true;
            string[] namesArr = names.ToArray();

            foreach (T item in items)
            {
                try
                {
                    string path = Path.Combine(directory, namesArr[index++] + ".ser");
                    string data = serializer.Serialize(item);

                    File.WriteAllText(path, data);
                }
                catch
                {
                    result = false;
                }
            }

            return result;
        }

        public static bool TryGetItems<T>(string directory, ISerializer<T> serializer, out IEnumerable<T> items)
        {
            var itemsList = new List<T>();

            try
            {
                var dirInfo = new DirectoryInfo(directory);
                FileInfo[] files = dirInfo.GetFiles();

                if (files == null || files.Length == 0)
                {
                    items = itemsList;
                    return false;
                }

                foreach (FileInfo file in files)
                {
                    try
                    {
                        string data = File.ReadAllText(file.FullName);
                        T item = serializer.Deserialize(data);

                        if (item != null)
                            itemsList.Add(item);
                    }
                    catch { }
                }
            }
            catch { }

            items = itemsList.Count > 0 ? itemsList : null;
            return true;
        }

        public static bool TryImportGroups(out IEnumerable<Group> groups, out DialogResult dialogResult)
        {
            groups = null;
            dialogResult = DialogProvider.GetFilePath(out string path);

            if (string.IsNullOrEmpty(path))
                return false;

            try
            {
                if (!TryGetGroups(path, out groups))
                    return false;

                var groupOrganizer = new GroupOrganizer();
                groupOrganizer.RedefineParentsForLinks(groups);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool TryExportGroups(IEnumerable<Group> groups, out DialogResult dialogResult)
        {
            dialogResult = DialogProvider.GetSavingFilePath(out string path);
            return !string.IsNullOrEmpty(path) && TrySaveGroups(path, groups);
        }

        public static bool TrySaveGroups(IEnumerable<Group> groups)
        {
            return TrySaveGroups(AppInfo.FilePaths.LinkCollection, groups);
        }

        public static bool TryGetGroups(out IEnumerable<Group> groups)
        {
            return TryGetGroups(AppInfo.FilePaths.LinkCollection, out groups);
        }

        public static bool TryGetGroups(out IEnumerable<Group> groups, out Exception exception)
        {
            return TryGetGroups(AppInfo.FilePaths.LinkCollection, out groups, out exception);
        }

        private static bool TryGetGroups(string path, out IEnumerable<Group> groups)
        {
            return TryGetGroups(path, out groups, out Exception _);
        }

        private static bool TrySaveGroups(string path, IEnumerable<Group> groups)
        {
            try
            {
                string data = new GroupSerializer().SerializeMany(groups);
                File.WriteAllText(path, data);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool TryGetGroups(string path, out IEnumerable<Group> groups, out Exception exception)
        {
            try
            {
                string data = File.ReadAllText(path);
                groups = new GroupSerializer().DeserializeMany(data);
                exception = null;

                return true;
            }
            catch (Exception ex)
            {
                groups = null;
                exception = ex;

                return false;
            }
        }
    }
}
