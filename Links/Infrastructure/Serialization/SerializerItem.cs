using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Links.Infrastructure.Serialization
{
    internal class SerializerItem : IEnumerable<KeyValuePair<string, string>>
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
            int openedBrackets = 0;
            bool isBracketOpen = false;

            foreach (char smb in data)
            {
                if (smb == '[')
                {
                    openedBrackets++;
                    isBracketOpen = true;
                }
                else if (smb == ']')
                {
                    if (--openedBrackets == 0)
                        isBracketOpen = false;
                }

                if (smb == separator && openedBrackets == 0)
                {
                    splittedData.Add(itemBuilder.ToString());
                    itemBuilder.Clear();
                }
                else if (smbIndex == data.Length - 1)
                {
                    splittedData.Add(itemBuilder.Append(smb).ToString());
                    itemBuilder.Clear();
                }

                if (smb != separator || isBracketOpen)
                    itemBuilder.Append(smb);

                smbIndex++;
            }

            return splittedData;
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public string GetValue(string key)
        {
            return _items.GetValueOrDefault(key);
        }

        public bool TryGetValue(string key, out string value)
        {
            return _items.TryGetValue(key, out value);
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
