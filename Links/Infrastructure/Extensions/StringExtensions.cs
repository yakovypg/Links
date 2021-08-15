namespace Links.Infrastructure.Extensions
{
    internal static class StringExtensions
    {
        public static string Surround(this string str, char start, char end)
        {
            return start + str + end;
        }

        public static string Extract(this string str, int removeStart = 1, int removeEnd = 1)
        {
            return str.Remove(str.Length - removeEnd).Substring(removeStart);
        }
    }
}
