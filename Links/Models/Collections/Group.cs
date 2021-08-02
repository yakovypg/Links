using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Links.Models.Collections
{
    internal class Group : IGroup, IEnumerable<LinkInfo>
    {
        public string Name { get; set; }
        public GroupIcon Icon { get; set; }
        public ObservableCollection<LinkInfo> Links { get; }

        public Group() : this(string.Empty)
        {
        }

        public Group(string name, ObservableCollection<LinkInfo> links = null) : this(name, null, links)
        {
        }

        public Group(string name, GroupIcon icon, ObservableCollection<LinkInfo> links)
        {
            Name = name;
            Icon = icon ?? new GroupIcon();
            Links = links ?? new ObservableCollection<LinkInfo>();
        }

        public LinkInfo this[int index]
        {
            get => Links[index];
            set => Links[index] = value;
        }

        public IEnumerator<LinkInfo> GetEnumerator()
        {
            return Links.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
