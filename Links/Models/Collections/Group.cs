using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Links.Models.Collections
{
    internal class Group : IGroup, ICollection<LinkInfo>
    {
        public int Count => Links?.Count ?? 0;
        public bool IsReadOnly => false;

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

        public bool EqualTo(Group other)
        {
            if (other == null ||
                Count != other.Count ||
                Name != other.Name ||
                !Icon.Equals(other.Icon))
            {
                return false;
            }

            if (Count == 0)
                return true;

            foreach (LinkInfo link in Links)
            {
                if (!other.Links.Contains(link))
                    return false;
            }

            return true;
        }

        public void MergeWith(Group other)
        {
            if (other == null || other.Links?.Count == 0)
                return;

            RedefineLinks(other.Links.ToArray());
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

            foreach (LinkInfo link in newLinks)
                link.ParentGroup = this;

            Links = new ObservableCollection<LinkInfo>(Links.Concat(newLinks));
        }

        public Group CopyDesign()
        {
            var icon = Icon.Clone() as GroupIcon;
            return new Group(Name, icon, null);
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

        public void Add(LinkInfo item)
        {
            Links.Add(item);
        }

        public void Clear()
        {
            Links.Clear();
        }

        public bool Contains(LinkInfo item)
        {
            return Links.Contains(item);
        }

        public void CopyTo(LinkInfo[] array, int arrayIndex)
        {
            Links.CopyTo(array, arrayIndex);
        }

        public bool Remove(LinkInfo item)
        {
            return Links.Remove(item);
        }

        public bool RemoveRange(IEnumerable<LinkInfo> items)
        {
            if (items == null)
                return true;

            bool result = true;

            foreach (LinkInfo link in items)
            {
                if (!Remove(link))
                    result = false;
            }

            return result;
        }
    }
}
