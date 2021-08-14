using Links.Models.Localization;
using System.Linq;

namespace Links.Infrastructure.Serialization
{
    internal class LocaleSerializer : ISerializer<Locale>
    {
        public Locale Deserialize(string data)
        {
            var item = new SerializerItem(data);

            string localeMessagesData = item.GetValue("LocaleMessages").Substring(1);
            localeMessagesData = localeMessagesData.Remove(localeMessagesData.Length - 1);

            var localeMessagesItem = new SerializerItem(localeMessagesData);
            var localeMessages = new LocaleMessages();

            foreach (var prop in localeMessages.GetType().GetProperties())
            {
                if (localeMessagesItem.TryGetValue(prop.Name, out string value))
                    prop.SetValue(localeMessages, value);
            }

            string displayName = item.GetValue("DisplayName");
            string cultureName = item.GetValue("CultureName");

            var locale = new Locale(displayName, cultureName)
            {
                LocaleMessages = localeMessages
            };

            foreach (var prop in locale.GetType().GetProperties().Where(t => t.Name != "DisplayName" && t.Name != "CultureName" && t.Name != "LocaleMessages"))
            {
                if (item.TryGetValue(prop.Name, out string value))
                    prop.SetValue(locale, value);
            }

            return locale;
        }

        public string Serialize(Locale locale)
        {
            return new DefaultSerializer().Serialize(locale, true);
        }
    }
}
