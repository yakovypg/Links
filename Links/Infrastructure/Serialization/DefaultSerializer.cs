using Links.Infrastructure.Extensions;
using Links.Infrastructure.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Links.Infrastructure.Serialization
{
    internal class DefaultSerializer
    {
        public Dictionary<Type, Func<object, string>> ComplexTypesConverters { get; }

        public DefaultSerializer(Dictionary<Type, Func<object, string>> complexTypesConverters = null)
        {
            ComplexTypesConverters = complexTypesConverters ?? new Dictionary<Type, Func<object, string>>();
        }

        public string Serialize(object obj, bool alwaysAddBrackets = false)
        {
            if (obj == null)
                return null;

            var objType = obj.GetType();
            var fields = objType.GetFields();
            var props = objType.GetProperties();

            var dataBuilder = new StringBuilder();
            var ignoreAttribute = new SerializerIgnoreAttribute();
            var complexTypeAttribute = new SerializerComplexTypeAttribute();
            var customConvertAttribute = new SerializerCustomConvertAttribute();
            var comparer = new SerializerAttributeEqualityComparer();

            if (fields != null)
            {
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
                        string serializedValue = Serialize(value, alwaysAddBrackets);
                        data = $"[{serializedValue}]";
                    }
                    else if (attributes.Contains(customConvertAttribute, comparer))
                    {
                        var fieldType = field.GetType();
                        var fieldValue = field.GetValue(obj);
                        var fieldValueType = fieldValue.GetType();
                        var func = ComplexTypesConverters.GetValueOrDefault(t => fieldValueType.IsSubclassOf(t));

                        data = func == null ? fieldValue?.ToString() : func(fieldValue);
                    }
                    else
                    {
                        data = field.GetValue(obj)?.ToString();
                    }

                    if (!string.IsNullOrEmpty(data) && alwaysAddBrackets && data[0] != '[')
                        data = $"[{data}]";

                    dataBuilder.Append($"{field.Name}={data} ");
                }
            }

            if (props != null)
            {
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
                        string serializedValue = Serialize(value, alwaysAddBrackets);
                        data = $"[{serializedValue}]";
                    }
                    else if (attributes.Contains(customConvertAttribute, comparer))
                    {
                        var propType = prop.GetType();
                        var propValue = prop.GetValue(obj);
                        var propValueType = propValue.GetType();
                        var func = ComplexTypesConverters.GetValueOrDefault(t => propValueType.IsSubclassOf(t));

                        data = func == null ? propValue?.ToString() : func(propValue);
                    }
                    else
                    {
                        data = prop.GetValue(obj)?.ToString();
                    }

                    if (!string.IsNullOrEmpty(data) && alwaysAddBrackets && data[0] != '[')
                        data = $"[{data}]";

                    dataBuilder.Append($"{prop.Name}={data} ");
                }
            }

            string output = dataBuilder.ToString();

            return !string.IsNullOrEmpty(output)
                ? output.Remove(output.Length - 1)
                : output;
        }
    }
}
