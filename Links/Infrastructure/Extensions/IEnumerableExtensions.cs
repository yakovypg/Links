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
    }
}
