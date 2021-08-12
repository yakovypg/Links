using Links.Data.Imaging;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Links.Models.Themes
{
    internal sealed class WindowDarkTheme : WindowTheme
    {
        public WindowDarkTheme() : base("Dark")
        {
            WindowBackground = BrushTransformer.ToSolidColorBrush("#212123");
            WindowSystemTopBarBackground = BrushTransformer.ToSolidColorBrush("#303038");
            WindowGridSplitterBackground = BrushTransformer.ToSolidColorBrush("#696969");

            TitleEffect = new DropShadowEffect() { ShadowDepth = 3, BlurRadius = 5, Color = Colors.Black };
            TitleForeground = BrushTransformer.ToSolidColorBrush("#E2E1E6");
            TitleForegroundMouseOver = BrushTransformer.ToSolidColorBrush("#FFD700");

            SystemButtonItemBackground = BrushTransformer.ToSolidColorBrush("#F7F6DA");

            MinimizeWindowButtonBackground = BrushTransformer.ToSolidColorBrush("#303038");
            MinimizeWindowButtonBackgroundMouseOver = BrushTransformer.ToSolidColorBrush("#5B6BCD");
            MinimizeWindowButtonBackgroundPressed = BrushTransformer.ToSolidColorBrush("#3A457C");

            MaximizeWindowButtonBackground = BrushTransformer.ToSolidColorBrush("#303038");
            MaximizeWindowButtonBackgroundMouseOver = BrushTransformer.ToSolidColorBrush("#5B6BCD");
            MaximizeWindowButtonBackgroundPressed = BrushTransformer.ToSolidColorBrush("#3A457C");

            CloseWindowButtonBackground = BrushTransformer.ToSolidColorBrush("#303038");
            CloseWindowButtonBackgroundMouseOver = BrushTransformer.ToSolidColorBrush("#CD5B5B");
            CloseWindowButtonBackgroundPressed = BrushTransformer.ToSolidColorBrush("#853C3C");

            IconButtonItemBackground = BrushTransformer.ToSolidColorBrush("#303038");
            IconButtonBackgroundMouseOver = BrushTransformer.ToSolidColorBrush("#BABABB");
            IconButtonBackgroundPressed = BrushTransformer.ToSolidColorBrush("#8B9BCF");

            SettingsFieldTextBlocksForeground = BrushTransformer.ToSolidColorBrush("#E2E1E6");
            SettingsFieldBottomBarBackground = BrushTransformer.ToSolidColorBrush("#303038");

            GroupsFieldBorderBrush = BrushTransformer.ToSolidColorBrush("#696969");
            GroupsFieldSubborderBrush = BrushTransformer.ToSolidColorBrush("#000000");
            GroupsFieldTextBlocksForeground = BrushTransformer.ToSolidColorBrush("#E2E1E6");

            GroupsFieldIconButtonItemBackground = BrushTransformer.ToSolidColorBrush("#303038");
            GroupsFieldIconButtonBackground = BrushTransformer.ToSolidColorBrush("#DCE0DF");
            GroupsFieldIconButtonBackgroundMouseOver = BrushTransformer.ToSolidColorBrush("#BABABB");
            GroupsFieldIconButtonBackgroundPressed = BrushTransformer.ToSolidColorBrush("#8B9BCF");

            GroupEditorBackground = BrushTransformer.ToSolidColorBrush("#303038");

            CreatorMenuBackground = BrushTransformer.ToSolidColorBrush("#DCE0DF");
            CreatorMenuBorderBrush = BrushTransformer.ToSolidColorBrush("#FFD700");
            CreatorMenuSubborderBrush = BrushTransformer.ToSolidColorBrush("#000000");

            LinksFieldTopBarBackground = BrushTransformer.ToSolidColorBrush("#DCE0DF");

            LinkPresenterGridBackground = BrushTransformer.ToSolidColorBrush("#AAAAAA");
            LinkPresenterBottomBarBackground = BrushTransformer.ToSolidColorBrush("#F5DEB3");
            LinkPresenterTextBlocksForeground = BrushTransformer.ToSolidColorBrush("#000000");
            LinkPresenterInformationGridBackground = BrushTransformer.ToSolidColorBrush("#AAAAAA");
            LinkPresenterImageBorderBrush = BrushTransformer.ToSolidColorBrush("#696969");
            LinkPresenterImageBackground = BrushTransformer.ToSolidColorBrush("#FFFFFF");
        }
    }
}
