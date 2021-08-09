using System.Collections.Generic;

namespace Links.Models.Collections.Comparers
{
    internal class GroupEqualityComparer : IEqualityComparer<Group>
    {
        public bool Equals(Group x, Group y)
        {
            if (x == null && y == null)
                return true;

            if (x == null || y == null)
                return false;

            return x.EqualTo(y);
        }

        public int GetHashCode(Group obj)
        {
            return obj.GetHashCode();
        }
    }
}
