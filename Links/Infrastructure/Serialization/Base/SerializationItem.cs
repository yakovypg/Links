using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Links.Infrastructure.Serialization.Base
{
    internal class SerializationItem : IEnumerable<KeyValuePair<string, string>>, ICloneable
    {
        private readonly List<SerializationItem> _children;
        public IReadOnlyList<SerializationItem> Children => _children;

        private readonly Dictionary<string, string> _valuesDictionary;
        public IReadOnlyDictionary<string, string> ValuesDictionary => _valuesDictionary;

        public SerializationItem(List<SerializationItem> children, Dictionary<string, string> valuesDictionary)
        {
            _children = children ?? new List<SerializationItem>();
            _valuesDictionary = valuesDictionary ?? new Dictionary<string, string>();
        }

        public SerializationItem(string data)
        {
            var serializerItem = new SerializeDataParser().ParseItem(data);

            _children = new List<SerializationItem>(serializerItem.Children);
            _valuesDictionary = new Dictionary<string, string>(serializerItem.ValuesDictionary);
        }

        public bool TryGetValue(string key, out string value)
        {
            return _valuesDictionary.TryGetValue(key, out value);
        }

        public string GetValueOrDefault(string key)
        {
            return ValuesDictionary.GetValueOrDefault(key);
        }

        public string GetValue(string key)
        {
            bool res = TryGetValue(key, out string value);
            return res ? value : throw new KeyNotFoundException();
        }

        public string GetValuesDataString()
        {
            return SerializerBase.GetDataString(_valuesDictionary.Values, _valuesDictionary.Keys);
        }

        public string GetChildrenDataString()
        {
            var items = _children.Select(t => t?.GetDataString() ?? SerializerBase.NullValueStr);
            return _children.Count == 0 ? null : SerializerBase.GetDataString(items);
        }

        public string GetDataString()
        {
            string valuesData = GetValuesDataString();
            string childrenData = GetChildrenDataString();

            bool isValuesDataEmpty = string.IsNullOrEmpty(valuesData);
            bool isChildrenDataEmpty = string.IsNullOrEmpty(childrenData);

            if (isValuesDataEmpty && isChildrenDataEmpty)
                return null;

            if (isValuesDataEmpty)
                return childrenData;

            if (isChildrenDataEmpty)
                return valuesData;

            return GetValuesDataString() + SerializerBase.DELIMITER + GetChildrenDataString();
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return _valuesDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public object Clone()
        {
            var children = new List<SerializationItem>(Children.Count);
            var valuesDictionary = new Dictionary<string, string>(ValuesDictionary);

            foreach (var child in Children)
            {
                children.Add(child.Clone() as SerializationItem);
            }

            return new SerializationItem(children, valuesDictionary);
        }
    }
}
