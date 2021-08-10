using Links.Data.App;
using Links.Infrastructure.Serialization;
using Links.Models.Collections;
using Links.Models.Configuration;
using Links.Models.Localization;
using Links.Models.Themes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Links.Data
{
    internal static class DataParser
    {
        public static bool SaveSettings(Settings settings)
        {
            if (settings == null)
                return false;

            try
            {
                string json = JsonConvert.SerializeObject(settings);
                File.WriteAllText(AppInfo.FilePaths.Settings, json);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static ISettings GetSettings()
        {
            try
            {
                string json = File.ReadAllText(AppInfo.FilePaths.Settings);
                return JsonConvert.DeserializeObject<Settings>(json);
            }
            catch
            {
                return new SettingsItems().GetDefaultSettings();
            }
        }

        public static bool TrySaveRecycleBin(IEnumerable<LinkInfo> deletedLinks)
        {
            if (deletedLinks == null || deletedLinks.Count() == 0)
            {
                try
                {
                    File.WriteAllText(AppInfo.FilePaths.RecycleBin, "");
                    return true;
                }
                catch { return false; }
            }

            var groupAnalyzer = new GroupAnalyzer();
            IEnumerable<Group> groups = groupAnalyzer.DistributeLinks(deletedLinks);

            return TrySaveGroups(AppInfo.FilePaths.RecycleBin, groups);
        }

        public static IEnumerable<LinkInfo> GetRecycleBin()
        {
            if (!TryGetGroups(AppInfo.FilePaths.RecycleBin, out IEnumerable<Group> groups))
                return null;

            var groupAnalyzer = new GroupAnalyzer();
            return groupAnalyzer.GetAllLinks(groups);
        }

        public static IEnumerable<ILocale> GetLocales()
        {
            return GetItems<Locale>(AppInfo.Directories.Locales);
        }

        public static IEnumerable<IWindowTheme> GetWindowThemes()
        {
            return GetItems<WindowTheme>(AppInfo.Directories.Themes);
        }

        public static bool SaveItems<T>(string directory, IEnumerable<T> items, IEnumerable<string> names)
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
                    string path = Path.Combine(directory, namesArr[index++] + ".json");
                    string json = JsonConvert.SerializeObject(item);

                    File.WriteAllText(path, json);
                }
                catch
                {
                    result = false;
                }
            }

            return result;
        }

        public static IEnumerable<T> GetItems<T>(string directory)
        {
            var items = new List<T>();

            try
            {
                var dirInfo = new DirectoryInfo(directory);
                FileInfo[] files = dirInfo.GetFiles();

                if (files == null || files.Length == 0)
                    return null;

                foreach (FileInfo file in files)
                {
                    try
                    {
                        string json = File.ReadAllText(file.FullName);
                        T item = JsonConvert.DeserializeObject<T>(json);

                        if (item != null)
                            items.Add(item);
                    }
                    catch { }
                }
            }
            catch { }

            return items.Count > 0 ? items : null;
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

                if (groups == null)
                    return true;

                foreach (Group group in groups)
                {
                    if (group.Links == null)
                        continue;

                    foreach (LinkInfo link in group.Links)
                        link.ParentGroup = group;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool TryExportGroups(IEnumerable<Group> groups, out DialogResult dialogResult)
        {
            if (groups == null)
            {
                dialogResult = DialogResult.Abort;
                return false;
            }

            dialogResult = DialogProvider.GetSavingFilePath(out string path);

            return !string.IsNullOrEmpty(path) && TrySaveGroups(path, groups);
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

        private static bool TryGetGroups(string path, out IEnumerable<Group> groups)
        {
            try
            {
                string data = File.ReadAllText(path);
                groups = new GroupSerializer().DeserializeMany(data);
                return true;
            }
            catch
            {
                groups = null;
                return false;
            }
        }
    }
}
