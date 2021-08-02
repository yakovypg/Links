namespace Links.Data.App
{
    internal static class AppInfo
    {
        public static string Name { get; } = "Links";
        public static string Version { get; } = "1.2";
        public static string Developer { get; } = "Y-Progs";

        public static string GenerateString()
        {
            return $"Name: {Name}\n" +
                   $"Version: {Version}\n" +
                   $"Developer: {Developer}";
        }
    }
}
