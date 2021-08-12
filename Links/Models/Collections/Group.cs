﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Links.Models.Collections
{
    //Do not override Equals and GetHashCode

    internal class Group : IGroup
    {
        public int Count => Links.Count;
        public bool IsReadOnly => false;

        public string Name { get; set; }
        public IGroupIcon Icon { get; set; }
        public ObservableCollection<LinkInfo> Links { get; private set; }

        public GroupIcon.Colors Color
        {
            get => Icon.ForegroundColor;
            set => Icon.ForegroundColor = value;
        }

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

        public bool EqualTo(IGroup other)
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

            foreach (var link in Links)
            {
                if (!other.Links.Contains(link))
                    return false;
            }

            return true;
        }

        public void MergeWith(IGroup other)
        {
            if (other == null || other.Links?.Count == 0)
                return;

            AddMany(other.Links.ToArray());
        }

        public void AddMany(params LinkInfo[] items)
        {
            if (items == null || items.Length == 0)
                return;

            foreach (var link in items)
                link.ParentGroup = this;

            Links = new ObservableCollection<LinkInfo>(Links.Concat(items));
        }

        public bool RemoveRange(IEnumerable<LinkInfo> items)
        {
            if (items == null)
                return true;

            bool result = true;

            foreach (var link in items)
            {
                if (!Remove(link))
                    result = false;
            }

            return result;
        }

        public Group CopyDesign()
        {
            var icon = new GroupIcon(Icon.ForegroundColor);
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
    }
}
