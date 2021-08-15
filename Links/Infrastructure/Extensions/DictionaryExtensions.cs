using System;
using System.Collections.Generic;

namespace Links.Infrastructure.Extensions
{
    internal static class Dictionary
    {
        public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dict, Predicate<TKey> predicate)
        {
            foreach (var kvPair in dict)
            {
                if (predicate(kvPair.Key))
                    return kvPair.Value;
            }

            return default;
        }
    }
}
