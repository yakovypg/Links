using System;

namespace Links.Models.Localization
{
    internal class Locale : ILocale
    {
        public static Locale English => new English();

        public Locale()
        {

        }
    }
}
