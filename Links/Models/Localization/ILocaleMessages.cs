namespace Links.Models.Localization
{
    internal interface ILocaleMessages
    {
        string SuccessfulLinksImportingInfo { get; set; }
        string SuccessfulLinksExportingInfo { get; set; }
        string NoSelectedLinksInfo { get; set; }

        string AutoLinksDistributionQuestion { get; set; }
        string DeleteGroupQuestion { get; set; }
        string DeleteLinkQuestion { get; set; }

        string DeleteGroupError { get; set; }
        string FollowLinkError { get; set; }
        string DeleteLinkError { get; set; }
        string RestoreLinkError { get; set; }
        string LinksImportingError { get; set; }
        string LinksExportingError { get; set; }
    }
}
