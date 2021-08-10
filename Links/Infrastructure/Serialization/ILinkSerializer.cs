using Links.Models.Collections;
using System.Collections.Generic;

namespace Links.Infrastructure.Serialization
{
    internal interface ILinkSerializer
    {
        LinkInfo Deserialize(string data);
        IEnumerable<LinkInfo> DeserializeMany(string data);

        string Serialize(LinkInfo link);
        string SerializeMany(IEnumerable<LinkInfo> links);
    }
}
