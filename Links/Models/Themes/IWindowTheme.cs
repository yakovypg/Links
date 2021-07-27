using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Links.Models.Themes
{
    internal interface IWindowTheme
    {
        SolidColorBrush WindowBackground { get; set; }
        SolidColorBrush TopBarBackground { get; set; }

        Effect TitleEffect { get; set; }
        SolidColorBrush TitleForeground { get; set; }
        SolidColorBrush TitleForegroundMouseOver { get; set; }

        SolidColorBrush SystemButtonItemBackground { get; set; }

        SolidColorBrush MinimizeWindowButtonBackground { get; set; }
        SolidColorBrush MinimizeWindowButtonBackgroundMouseOver { get; set; }
        SolidColorBrush MinimizeWindowButtonBackgroundPressed { get; set; }

        SolidColorBrush MaximizeWindowButtonBackground { get; set; }
        SolidColorBrush MaximizeWindowButtonBackgroundMouseOver { get; set; }
        SolidColorBrush MaximizeWindowButtonBackgroundPressed { get; set; }

        SolidColorBrush CloseWindowButtonBackground { get; set; }
        SolidColorBrush CloseWindowButtonBackgroundMouseOver { get; set; }
        SolidColorBrush CloseWindowButtonBackgroundPressed { get; set; }
    }
}
