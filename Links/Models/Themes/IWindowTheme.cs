using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Links.Models.Themes
{
    internal interface IWindowTheme
    {
        string DisplayName { get; }

        Brush WindowBackground { get; set; }
        Brush WindowSystemTopBarBackground { get; set; }
        Brush WindowGridSplitterBackground { get; set; }

        DropShadowEffect TitleEffect { get; set; }
        Brush TitleForeground { get; set; }
        Brush TitleForegroundMouseOver { get; set; }

        Brush SystemButtonItemBackground { get; set; }

        Brush MinimizeWindowButtonBackground { get; set; }
        Brush MinimizeWindowButtonBackgroundMouseOver { get; set; }
        Brush MinimizeWindowButtonBackgroundPressed { get; set; }

        Brush MaximizeWindowButtonBackground { get; set; }
        Brush MaximizeWindowButtonBackgroundMouseOver { get; set; }
        Brush MaximizeWindowButtonBackgroundPressed { get; set; }

        Brush CloseWindowButtonBackground { get; set; }
        Brush CloseWindowButtonBackgroundMouseOver { get; set; }
        Brush CloseWindowButtonBackgroundPressed { get; set; }

        Brush IconButtonItemBackground { get; set; }
        Brush IconButtonBackgroundMouseOver { get; set; }
        Brush IconButtonBackgroundPressed { get; set; }

        Brush SettingsFieldTextBlocksForeground { get; set; }
        Brush SettingsFieldBottomBarBackground { get; set; }

        Brush GroupsFieldBorderBrush { get; set; }
        Brush GroupsFieldSubborderBrush { get; set; }
        Brush GroupsFieldTextBlocksForeground { get; set; }

        Brush GroupsFieldIconButtonItemBackground { get; set; }
        Brush GroupsFieldIconButtonBackground { get; set; }
        Brush GroupsFieldIconButtonBackgroundMouseOver { get; set; }
        Brush GroupsFieldIconButtonBackgroundPressed { get; set; }

        Brush GroupEditorBackground { get; set; }

        Brush CreatorMenuBackground { get; set; }
        Brush CreatorMenuBorderBrush { get; set; }
        Brush CreatorMenuSubborderBrush { get; set; }

        Brush LinksFieldTopBarBackground { get; set; }

        Brush LinkPresenterGridBackground { get; set; }
        Brush LinkPresenterBottomBarBackground { get; set; }
        Brush LinkPresenterTextBlocksForeground { get; set; }
        Brush LinkPresenterInformationGridBackground { get; set; }
        Brush LinkPresenterImageBorderBrush { get; set; }
        Brush LinkPresenterImageBackground { get; set; }
    }
}
