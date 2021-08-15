using Links.Infrastructure.Serialization.Base;
using System.Collections.Generic;
using System.Windows.Media;

namespace Links.Infrastructure.Serialization
{
    internal class ColorSerializer : Serializer<Color>
    {
        public override Color Deserialize(string data)
        {
            var item = new SerializeDataParser().ParseData(data).SerializationItem;

            if (item == null)
                return new Color();

            byte a = byte.Parse(item.GetValue("A"));
            byte r = byte.Parse(item.GetValue("R"));
            byte g = byte.Parse(item.GetValue("G"));
            byte b = byte.Parse(item.GetValue("B"));

            float scA = float.Parse(item.GetValue("ScA"));
            float scR = float.Parse(item.GetValue("ScR"));
            float scG = float.Parse(item.GetValue("ScG"));
            float scB = float.Parse(item.GetValue("ScB"));

            return new Color()
            {
                A = a,
                R = r,
                G = g,
                B = b,
                ScA = scA,
                ScR = scR,
                ScG = scG,
                ScB = scB
            };
        }

        public override string Serialize(Color color)
        {
            var dict = new Dictionary<string, object>()
            {
                { "A", color.A },
                { "R", color.R },
                { "G", color.G },
                { "B", color.B },
                { "ScA", color.ScA },
                { "ScR", color.ScR },
                { "ScG", color.ScG },
                { "ScB", color.B },
            };

            string data = ConvertToDataString(dict);
            return GenerateFullDataString(data);
        }
    }
}
