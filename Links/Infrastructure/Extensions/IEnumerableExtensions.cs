using System;
using System.Collections.Generic;
using System.Linq;

namespace Links.Infrastructure.Extensions
{
    internal static class IEnumerableExtensions
    {
        public static IEnumerable<string> ToStringEnumerable<T>(this IEnumerable<T> enumerable)
        {
            int index = 0;
            string[] output = new string[enumerable.Count()];

            foreach (T item in enumerable)
            {
                output[index++] = item.ToString();
            }

            return output;
        }

        public static IEnumerable<Attribute> ToAttributeEnumerable(this IEnumerable<object> enumerable)
        {
            int index = 0;
            var output = new Attribute[enumerable.Count()];

            foreach (var item in enumerable)
            {
                output[index++] = item as Attribute;
            }

            return output;
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable is null || enumerable.Count() == 0;
        }
    }
}
