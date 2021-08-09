using System.Collections.Generic;

namespace Links.Models.Collections.Serialization
{
    internal interface IGroupSerializer
    {
        Group Deserialize(string data);
        IEnumerable<Group> DeserializeMany(string data);

        string Serialize(Group group);
        string SerializeMany(IEnumerable<Group> groups);
    }
}
