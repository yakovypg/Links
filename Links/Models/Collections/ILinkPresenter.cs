namespace Links.Models.Collections
{
    internal interface ILinkPresenter
    {
        LinkInfo Self { get; }

        bool IsLinkMoved { get; }
        System.Windows.Visibility PresenterVisibility { get; set; }

        void CancelMove();
        void ResetPrimaryGroup();
    }
}
