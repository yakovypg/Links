using System;
using System.Collections.Generic;

namespace Links.Infrastructure.Serialization.Attributes
{
    internal class SerializerAttributeEqualityComparer : IEqualityComparer<Attribute>
    {
        public bool Equals(Attribute x, Attribute y)
        {
            if (x == null && y == null)
                return true;

            if (x == null || y == null)
                return false;

            return x.GetType() == y.GetType();
        }

        public int GetHashCode(Attribute obj)
        {
            return obj.GetHashCode();
        }
    }
}
