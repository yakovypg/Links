using System.Collections.ObjectModel;

namespace Links.Models.Collections
{
    internal interface IGroup
    {
        string Name { get; set; }
        GroupIcon Icon { get; set; }
        ObservableCollection<LinkInfo> Links { get; }

        bool EqualTo(Group other);
        void MergeWith(Group other);

        void AddLinks(params LinkInfo[] links);
        void RedefineLinks(params LinkInfo[] newLinks);

        Group CopyDesign();
    }
}
