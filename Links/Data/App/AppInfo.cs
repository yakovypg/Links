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
    }
}
