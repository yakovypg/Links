using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Links.Models.Collections
{
    internal class Group : IGroup, IEnumerable<LinkInfo>
    {
        public string Name { get; set; }
        public GroupIcon Icon { get; set; }
        public ObservableCollection<LinkInfo> Links { get; private set; }

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

        public void AddLinks(params LinkInfo[] links)
        {
            if (links == null)
                return;

            foreach (LinkInfo link in links)
                Links.Add(link);
        }

        public void RedefineLinks(params LinkInfo[] newLinks)
        {
            if (newLinks == null || newLinks.Length == 0)
            {
                Links = new ObservableCollection<LinkInfo>(Links);
                return;
            }

            Links = new ObservableCollection<LinkInfo>(Links.Concat(newLinks));
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
