using Links.Data.App;
using Links.Models.Collections;
using Links.Models.Collections.Serialization;
using Links.Models.Configuration;
using Links.Models.Localization;
using Links.Models.Themes;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

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

        public static bool TryImportGroups(out IEnumerable<Group> groups, out DialogResult dialogResult)
        {
            groups = null;
            dialogResult = DialogProvider.GetFilePath(out string path);

            if (string.IsNullOrEmpty(path))
                return false;

            try
            {
                string data = File.ReadAllText(path);
                groups = new GroupSerializer().DeserializeMany(data);

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

            if (string.IsNullOrEmpty(path))
                return false;

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
    }
}
