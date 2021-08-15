using Links.Data.Imaging;
using Links.Infrastructure.Extensions;
using Links.Infrastructure.Serialization.Base;
using Links.Models.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Effects;

namespace Links.Infrastructure.Serialization
{
    internal class WindowThemeSerializer : Serializer<WindowTheme>
    {
        public override WindowTheme Deserialize(string data)
        {
            var item = new SerializeDataParser().ParseData(data).SerializationItem;

            if (item == null)
                return null;

            string displayName = item.GetValue("DisplayName");

            string titleEffectData = item.GetValue("TitleEffect").Extract(1, 1);
            var titleEffect = new DropShadowEffectSerializer().Deserialize(titleEffectData);

            var theme = new WindowTheme(displayName)
            {
                TitleEffect = titleEffect
            };

            var themeType = theme.GetType();
            var props = themeType.GetProperties();
            var necessaryProps = props.Where(t => t.Name != "DisplayName" && t.Name != "TitleEffect");

            foreach (var prop in necessaryProps)
            {
                if (item.TryGetValue(prop.Name, out string value))
                {
                    var brush = BrushTransformer.ToSolidColorBrush(value);
                    prop.SetValue(theme, brush);
                }
            }

            return theme;
        }

        public override string Serialize(WindowTheme theme)
        {
            var converters = new Dictionary<Type, Func<object, string>>()
            {
                { typeof(Effect), SerializeEffect }
            };

            var serializer = new DefaultSerializer(converters)
            {
                SerializerName = GetInfo().SerializerName
            };

            return serializer.Serialize(theme);
        }

        private string SerializeEffect(object effect)
        {
            string data = new DropShadowEffectSerializer().Serialize(effect as DropShadowEffect);
            return data.Surround(START_COMPLEX_TYPE, END_COMPLEX_TYPE);
        }
    }
}
