using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Links.Models.Themes
{
    internal interface IWindowTheme
    {
        string DisplayName { get; }

        SolidColorBrush WindowBackground { get; set; }
        SolidColorBrush WindowSystemTopBarBackground { get; set; }
        SolidColorBrush WindowGridSplitterBackground { get; set; }

        DropShadowEffect TitleEffect { get; set; }
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

        SolidColorBrush SettingsFieldTextBlocksForeground { get; set; }
        SolidColorBrush SettingsFieldBottomBarBackground { get; set; }

        SolidColorBrush GroupsFieldBorderBrush { get; set; }
        SolidColorBrush GroupsFieldSubborderBrush { get; set; }
        SolidColorBrush GroupsFieldTextBlocksForeground { get; set; }

        SolidColorBrush GroupsFieldIconButtonItemBackground { get; set; }
        SolidColorBrush GroupsFieldIconButtonBackground { get; set; }
        SolidColorBrush GroupsFieldIconButtonBackgroundMouseOver { get; set; }
        SolidColorBrush GroupsFieldIconButtonBackgroundPressed { get; set; }

        SolidColorBrush GroupEditorBackground { get; set; }

        SolidColorBrush CreatorMenuBackground { get; set; }
        SolidColorBrush CreatorMenuBorderBrush { get; set; }
        SolidColorBrush CreatorMenuSubborderBrush { get; set; }

        SolidColorBrush LinksFieldTopBarBackground { get; set; }

        SolidColorBrush LinkPresenterGridBackground { get; set; }
        SolidColorBrush LinkPresenterBottomBarBackground { get; set; }
        SolidColorBrush LinkPresenterTextBlocksForeground { get; set; }
        SolidColorBrush LinkPresenterInformationGridBackground { get; set; }
        SolidColorBrush LinkPresenterSelectorBackground { get; set; }
        SolidColorBrush LinkPresenterImageBorderBrush { get; set; }
        SolidColorBrush LinkPresenterImageBackground { get; set; }
    }
}
