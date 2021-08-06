namespace Links.Models.Localization
{
    internal interface ILocale
    {
        string DisplayName { get; }
        string CultureName { get; }

        string GroupsSorting { get; set; }
        string LinksSorting { get; set; }
        string SortingMode { get; set; }

        string Warnings { get; set; }
        string PresenterSize { get; set; }
        string Theme { get; set; }
        string Language { get; set; }

        string RecycleBin { get; set; }
        string EmptyRecycleBin { get; set; }

        string Days { get; set; }
        string Every { get; set; }
        string Never { get; set; }

        string Unsorted { get; set; }

        string Close { get; set; }
        string Reset { get; set; }
        string Import { get; set; }
        string Export { get; set; }
        string Restore { get; set; }
        string Remove { get; set; }
        string Empty { get; set; }

        string Name { get; set; }
        string Colour { get; set; }

        string Date { get; set; }
        string Title { get; set; }
        string Link { get; set; }
        string Group { get; set; }
        string Image { get; set; }

        string Ok { get; set; }
        string On { get; set; }
        string Off { get; set; }

        string Add { get; set; }
        string Find { get; set; }

        string Ascending { get; set; }
        string Descending { get; set; }

        string Error { get; set; }
        string Warning { get; set; }
        string Question { get; set; }
        string Comment { get; set; }

        LocaleMessages LocaleMessages { get; set; }
    }
}
