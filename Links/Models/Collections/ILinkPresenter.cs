namespace Links.Models.Collections
{
    internal interface ILinkPresenter
    {
        LinkInfo Self { get; }

        bool IsLinkMoved { get; }
        bool IsSelected { get; set; }
        System.Windows.Visibility PresenterVisibility { get; set; }

        void CancelMove();
        void ResetPrimaryGroup();
    }
}
