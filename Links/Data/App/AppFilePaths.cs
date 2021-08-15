using System;
using System.IO;

namespace Links.Data.App
{
    internal class AppFilePaths
    {
        public string Settings { get; }
        public string LinkCollection { get; }
        public string RecycleBin { get; }
        public string LastRecycleBinCleaning { get; }

        public AppFilePaths(AppDirectories directories)
        {
            if (directories == null)
                throw new ArgumentNullException();

            Settings = Path.Combine(directories.Data, "settings.ser");
            LinkCollection = Path.Combine(directories.Links, "links.groups");
            RecycleBin = Path.Combine(directories.Links, "recyclebin.groups");
            LastRecycleBinCleaning = Path.Combine(directories.Data, "lastRecycleBinCleaning.txt");
        }
    }
}
