using Links.Models.Collections;

namespace Links.Models
{
    internal class GroupCreator
    {
        public string Name { get; set; }
        public GroupIcon Icon { get; set; }

        public GroupCreator()
        {
            Icon = new GroupIcon();
        }

        public Group Create()
        {
            return new Group(Name, Icon, null);
        }
    }
}
