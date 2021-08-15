using Links.Infrastructure.Serialization.Attributes;
using System;
using System.Linq;
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

        public SolidColorBrush WindowBackground { get; set; }
        public SolidColorBrush WindowSystemTopBarBackground { get; set; }
        public SolidColorBrush WindowGridSplitterBackground { get; set; }

        [SerializerCustomConvert]
        public DropShadowEffect TitleEffect { get; set; }
        public SolidColorBrush TitleForeground { get; set; }
        public SolidColorBrush TitleForegroundMouseOver { get; set; }

        public SolidColorBrush SystemButtonItemBackground { get; set; }

        public SolidColorBrush MinimizeWindowButtonBackground { get; set; }
        public SolidColorBrush MinimizeWindowButtonBackgroundMouseOver { get; set; }
        public SolidColorBrush MinimizeWindowButtonBackgroundPressed { get; set; }

        public SolidColorBrush MaximizeWindowButtonBackground { get; set; }
        public SolidColorBrush MaximizeWindowButtonBackgroundMouseOver { get; set; }
        public SolidColorBrush MaximizeWindowButtonBackgroundPressed { get; set; }

        public SolidColorBrush CloseWindowButtonBackground { get; set; }
        public SolidColorBrush CloseWindowButtonBackgroundMouseOver { get; set; }
        public SolidColorBrush CloseWindowButtonBackgroundPressed { get; set; }

        public SolidColorBrush IconButtonItemBackground { get; set; }
        public SolidColorBrush IconButtonBackgroundMouseOver { get; set; }
        public SolidColorBrush IconButtonBackgroundPressed { get; set; }

        public SolidColorBrush SettingsFieldTextBlocksForeground { get; set; }
        public SolidColorBrush SettingsFieldBottomBarBackground { get; set; }

        public SolidColorBrush GroupsFieldBorderBrush { get; set; }
        public SolidColorBrush GroupsFieldSubborderBrush { get; set; }
        public SolidColorBrush GroupsFieldTextBlocksForeground { get; set; }

        public SolidColorBrush GroupsFieldIconButtonItemBackground { get; set; }
        public SolidColorBrush GroupsFieldIconButtonBackground { get; set; }
        public SolidColorBrush GroupsFieldIconButtonBackgroundMouseOver { get; set; }
        public SolidColorBrush GroupsFieldIconButtonBackgroundPressed { get; set; }

        public SolidColorBrush GroupEditorBackground { get; set; }

        public SolidColorBrush CreatorMenuBackground { get; set; }
        public SolidColorBrush CreatorMenuBorderBrush { get; set; }
        public SolidColorBrush CreatorMenuSubborderBrush { get; set; }

        public SolidColorBrush LinksFieldTopBarBackground { get; set; }

        public SolidColorBrush LinkPresenterGridBackground { get; set; }
        public SolidColorBrush LinkPresenterBottomBarBackground { get; set; }
        public SolidColorBrush LinkPresenterTextBlocksForeground { get; set; }
        public SolidColorBrush LinkPresenterInformationGridBackground { get; set; }
        public SolidColorBrush LinkPresenterImageBorderBrush { get; set; }
        public SolidColorBrush LinkPresenterImageBackground { get; set; }

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

            PropertyInfo[] currProps = GetType().GetProperties().Where(t => t.CanWrite).ToArray();
            PropertyInfo[] otherProps = other.GetType().GetProperties().Where(t => t.CanWrite).ToArray();

            if (currProps.Length != otherProps.Length)
                return false;

            for (int i = 0; i < currProps.Length; ++i)
            {
                object currPropValue = currProps[i].GetValue(this);
                object otherPropValue = otherProps[i].GetValue(other);

                if (currPropValue is DropShadowEffect)
                {
                    var currEffect = currPropValue as DropShadowEffect;
                    var otherEffect = otherPropValue as DropShadowEffect;

                    if (currEffect.BlurRadius != otherEffect.BlurRadius ||
                        currEffect.Direction != otherEffect.Direction ||
                        currEffect.Opacity != otherEffect.Opacity ||
                        currEffect.ShadowDepth != otherEffect.ShadowDepth ||
                        currEffect.Color != otherEffect.Color)
                    {
                        return false;
                    }
                }
                else if (currPropValue is SolidColorBrush)
                {
                    var currSolidColorBrush = currPropValue as SolidColorBrush;
                    var otherSolidColorBrush = otherPropValue as SolidColorBrush;

                    if (!currSolidColorBrush.Color.Equals(otherSolidColorBrush?.Color))
                        return false;
                }
                else
                {
                    if (!currPropValue.Equals(otherPropValue))
                        return false;
                }
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
