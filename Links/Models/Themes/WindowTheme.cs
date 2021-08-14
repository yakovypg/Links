using Links.Infrastructure.Serialization.Attributes;
using System;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Links.Models.Themes
{
    internal class WindowTheme : IWindowTheme
    {
        public string DisplayName { get; }

        [SerializerIgnore]
        public static WindowTheme Dark => new WindowDarkTheme();
        [SerializerIgnore]
        public static WindowTheme Blue => new WindowBlueTheme();
        [SerializerIgnore]
        public static WindowTheme Light => new WindowLightTheme();

        public Brush WindowBackground { get; set; }
        public Brush WindowSystemTopBarBackground { get; set; }
        public Brush WindowGridSplitterBackground { get; set; }

        [SerializerCustomConvert]
        public DropShadowEffect TitleEffect { get; set; }
        public Brush TitleForeground { get; set; }
        public Brush TitleForegroundMouseOver { get; set; }

        public Brush SystemButtonItemBackground { get; set; }

        public Brush MinimizeWindowButtonBackground { get; set; }
        public Brush MinimizeWindowButtonBackgroundMouseOver { get; set; }
        public Brush MinimizeWindowButtonBackgroundPressed { get; set; }

        public Brush MaximizeWindowButtonBackground { get; set; }
        public Brush MaximizeWindowButtonBackgroundMouseOver { get; set; }
        public Brush MaximizeWindowButtonBackgroundPressed { get; set; }

        public Brush CloseWindowButtonBackground { get; set; }
        public Brush CloseWindowButtonBackgroundMouseOver { get; set; }
        public Brush CloseWindowButtonBackgroundPressed { get; set; }

        public Brush IconButtonItemBackground { get; set; }
        public Brush IconButtonBackgroundMouseOver { get; set; }
        public Brush IconButtonBackgroundPressed { get; set; }

        public Brush SettingsFieldTextBlocksForeground { get; set; }
        public Brush SettingsFieldBottomBarBackground { get; set; }

        public Brush GroupsFieldBorderBrush { get; set; }
        public Brush GroupsFieldSubborderBrush { get; set; }
        public Brush GroupsFieldTextBlocksForeground { get; set; }

        public Brush GroupsFieldIconButtonItemBackground { get; set; }
        public Brush GroupsFieldIconButtonBackground { get; set; }
        public Brush GroupsFieldIconButtonBackgroundMouseOver { get; set; }
        public Brush GroupsFieldIconButtonBackgroundPressed { get; set; }

        public Brush GroupEditorBackground { get; set; }

        public Brush CreatorMenuBackground { get; set; }
        public Brush CreatorMenuBorderBrush { get; set; }
        public Brush CreatorMenuSubborderBrush { get; set; }

        public Brush LinksFieldTopBarBackground { get; set; }

        public Brush LinkPresenterGridBackground { get; set; }
        public Brush LinkPresenterBottomBarBackground { get; set; }
        public Brush LinkPresenterTextBlocksForeground { get; set; }
        public Brush LinkPresenterInformationGridBackground { get; set; }
        public Brush LinkPresenterImageBorderBrush { get; set; }
        public Brush LinkPresenterImageBackground { get; set; }

        public WindowTheme(string displayName)
        {
            DisplayName = displayName;
        }

        public override string ToString()
        {
            return DisplayName;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is WindowTheme other))
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
            hash.Add(WindowBackground);
            hash.Add(WindowSystemTopBarBackground);
            hash.Add(WindowGridSplitterBackground);
            hash.Add(TitleEffect);
            hash.Add(TitleForeground);
            hash.Add(TitleForegroundMouseOver);
            hash.Add(SystemButtonItemBackground);
            hash.Add(MinimizeWindowButtonBackground);
            hash.Add(MinimizeWindowButtonBackgroundMouseOver);
            hash.Add(MinimizeWindowButtonBackgroundPressed);
            hash.Add(MaximizeWindowButtonBackground);
            hash.Add(MaximizeWindowButtonBackgroundMouseOver);
            hash.Add(MaximizeWindowButtonBackgroundPressed);
            hash.Add(CloseWindowButtonBackground);
            hash.Add(CloseWindowButtonBackgroundMouseOver);
            hash.Add(CloseWindowButtonBackgroundPressed);
            hash.Add(IconButtonItemBackground);
            hash.Add(IconButtonBackgroundMouseOver);
            hash.Add(IconButtonBackgroundPressed);
            hash.Add(SettingsFieldTextBlocksForeground);
            hash.Add(SettingsFieldBottomBarBackground);
            hash.Add(GroupsFieldBorderBrush);
            hash.Add(GroupsFieldSubborderBrush);
            hash.Add(GroupsFieldTextBlocksForeground);
            hash.Add(GroupsFieldIconButtonItemBackground);
            hash.Add(GroupsFieldIconButtonBackground);
            hash.Add(GroupsFieldIconButtonBackgroundMouseOver);
            hash.Add(GroupsFieldIconButtonBackgroundPressed);
            hash.Add(GroupEditorBackground);
            hash.Add(CreatorMenuBackground);
            hash.Add(CreatorMenuBorderBrush);
            hash.Add(CreatorMenuSubborderBrush);
            hash.Add(LinksFieldTopBarBackground);
            hash.Add(LinkPresenterGridBackground);
            hash.Add(LinkPresenterBottomBarBackground);
            hash.Add(LinkPresenterTextBlocksForeground);
            hash.Add(LinkPresenterInformationGridBackground);
            hash.Add(LinkPresenterImageBorderBrush);
            hash.Add(LinkPresenterImageBackground);

            return hash.ToHashCode();
        }
    }
}
