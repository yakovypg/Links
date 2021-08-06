namespace Links.Models.Localization
{
    internal interface ILocaleMessages
    {
        string DeleteGroupQuestion { get; set; }

        string DeleteLinkWarning { get; set; }

        string DeleteGroupError { get; set; }
        string FollowLinkError { get; set; }
        string DeleteLinkError { get; set; }
        string RestoreLinkError { get; set; }
    }
}
