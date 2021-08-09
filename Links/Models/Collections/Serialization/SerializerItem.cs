using System.Collections.Generic;
using System.Text;

namespace Links.Models.Collections.Serialization
{
    internal class SerializerItem
    {
        private readonly Dictionary<string, string> _items;

        public SerializerItem(string data)
        {
            _items = new Dictionary<string, string>();

            IEnumerable<string> splittedData = SplitData(data, ' ');
            ParseData(splittedData);
        }

        public static IEnumerable<string> SplitData(string data, char separator)
        {
            var splittedData = new List<string>();
            var itemBuilder = new StringBuilder();

            int smbIndex = 0;
            bool isBracketOpen = false;

            foreach (char smb in data)
            {
                if (smb == '[')
                    isBracketOpen = true;
                else if (smb == ']')
                    isBracketOpen = false;

                if (smb == separator && !isBracketOpen)
                {
                    splittedData.Add(itemBuilder.ToString());
                    _ = itemBuilder.Clear();
                }
                else if (smbIndex == data.Length - 1)
                {
                    splittedData.Add(itemBuilder.Append(smb).ToString());
                    _ = itemBuilder.Clear();
                }

                if (smb != separator || isBracketOpen)
                    _ = itemBuilder.Append(smb);

                smbIndex++;
            }

            return splittedData;
        }

        public string GetValue(string key)
        {
            return _items.GetValueOrDefault(key);
        }

        private void ParseData(IEnumerable<string> splittedData)
        {
            if (splittedData == null)
                return;

            foreach (string item in splittedData)
            {
                int index = item.IndexOf("=");

                if (index == -1)
                {
                    _items.Add(item, null);
                    continue;
                }
                if (index == item.Length - 1)
                {
                    _items.Add(item.Remove(index), null);
                    continue;
                }

                string key = item.Remove(index);
                string value = item.Substring(index + 1);

                _items.Add(key, value);
            }
        }
    }
}
