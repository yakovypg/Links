using System;
using System.Reflection;

namespace Links.Models.Localization
{
    internal class Locale : ILocale
    {
        public static Locale English => new English();

        public string DisplayName { get; }
        public string CultureName { get; }

        public string GroupsSorting { get; set; }
        public string LinksSorting { get; set; }
        public string SortingMode { get; set; }

        public string Warnings { get; set; }
        public string PresenterSize { get; set; }
        public string Theme { get; set; }
        public string Language { get; set; }

        public string RecycleBin { get; set; }
        public string EmptyRecycleBin { get; set; }

        public string Days { get; set; }
        public string Every { get; set; }
        public string Never { get; set; }

        public string Unsorted { get; set; }

        public string Close { get; set; }
        public string Reset { get; set; }
        public string Import { get; set; }
        public string Export { get; set; }
        public string Restore { get; set; }
        public string Remove { get; set; }
        public string Empty { get; set; }

        public string Name { get; set; }
        public string Color { get; set; }

        public string Date { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Group { get; set; }
        public string Image { get; set; }

        public string Ok { get; set; }
        public string On { get; set; }
        public string Off { get; set; }

        public string Add { get; set; }
        public string Find { get; set; }
        public string CheckAll { get; set; }

        public string Ascending { get; set; }
        public string Descending { get; set; }

        public string Error { get; set; }
        public string Warning { get; set; }
        public string Question { get; set; }
        public string Information { get; set; }
        public string Comment { get; set; }

        public LocaleMessages LocaleMessages { get; set; }

        public Locale(string displayName, string cultureName)
        {
            DisplayName = displayName;
            CultureName = cultureName;
        }

        public override string ToString()
        {
            return DisplayName;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Locale other))
                return false;

            if (DisplayName != other.DisplayName)
                return false;

            PropertyInfo[] currProps = GetType().GetProperties();
            PropertyInfo[] otherProps = other.GetType().GetProperties();

            if (currProps.Length != otherProps.Length)
                return false;

            for (int i = 0; i < currProps.Length; ++i)
            {
                if (!currProps[i].Equals(otherProps[i]))
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(DisplayName);
            hash.Add(CultureName);
            hash.Add(GroupsSorting);
            hash.Add(LinksSorting);
            hash.Add(SortingMode);
            hash.Add(Warnings);
            hash.Add(PresenterSize);
            hash.Add(Theme);
            hash.Add(Language);
            hash.Add(RecycleBin);
            hash.Add(EmptyRecycleBin);
            hash.Add(Days);
            hash.Add(Every);
            hash.Add(Never);
            hash.Add(Unsorted);
            hash.Add(Close);
            hash.Add(Reset);
            hash.Add(Import);
            hash.Add(Export);
            hash.Add(Restore);
            hash.Add(Remove);
            hash.Add(Empty);
            hash.Add(Name);
            hash.Add(Color);
            hash.Add(Date);
            hash.Add(Title);
            hash.Add(Link);
            hash.Add(Group);
            hash.Add(Image);
            hash.Add(Ok);
            hash.Add(On);
            hash.Add(Off);
            hash.Add(Add);
            hash.Add(Find);
            hash.Add(CheckAll);
            hash.Add(Ascending);
            hash.Add(Descending);
            hash.Add(Error);
            hash.Add(Warning);
            hash.Add(Question);
            hash.Add(Information);
            hash.Add(Comment);
            hash.Add(LocaleMessages);

            return hash.ToHashCode();
        }
    }
}
