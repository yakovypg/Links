namespace Links.Models.Localization
{
    internal sealed class English : Locale
    {
        public English() : base("English", "en-US")
        {
            GroupsSorting = "Groups sorting";
            LinksSorting = "Links sorting";
            SortingMode = "Sorting mode";

            Warnings = "Warnings";
            PresenterSize = "Presenter size";
            Theme = "Theme";
            Language = "Language";

            RecycleBin = "Recycle bin";
            EmptyRecycleBin = "Empty recycle bin";

            Days = "Days";
            Every = "Every";
            Never = "Never";

            Unsorted = "Unsorted";

            Close = "Close";
            Reset = "Reset";
            Import = "Import";
            Export = "Export";
            Restore = "Restore";
            Remove = "Remove";
            Empty = "Empty";

            Name = "Name";
            Colour = "Colour";

            Date = "Date";
            Title = "Title";
            Link = "Link";
            Group = "Group";
            Image = "Image";

            Ok = "Ok";
            On = "On";
            Off = "Off";

            Add = "Add";
            Find = "Find";

            Ascending = "Ascending";
            Descending = "Descending";

            Error = "Error";
            Warning = "Warning";
            Question = "Question";
            Comment = "Comment";

            LocaleMessages = new LocaleMessages()
            {
                DeleteGroupQuestion = "Delete all links from the group?",
                DeleteLinkWarning = "Are you sure you want to delete the link?",

                DeleteGroupError = "Failed to delete the group.",
                FollowLinkError = "Failed to follow the link.",
                DeleteLinkError = "Failed to delete the link.",
                RestoreLinkError = "Failed to restore the link."
            };
        }
    }
}
