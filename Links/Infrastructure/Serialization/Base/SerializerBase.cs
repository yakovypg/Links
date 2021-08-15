using Links.Infrastructure.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Links.Infrastructure.Serialization.Base
{
    internal abstract class SerializerBase
    {
        public const char START_COMPLEX_TYPE = '[';
        public const char END_COMPLEX_TYPE = ']';

        public const char START_IGNORE_SPACE = '{';
        public const char END_IGNORE_SPACE = '}';

        public const char ASSIGN = '=';
        public const char DELIMITER = ' ';

        public const char NULL_VALUE = '?';
        public const char END_SYSTEM_INFO = ';';

        public static string NullValueStr = "" + NULL_VALUE;

        public static string GetDataString(IReadOnlyDictionary<string, object> nameItemDict)
        {
            return GetDataString(nameItemDict.Values, nameItemDict.Keys);
        }

        public static string GetDataString(IEnumerable<object> items)
        {
            if (items.IsNullOrEmpty())
                return null;

            var dataBuilder = new StringBuilder();

            foreach (var item in items)
            {
                string part = START_COMPLEX_TYPE + item.ToString() + END_COMPLEX_TYPE + DELIMITER;
                dataBuilder.Append(part);
            }

            return dataBuilder.ToString().Extract(0, 1);
        }

        public static string GetDataString(IEnumerable<object> items, IEnumerable<string> names)
        {
            if (items.IsNullOrEmpty())
                return null;

            string[] itemsNames = names.ToArray();
            var dataBuilder = new StringBuilder();

            int index = 0;

            foreach (var item in items)
            {
                string part = itemsNames[index++] + ASSIGN + item.ToString() + DELIMITER;
                dataBuilder.Append(part);
            }

            return dataBuilder.ToString().Extract(0, 1);
        }

        protected string GenerateNullValueDataString()
        {
            return GetInfo().GetSystemInfo() + NullValueStr;
        }

        protected string GenerateFullDataString(string part, bool addInfo = true)
        {
            string info = GetInfo().GetSystemInfo();
            return addInfo ? (info + part) : part;
        }

        protected virtual string ConvertToDataString(IEnumerable<object> items, IEnumerable<string> names)
        {
            return GetDataString(items, names);
        }

        protected virtual string ConvertToDataString(IReadOnlyDictionary<string, object> nameItemDict)
        {
            return GetDataString(nameItemDict.Values, nameItemDict.Keys);
        }

        protected virtual SerializationInfo GetInfo()
        {
            string name = GetType().Name;
            return new SerializationInfo(name);
        }
    }
}
