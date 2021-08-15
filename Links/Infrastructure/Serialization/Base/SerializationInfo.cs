using System.Collections.Generic;

namespace Links.Infrastructure.Serialization.Base
{
    internal class SerializationInfo
    {
        public string SerializerName { get; }

        public SerializationInfo(string serializerName)
        {
            SerializerName = serializerName;
        }

        public string GetSystemInfo()
        {
            var dict = new Dictionary<string, object>()
            {
                { "SerializerName", SerializerName }
            };

            return SerializerBase.GetDataString(dict) + SerializerBase.END_SYSTEM_INFO;
        }

        public override string ToString()
        {
            return SerializerName;
        }
    }
}
