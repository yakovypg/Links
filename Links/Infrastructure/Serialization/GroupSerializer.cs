using Links.Infrastructure.Extensions;
using Links.Infrastructure.Serialization.Base;
using Links.Models.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Links.Infrastructure.Serialization
{
    internal class GroupSerializer : Serializer<Group>, IMultiSerializer<Group>
    {
        public override Group Deserialize(string data)
        {
            var item = new SerializeDataParser().ParseData(data).SerializationItem;

            string name = item.GetValue("Name");

            int colorIndex = int.Parse(item.GetValue("Icon"));
            var icon = new GroupIcon((GroupIcon.Colors)colorIndex);

            string linksData = item.GetValue("Links").Extract(1, 1);
            var linksCollection = new LinkSerializer().DeserializeMany(linksData);

            var links = !linksCollection.IsNullOrEmpty()
                ? new ObservableCollection<LinkInfo>(linksCollection)
                : new ObservableCollection<LinkInfo>();

            return new Group(name, icon, links);
        }

        public IEnumerable<Group> DeserializeMany(string data)
        {
            var item = new SerializeDataParser().ParseData(data).SerializationItem;

            if (item == null || item.Children.Count == 0)
                return null;

            var groupsPresenters = item.Children[0]?.Children;

            if (groupsPresenters == null || groupsPresenters.Count == 0)
                return null;

            var groups = new List<Group>();

            for (int i = 0; i < groupsPresenters.Count; ++i)
            {
                string groupData = groupsPresenters[i]?.GetValuesDataString();

                if (!string.IsNullOrEmpty(groupData))
                {
                    var group = Deserialize(groupData);

                    if (group != null)
                        groups.Add(group);
                }
            }

            return groups;
        }

        public override string Serialize(Group group)
        {
            return Serialize(group, true);
        }

        public string Serialize(Group group, bool addInfo)
        {
            if (group == null)
                return GenerateNullValueDataString();

            string linksData = new LinkSerializer().SerializeMany(group.Links);

            var dict = new Dictionary<string, object>()
            {
                { "Name", group.Name },
                { "Icon", group.Icon.ColorIndex },
                { "Links", linksData.Surround(START_COMPLEX_TYPE, END_COMPLEX_TYPE) },
            };

            string data = ConvertToDataString(dict);
            return GenerateFullDataString(data, addInfo);
        }

        public string SerializeMany(IEnumerable<Group> groups)
        {
            if (groups.IsNullOrEmpty())
                return GenerateNullValueDataString();

            int currPos = 0;
            int groupsCount = groups.Count();
            var dataBuilder = new StringBuilder(START_COMPLEX_TYPE + "");

            foreach (Group group in groups)
            {
                currPos++;
                string currData = Serialize(group, false).Surround(START_COMPLEX_TYPE, END_COMPLEX_TYPE);

                dataBuilder.Append(currData);

                if (currPos < groupsCount)
                    dataBuilder.Append(DELIMITER);
            }

            string data = dataBuilder.Append(END_COMPLEX_TYPE).ToString();
            return GenerateFullDataString(data);
        }
    }
}
