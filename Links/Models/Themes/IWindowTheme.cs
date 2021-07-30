using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Links.Models.Themes
{
    internal interface IWindowTheme
    {
        SolidColorBrush WindowBackground { get; set; }
        SolidColorBrush WindowTopBarBackground { get; set; }
        SolidColorBrush WindowGridSplitterBackground { get; set; }

        SolidColorBrush TopBarBackground { get; set; }

        SolidColorBrush SettingsPageTextBlocksForeground { get; set; }
        SolidColorBrush SettingsPageBottomBarBackground { get; set; }

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

        SolidColorBrush IconButtonItemBackground { get; set; }
        SolidColorBrush IconButtonBackgroundMouseOver { get; set; }
        SolidColorBrush IconButtonBackgroundPressed { get; set; }

        SolidColorBrush GroupFieldBorderBrush { get; set; }
        SolidColorBrush GroupFieldSubborderBrush { get; set; }
        SolidColorBrush GroupFieldTextBlocksForeground { get; set; }

        SolidColorBrush GroupFieldIconButtonItemBackground { get; set; }
        SolidColorBrush GroupFieldIconButtonBackground { get; set; }
        SolidColorBrush GroupFieldIconButtonBackgroundMouseOver { get; set; }
        SolidColorBrush GroupFieldIconButtonBackgroundPressed { get; set; }

        SolidColorBrush LinkPresenterGridBackground { get; set; }
        SolidColorBrush LinkPresenterBottomBarBackground { get; set; }
        SolidColorBrush LinkPresenterTextBlocksForeground { get; set; }
        SolidColorBrush LinkPresenterInformationGridBackground { get; set; }
        SolidColorBrush LinkPresenterImageBorderBrush { get; set; }
        SolidColorBrush LinkPresenterImageBackground { get; set; }
    }
}
