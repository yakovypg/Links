using System.Windows.Media;

namespace Links.Infrastructure.Serialization
{
    internal class ColorSerializer : ISerializer<Color>
    {
        public Color Deserialize(string data)
        {
            var item = new SerializerItem(data);

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
                A = a, R = r, G = g, B = b,
                ScA = scA, ScR = scR, ScG = scG, ScB = scB
            };
        }

        public string Serialize(Color color)
        {
            return $"A={color.A} R={color.R} G={color.G} B={color.B} " +
                   $"ScA={color.ScA} ScR={color.ScR} ScG={color.ScG} ScB={color.ScB}";
        }
    }
}
