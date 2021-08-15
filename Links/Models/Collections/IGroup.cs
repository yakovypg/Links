using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Links.Models.Collections
{
    internal interface IGroup : ICollection<LinkInfo>
    {
        string Name { get; set; }
        IGroupIcon Icon { get; set; }
        ObservableCollection<LinkInfo> Links { get; }

        int ColorIndex { get; set; }
        GroupIcon.Colors Color { get; set; }

        bool EqualTo(IGroup other);

        void MergeWith(IGroup other);
        void AddMany(params LinkInfo[] newLinks);
        bool RemoveRange(IEnumerable<LinkInfo> items);

        Group CopyDesign();
    }
}
