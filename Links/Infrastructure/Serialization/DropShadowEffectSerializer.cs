using Links.Infrastructure.Extensions;
using Links.Infrastructure.Serialization.Base;
using System.Collections.Generic;
using System.Windows.Media.Effects;

namespace Links.Infrastructure.Serialization
{
    internal class DropShadowEffectSerializer : Serializer<DropShadowEffect>
    {
        public override DropShadowEffect Deserialize(string data)
        {
            var item = new SerializeDataParser().ParseData(data).SerializationItem;

            if (item == null)
                return null;

            double blurRadius = double.Parse(item.GetValue("BlurRadius"));
            double direction = double.Parse(item.GetValue("Direction"));
            double opacity = double.Parse(item.GetValue("Opacity"));
            double shadowDepth = double.Parse(item.GetValue("ShadowDepth"));

            string colorData = item.GetValue("Color").Extract(1, 1);
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

        public override string Serialize(DropShadowEffect item)
        {
            if (item == null)
                return GenerateNullValueDataString();

            string colorData = new ColorSerializer().Serialize(item.Color);

            var dict = new Dictionary<string, object>()
            {
                { "BlurRadius", item.BlurRadius },
                { "Direction", item.Direction },
                { "Opacity", item.Opacity },
                { "ShadowDepth", item.ShadowDepth },
                { "Color", colorData.Surround(START_COMPLEX_TYPE, END_COMPLEX_TYPE) },
            };

            string data = ConvertToDataString(dict);
            return GenerateFullDataString(data);
        }
    }
}
