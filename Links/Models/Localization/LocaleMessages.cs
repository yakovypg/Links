using System;
using System.Reflection;

namespace Links.Models.Localization
{
    internal class LocaleMessages : ILocaleMessages
    {
        public string SuccessfulLinksImportingInfo { get; set; }
        public string SuccessfulLinksExportingInfo { get; set; }
        public string NoSelectedLinksInfo { get; set; }

        public string LoadProgramStateQuestion { get; set; }
        public string SaveProgramStateQuestion { get; set; }
        public string AutoLinksDistributionQuestion { get; set; }
        public string DeleteGroupQuestion { get; set; }
        public string DeleteLinkQuestion { get; set; }

        public string DeleteGroupError { get; set; }
        public string FollowLinkError { get; set; }
        public string DeleteLinkError { get; set; }
        public string RestoreLinkError { get; set; }
        public string LinksImportingError { get; set; }
        public string LinksExportingError { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is LocaleMessages other))
                return false;

            PropertyInfo[] currProps = GetType().GetProperties();
            PropertyInfo[] otherProps = other.GetType().GetProperties();

            if (currProps.Length != otherProps.Length)
                return false;

            for (int i = 0; i < currProps.Length; ++i)
            {
                object currPropValue = currProps[i].GetValue(this);
                object otherPropValue = otherProps[i].GetValue(other);

                if (!currPropValue.Equals(otherPropValue))
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(SuccessfulLinksImportingInfo);
            hash.Add(SuccessfulLinksExportingInfo);
            hash.Add(NoSelectedLinksInfo);
            hash.Add(LoadProgramStateQuestion);
            hash.Add(SaveProgramStateQuestion);
            hash.Add(AutoLinksDistributionQuestion);
            hash.Add(DeleteGroupQuestion);
            hash.Add(DeleteLinkQuestion);
            hash.Add(DeleteGroupError);
            hash.Add(FollowLinkError);
            hash.Add(DeleteLinkError);
            hash.Add(RestoreLinkError);
            hash.Add(LinksImportingError);
            hash.Add(LinksExportingError);

            return hash.ToHashCode();
        }
    }
}
