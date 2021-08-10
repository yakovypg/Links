using Links.Models.Collections;
using System.Collections.Generic;

namespace Links.Infrastructure.Serialization
{
    internal interface IGroupSerializer
    {
        Group Deserialize(string data);
        IEnumerable<Group> DeserializeMany(string data);

        string Serialize(Group group);
        string SerializeMany(IEnumerable<Group> groups);
    }
}
