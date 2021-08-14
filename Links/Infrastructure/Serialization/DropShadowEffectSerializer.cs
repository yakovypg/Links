using System.Windows.Media.Effects;

namespace Links.Infrastructure.Serialization
{
    internal class DropShadowEffectSerializer : ISerializer<DropShadowEffect>
    {
        public DropShadowEffect Deserialize(string data)
        {
            if (string.IsNullOrEmpty(data))
                return null;

            var item = new SerializerItem(data);

            double blurRadius = double.Parse(item.GetValue("BlurRadius"));
            double direction = double.Parse(item.GetValue("Direction"));
            double opacity = double.Parse(item.GetValue("Opacity"));
            double shadowDepth = double.Parse(item.GetValue("ShadowDepth"));

            string colorData = item.GetValue("Color").Substring(1);
            colorData = colorData.Remove(colorData.Length - 1);
            var color = new ColorSerializer().Deserialize(colorData);

            return new DropShadowEffect()
            {
                BlurRadius = blurRadius,
                Direction = direction,
                Opacity = opacity,
                ShadowDepth = shadowDepth,
                Color = color
            };
        }

        public string Serialize(DropShadowEffect item)
        {
            if (item == null)
                return null;
            
            string colorData = new ColorSerializer().Serialize(item.Color);

            return $"BlurRadius={item.BlurRadius} Direction={item.Direction} Opacity={item.Opacity} " +
                   $"ShadowDepth={item.ShadowDepth} Color=[{colorData}]";
        }
    }
}
