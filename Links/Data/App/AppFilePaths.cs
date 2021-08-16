using System;
using System.IO;
using Links.Models.Configuration;

namespace Links.Data.App
{
    internal class AppFilePaths
    {
        public string Settings { get; }
        public string LinkCollection { get; }
        public string RecycleBin { get; }
        public string LastRecycleBinCleaning { get; }

        public bool IsFilesInOrder => File.Exists(Settings) && File.Exists(LinkCollection) &&
            File.Exists(RecycleBin) && File.Exists(LastRecycleBinCleaning);

        public AppFilePaths(AppDirectories directories)
        {
            if (directories == null)
                throw new ArgumentNullException();

            Settings = Path.Combine(directories.Data, "settings.ser");
            LinkCollection = Path.Combine(directories.Links, "links.groups");
            RecycleBin = Path.Combine(directories.Links, "recyclebin.groups");
            LastRecycleBinCleaning = Path.Combine(directories.Data, "lastRecycleBinCleaning.txt");
        }

        public bool TryRestoreFiles()
        {
            try
            {
                if (!File.Exists(Settings) && !DataOrganizer.TrySaveSettings(new SettingsControlsItems().GetDefaultSettings()))
                    return false;

                if (!File.Exists(LinkCollection) && !DataOrganizer.TrySaveGroups(null))
                    return false;

                if (!File.Exists(RecycleBin) && !DataOrganizer.TrySaveRecycleBin(null))
                    return false;

                if (!File.Exists(LastRecycleBinCleaning))
                    File.Create(LastRecycleBinCleaning).Close();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
