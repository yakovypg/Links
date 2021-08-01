using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Links.Models.Themes
{
    internal class WindowTheme : IWindowTheme
    {
        public static WindowTheme Dark => new WindowDarkTheme();
        public static WindowTheme Blue => new WindowBlueTheme();
        public static WindowTheme Light => new WindowLightTheme();

        public SolidColorBrush WindowBackground { get; set; }
        public SolidColorBrush WindowSystemTopBarBackground { get; set; }
        public SolidColorBrush WindowGridSplitterBackground { get; set; }

        public Effect TitleEffect { get; set; }
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
    }
}
