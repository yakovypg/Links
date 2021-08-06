namespace Links.Models.Collections.Creators
{
    internal interface IGroupCreator
    {
        string Name { get; set; }
        GroupIcon Icon { get; set; }

        Group CreateGroup();
    }
}
