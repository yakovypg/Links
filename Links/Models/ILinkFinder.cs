using Links.Models.Collections;
using System.Collections.Generic;

namespace Links.Models
{
    internal interface ILinkFinder
    {
        string Filter { get; }
        string Flags { get; }

        IEnumerable<LinkInfo> FindAmong(IEnumerable<Group> groups);
    }
}
