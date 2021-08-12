using Links.Models.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Links.Infrastructure.Serialization
{
    internal class GroupSerializer : IGroupSerializer
    {
        public GroupSerializer()
        {
        }

        public Group Deserialize(string data)
        {
            if (string.IsNullOrEmpty(data))
                return null;

            var item = new SerializerItem(data);

            string name = item.GetValue("Name");

            int colorIndex = int.Parse(item.GetValue("Icon"));
            var icon = new GroupIcon(colorIndex);

            string linksData = item.GetValue("Links");
            IEnumerable<LinkInfo> linksList = new LinkSerializer().DeserializeMany(linksData);
            var links = new ObservableCollection<LinkInfo>(linksList);

            return new Group(name, icon, links);
        }

        public IEnumerable<Group> DeserializeMany(string data)
        {
            if (string.IsNullOrEmpty(data) || data.Length < 2)
                return null;

            data = data.Remove(data.Length - 1).Substring(1);
            string[] items = SerializerItem.SplitData(data, ',').ToArray();

            if (items == null || items.Length == 0)
                return null;

            var groups = new Group[items.Length];

            for (int i = 0; i < items.Length; ++i)
                groups[i] = Deserialize(items[i]);

            return groups;
        }

        public string Serialize(Group group)
        {
            if (group == null)
                return null;

            string linksData = new LinkSerializer().SerializeMany(group.Links);

            return $"Name={group.Name} Icon={group.Icon.ColorIndex} Links={linksData}";
        }

        public string SerializeMany(IEnumerable<Group> groups)
        {
            int groupsCount = groups.Count();

            if (groups == null || groupsCount == 0)
                return null;

            int currPos = 0;
            var dataBuilder = new StringBuilder("[");

            foreach (Group group in groups)
            {
                currPos++;
                string data = Serialize(group);

                dataBuilder.Append(data);

                if (currPos < groupsCount)
                    dataBuilder.Append(",");
            }

            return dataBuilder.Append("]").ToString();
        }
    }
}
