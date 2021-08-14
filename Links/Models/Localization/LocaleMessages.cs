namespace Links.Models.Localization
{
    internal class LocaleMessages : ILocaleMessages
    {
        public string SuccessfulLinksImportingInfo { get; set; }
        public string SuccessfulLinksExportingInfo { get; set; }
        public string NoSelectedLinksInfo { get; set; }

        public string LoadProgramStateQuestion { get; set; }
        public string SaveProgramStateQuestion { get; set; }
        public string AutoLinksDistributionQuestion { get; set; }
        public string DeleteGroupQuestion { get; set; }
        public string DeleteLinkQuestion { get; set; }

        public string DeleteGroupError { get; set; }
        public string FollowLinkError { get; set; }
        public string DeleteLinkError { get; set; }
        public string RestoreLinkError { get; set; }
        public string LinksImportingError { get; set; }
        public string LinksExportingError { get; set; }
    }
}
