using Links.Models.Collections;
using Links.ViewModels.Base;

namespace Links.ViewModels
{
    internal class LinkViewModel : ViewModel
    {
        public LinkInfo Source { get; }

        public string Title => Source.Title;
        public string Link => Source.Link;

        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set => SetValue(ref _isChecked, value);
        }

        public LinkViewModel(LinkInfo link)
        {
            Source = link;
        }
    }
}
