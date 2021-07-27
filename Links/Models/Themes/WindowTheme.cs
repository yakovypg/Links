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
        public SolidColorBrush TopBarBackground { get; set; }

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
    }
}
