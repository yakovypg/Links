using Links.Infrastructure.Extensions;
using Links.Infrastructure.Serialization.Base;
using Links.Models.Localization;
using System.Linq;

namespace Links.Infrastructure.Serialization
{
    internal class LocaleSerializer : Serializer<Locale>
    {
        public override Locale Deserialize(string data)
        {
            var item = new SerializeDataParser().ParseData(data).SerializationItem;

            string localeMessagesData = item.GetValue("LocaleMessages").Extract(1, 1);
            var msgItem = new SerializeDataParser().ParseData(localeMessagesData).SerializationItem;

            var localeMessages = new LocaleMessages();

            foreach (var prop in localeMessages.GetType().GetProperties())
            {
                if (msgItem.TryGetValue(prop.Name, out string value))
                    prop.SetValue(localeMessages, value);
            }

            string displayName = item.GetValue("DisplayName");
            string cultureName = item.GetValue("CultureName");

            var locale = new Locale(displayName, cultureName)
            {
                LocaleMessages = localeMessages
            };

            var localeType = locale.GetType();
            var props = localeType.GetProperties();
            var necessaryProps = props.Where(t => t.Name != "DisplayName" && t.Name != "CultureName" && t.Name != "LocaleMessages");

            foreach (var prop in necessaryProps)
            {
                if (item.TryGetValue(prop.Name, out string value))
                    prop.SetValue(locale, value);
            }

            return locale;
        }

        public override string Serialize(Locale locale)
        {
            var serializer = new DefaultSerializer()
            {
                SerializerName = GetInfo().SerializerName
            };

            return serializer.Serialize(locale, true);
        }
    }
}
