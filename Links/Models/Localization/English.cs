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
            Color = "Color";

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
            CheckAll = "Check all";

            Ascending = "Ascending";
            Descending = "Descending";

            Error = "Error";
            Warning = "Warning";
            Question = "Question";
            Information = "Information";
            Comment = "Comment";

            LocaleMessages = new LocaleMessages()
            {
                SuccessfulLinksImportingInfo = "The links were successfully imported.",
                SuccessfulLinksExportingInfo = "The links were successfully exported.",
                NoSelectedLinksInfo = "You have not selected any links.",

                LoadProgramStateQuestion = "Failed to load program state. Try again?",
                SaveProgramStateQuestion = "Failed to save program state. Try again? Some data may be lost if you click 'No'.",
                AutoLinksDistributionQuestion = "Apply automatic link allocation? Click 'No' to put all the links in one folder.",
                DeleteGroupQuestion = "Are you sure you want to delete the group?",
                DeleteLinkQuestion = "Are you sure you want to delete the link?",

                DeleteGroupError = "Failed to delete the group.",
                FollowLinkError = "Failed to follow the link.",
                DeleteLinkError = "Failed to delete the link.",
                RestoreLinkError = "Failed to restore the link.",
                LinksImportingError = "Failed to import the links.",
                LinksExportingError = "Failed to export the links."
            };
        }
    }
}
