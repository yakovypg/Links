using Links.Data.Imaging;
using Links.Models.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Effects;

namespace Links.Infrastructure.Serialization
{
    internal class WindowThemeSerializer : ISerializer<WindowTheme>
    {
        public WindowTheme Deserialize(string data)
        {
            var item = new SerializerItem(data);

            string displayName = item.GetValue("DisplayName");

            string titleEffectData = item.GetValue("TitleEffect").Substring(1);
            titleEffectData = titleEffectData.Remove(titleEffectData.Length - 1);
            var titleEffect = new DropShadowEffectSerializer().Deserialize(titleEffectData);

            var theme = new WindowTheme(displayName) { TitleEffect = titleEffect };
            var themeType = theme.GetType();

            foreach (var prop in themeType.GetProperties().Where(t => t.Name != "DisplayName" && t.Name != "TitleEffect"))
            {
                if (item.TryGetValue(prop.Name, out string value))
                {
                    var brush = BrushTransformer.ToSolidColorBrush(value);
                    prop.SetValue(theme, brush);
                }
            }

            return theme;
        }

        public string Serialize(WindowTheme theme)
        {
            var converters = new Dictionary<Type, Func<object, string>>()
            {
                { typeof(Effect), SerializeEffect }
            };

            return new DefaultSerializer(converters).Serialize(theme);
        }

        private string SerializeEffect(object effect)
        {
            string data = new DropShadowEffectSerializer().Serialize(effect as DropShadowEffect);
            return $"[{data}]";
        }
    }
}
