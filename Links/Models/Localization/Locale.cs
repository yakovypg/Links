namespace Links.Models.Localization
{
    internal class Locale : ILocale
    {
        public static Locale English => new English();

        public string DisplayName { get; }
        public string CultureName { get; }

        public string GroupsSorting { get; set; }
        public string LinksSorting { get; set; }
        public string SortingMode { get; set; }

        public string LinkSize { get; set; }
        public string Theme { get; set; }
        public string Language { get; set; }

        public string UseRecycleBin { get; set; }
        public string EmptyRecycleBin { get; set; }

        public string Reset { get; set; }
        public string Import { get; set; }
        public string Export { get; set; }
        public string Restore { get; set; }
        public string Remove { get; set; }
        public string Empty { get; set; }

        public string Name { get; set; }
        public string Colour { get; set; }

        public string Title { get; set; }
        public string Link { get; set; }
        public string Group { get; set; }
        public string Image { get; set; }

        public string Ok { get; set; }

        public Locale(string displayName, string cultureName)
        {
            DisplayName = displayName;
            CultureName = cultureName;
        }
    }
}
