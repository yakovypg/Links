namespace Links.Models.Localization
{
    internal interface ILocale
    {
        string DisplayName { get; }
        string CultureName { get; }

        string GroupsSorting { get; set; }
        string LinksSorting { get; set; }
        string SortingMode { get; set; }

        string LinkSize { get; set; }
        string Theme { get; set; }
        string Language { get; set; }

        string UseRecycleBin { get; set; }
        string EmptyRecycleBin { get; set; }

        string Close { get; set; }
        string Reset { get; set; }
        string Import { get; set; }
        string Export { get; set; }
        string Restore { get; set; }
        string Remove { get; set; }
        string Empty { get; set; }

        string Name { get; set; }
        string Colour { get; set; }

        string Title { get; set; }
        string Link { get; set; }
        string Group { get; set; }
        string Image { get; set; }

        string Ok { get; set; }
        string Add { get; set; }

        string Unsorted { get; set; }
    }
}
