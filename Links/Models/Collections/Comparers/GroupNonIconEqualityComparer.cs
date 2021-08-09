using System;
using System.Collections.Generic;

namespace Links.Models.Collections.Comparers
{
    internal class GroupNonIconEqualityComparer : IEqualityComparer<Group>
    {
        public bool Equals(Group x, Group y)
        {
            if (x == null && y == null)
                return true;

            if (x == null || y == null)
                return false;

            if (x.Count != y.Count || x.Name != y.Name)
                return false;

            if (x.Count == 0)
                return true;

            foreach (LinkInfo link in x.Links)
            {
                if (!y.Links.Contains(link))
                    return false;
            }

            return true;
        }

        public int GetHashCode(Group obj)
        {
            return HashCode.Combine(obj.Count, obj.IsReadOnly, obj.Name, obj.Links);
        }
    }
}
