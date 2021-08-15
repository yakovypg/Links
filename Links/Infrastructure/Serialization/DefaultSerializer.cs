using Links.Infrastructure.Extensions;
using Links.Infrastructure.Serialization.Attributes;
using Links.Infrastructure.Serialization.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Links.Infrastructure.Serialization
{
    internal class DefaultSerializer : SerializerBase
    {
        public Dictionary<Type, Func<object, string>> CustomConverters { get; }

        private string _serializerName;
        public string SerializerName
        {
            get => _serializerName;
            set => _serializerName = string.IsNullOrEmpty(value) ? GetType().Name : value;
        }

        public DefaultSerializer(Dictionary<Type, Func<object, string>> customConverters = null)
        {
            SerializerName = GetType().Name;
            CustomConverters = customConverters ?? new Dictionary<Type, Func<object, string>>();
        }

        protected override SerializationInfo GetInfo()
        {
            return new SerializationInfo(SerializerName);
        }

        public string Serialize(object obj, bool alwaysAddIgnoreSpaceParam = false)
        {
            if (obj == null)
                return GenerateNullValueDataString();

            Type objType = obj.GetType();
            FieldInfo[] fields = objType.GetFields();
            PropertyInfo[] props = objType.GetProperties();

            var serializeDataBuilder = new StringBuilder();
            var comparer = new SerializerAttributeEqualityComparer();

            var ignoreAttribute = new SerializerIgnoreAttribute();
            var complexTypeAttribute = new SerializerComplexTypeAttribute();
            var customConvertAttribute = new SerializerCustomConvertAttribute();

            foreach (var field in fields)
            {
                var attributesObjs = field.GetCustomAttributes(true);
                var attributes = attributesObjs.ToAttributeEnumerable();

                string data;

                if (attributes.Contains(ignoreAttribute, comparer))
                {
                    continue;
                }
                else if (attributes.Contains(complexTypeAttribute, comparer))
                {
                    var value = field.GetValue(obj);
                    string serializedValue = Serialize(value, alwaysAddIgnoreSpaceParam);
                    data = serializedValue.Surround(START_COMPLEX_TYPE, END_COMPLEX_TYPE);
                }
                else if (attributes.Contains(customConvertAttribute, comparer))
                {
                    var fieldType = field.GetType();
                    var fieldValue = field.GetValue(obj);
                    var fieldValueType = fieldValue.GetType();
                    var func = CustomConverters.GetValueOrDefault(t => fieldValueType.IsSubclassOf(t));

                    data = func == null ? fieldValue?.ToString() : func(fieldValue);
                }
                else
                {
                    data = field.GetValue(obj)?.ToString();
                }

                if (alwaysAddIgnoreSpaceParam &&
                    !string.IsNullOrEmpty(data) &&
                    data[0] != START_COMPLEX_TYPE)
                {
                    data = data.Surround(START_IGNORE_SPACE, END_IGNORE_SPACE);
                }

                string currData = field.Name + ASSIGN + data + DELIMITER;
                serializeDataBuilder.Append(currData);
            }

            foreach (var prop in props)
            {
                var attributesObjs = prop.GetCustomAttributes(true);
                var attributes = attributesObjs.ToAttributeEnumerable();

                string data;

                if (attributes.Contains(ignoreAttribute, comparer))
                {
                    continue;
                }
                else if (attributes.Contains(complexTypeAttribute, comparer))
                {
                    var value = prop.GetValue(obj);
                    string serializedValue = Serialize(value, alwaysAddIgnoreSpaceParam);
                    data = serializedValue.Surround(START_COMPLEX_TYPE, END_COMPLEX_TYPE);
                }
                else if (attributes.Contains(customConvertAttribute, comparer))
                {
                    var propType = prop.GetType();
                    var propValue = prop.GetValue(obj);
                    var propValueType = propValue.GetType();
                    var func = CustomConverters.GetValueOrDefault(t => propValueType.IsSubclassOf(t));

                    data = func == null ? propValue?.ToString() : func(propValue);
                }
                else
                {
                    data = prop.GetValue(obj)?.ToString();
                }

                if (alwaysAddIgnoreSpaceParam &&
                    !string.IsNullOrEmpty(data) &&
                    data[0] != START_COMPLEX_TYPE)
                {
                    data = data.Surround(START_IGNORE_SPACE, END_IGNORE_SPACE);
                }

                string currData = prop.Name + ASSIGN + data + DELIMITER;
                serializeDataBuilder.Append(currData);
            }

            string output = serializeDataBuilder.ToString();

            return output == null
                ? GenerateNullValueDataString()
                : GenerateFullDataString(output.Remove(output.Length - 1));
        }
    }
}
