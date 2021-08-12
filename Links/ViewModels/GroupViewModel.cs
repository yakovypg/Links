using Links.Models.Collections;
using Links.ViewModels.Base;

namespace Links.ViewModels
{
    internal class GroupViewModel : ViewModel
    {
        private readonly Group _group;

        public string Name => _group.Name;
        public IGroupIcon Icon => _group.Icon;

        public LinkViewModel[] Links { get; }

        public GroupViewModel(Group group)
        {
            _group = group;

            if (_group == null || _group.Links.Count == 0)
                return;

            Links = new LinkViewModel[_group.Links.Count];

            for (int i = 0; i < Links.Length; ++i)
                Links[i] = new LinkViewModel(_group.Links[i]);
        }
    }
}
