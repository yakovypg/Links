using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Links.Models.Collections
{
    internal class Group : IGroup, IEnumerable<LinkInfo>
    {
        public string Name { get; set; }
        public SolidColorBrush IconForeground { get; set; }
        public ObservableCollection<LinkInfo> Links { get; }

        public Group() : this(string.Empty)
        {
        }

        public Group(string name, ObservableCollection<LinkInfo> links = null) : this(name, null, links)
        {
        }

        public Group(string name, SolidColorBrush iconForeground, ObservableCollection<LinkInfo> links)
        {
            Name = name;
            IconForeground = iconForeground ?? Brushes.Gold;
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
    }
}
