using Links.Models.Collections;

namespace Links.Models
{
    internal interface IGroupCreator
    {
        string Name { get; set; }
        GroupIcon Icon { get; set; }

        Group CreateGroup();
    }
}
