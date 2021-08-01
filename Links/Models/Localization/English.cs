namespace Links.Models.Localization
{
    internal sealed class English : Locale
    {
        public English() : base("English", "en-US")
        {
            GroupsSorting = "Groups sorting";
            LinksSorting = "Links sorting";
            SortingMode = "Sorting mode";

            LinkSize = "Link size";
            Theme = "Theme";
            Language = "Language";

            UseRecycleBin = "Use recycle bin";
            EmptyRecycleBin = "Empty";

            Close = "Close";
            Reset = "Reset";
            Import = "Import";
            Export = "Export";
            Restore = "Restore";
            Remove = "Remove";
            Empty = "Empty";

            Name = "Name";
            Colour = "Colour";

            Title = "Title";
            Link = "Link";
            Group = "Group";
            Image = "Image";

            Ok = "Ok";
            Add = "Add";

            Unsorted = "Unsorted";
        }
    }
}
