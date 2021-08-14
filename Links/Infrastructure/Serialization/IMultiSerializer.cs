﻿using System.Collections.Generic;

namespace Links.Infrastructure.Serialization
{
    internal interface IMultiSerializer<T> : ISerializer<T>
    {
        IEnumerable<T> DeserializeMany(string data);
        string SerializeMany(IEnumerable<T> items);
    }
}
