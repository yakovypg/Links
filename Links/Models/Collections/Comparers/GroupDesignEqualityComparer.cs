using System;
using System.Collections.Generic;

namespace Links.Models.Collections.Comparers
{
    internal class GroupDesignEqualityComparer : IEqualityComparer<Group>
    {
        public bool Equals(Group x, Group y)
        {
            if (x == null && y == null)
                return true;

            if (x == null || y == null)
                return false;

            return x.Name == y.Name && x.Icon.Equals(y.Icon);
        }

        public int GetHashCode(Group obj)
        {
            return HashCode.Combine(obj.Count, obj.IsReadOnly, obj.Name, obj.Icon);
        }
    }
}
