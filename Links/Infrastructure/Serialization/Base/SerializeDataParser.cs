using Links.Infrastructure.Extensions;
using System.Collections.Generic;
using System.Text;

namespace Links.Infrastructure.Serialization.Base
{
    internal class SerializeDataParser
    {
        public SerializeDataParser()
        {
        }

        public (SerializationInfo SerializationInfo, SerializationItem SerializationItem) ParseData(string data)
        {
            int endInfoIndex = data.IndexOf(SerializerBase.END_SYSTEM_INFO);
            int delimitrIndex = data.IndexOf(SerializerBase.DELIMITER);

            if (endInfoIndex == -1 || (delimitrIndex != -1 && delimitrIndex < endInfoIndex))
                return (null, ParseItem(data));

            data = ExtractInfo(data, out SerializationInfo info);
            var item = ParseItem(data);

            return (info, item);
        }

        public string ExtractInfo(string data, out SerializationInfo info)
        {
            info = ExtractInfo(data);
            int endInfoIndex = data.IndexOf(SerializerBase.END_SYSTEM_INFO);

            return data.Substring(endInfoIndex + 1);
        }

        public SerializationInfo ExtractInfo(string data)
        {
            int endInfoIndex = data.IndexOf(SerializerBase.END_SYSTEM_INFO);
            data = data.Remove(endInfoIndex);

            SerializationItem item = ParseItem(data);
            string serializerName = item.GetValueOrDefault("SerializerName");

            return new SerializationInfo(serializerName);
        }

        public SerializationItem ParseItem(string data)
        {
            if (data == SerializerBase.NullValueStr)
                return null;

            string[] splittedData = SplitData(data);

            var children = new List<SerializationItem>();
            var valuesDictionary = new Dictionary<string, string>();

            foreach (var dataItem in splittedData)
            {
                if (string.IsNullOrEmpty(dataItem))
                    continue;

                if (dataItem[0] == SerializerBase.START_COMPLEX_TYPE)
                {
                    string complexTypeData = dataItem.Extract(1, 1);
                    SerializationItem item = ParseItem(complexTypeData);

                    children.Add(item);
                }
                else
                {
                    int index = dataItem.IndexOf(SerializerBase.ASSIGN);
                    string key = dataItem.Remove(index);
                    string value = dataItem.Substring(index + 1);

                    if (value == SerializerBase.NullValueStr)
                        value = null;

                    if (!string.IsNullOrEmpty(value) && value[0] == SerializerBase.START_IGNORE_SPACE)
                        value = value.Extract(1, 1);

                    valuesDictionary.Add(key, value);
                }
            }

            return new SerializationItem(children, valuesDictionary);
        }

        public string[] SplitData(string data)
        {
            var splittedData = new List<string>();
            var itemBuilder = new StringBuilder();

            int smbIndex = 0;
            int startSymbols = 0;
            bool ignoreSpace = false;

            foreach (char smb in data)
            {
                if (smb == SerializerBase.START_COMPLEX_TYPE)
                {
                    startSymbols++;
                }
                else if (smb == SerializerBase.END_COMPLEX_TYPE)
                {
                    startSymbols--;
                }

                if (smb == SerializerBase.START_IGNORE_SPACE)
                {
                    ignoreSpace = true;
                }
                else if (smb == SerializerBase.END_IGNORE_SPACE)
                {
                    ignoreSpace = false;
                }

                if (smb == SerializerBase.DELIMITER && startSymbols == 0 && !ignoreSpace)
                {
                    splittedData.Add(itemBuilder.ToString());
                    itemBuilder.Clear();
                }
                else if (smbIndex == data.Length - 1)
                {
                    if (smb != SerializerBase.END_IGNORE_SPACE && smb != ' ')
                        itemBuilder.Append(smb);

                    splittedData.Add(itemBuilder.ToString());
                    itemBuilder.Clear();
                }

                if (startSymbols > 0)
                {
                    itemBuilder.Append(smb);
                }
                else if (smb != SerializerBase.DELIMITER || ignoreSpace)
                {
                    if (smb != SerializerBase.START_IGNORE_SPACE && smb != SerializerBase.END_IGNORE_SPACE)
                        itemBuilder.Append(smb);
                }

                smbIndex++;
            }

            return splittedData.ToArray();
        }
    }
}
