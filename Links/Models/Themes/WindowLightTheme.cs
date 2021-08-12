using Links.Data;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Links.Models.Themes
{
    internal sealed class WindowLightTheme : WindowTheme
    {
        public WindowLightTheme() : base("Light")
        {
            WindowBackground = BrushTransformer.ToSolidColorBrush("#EFF8FF");
            WindowSystemTopBarBackground = BrushTransformer.ToSolidColorBrush("#464775");
            WindowGridSplitterBackground = BrushTransformer.ToSolidColorBrush("#464775");

            TitleEffect = new DropShadowEffect() { ShadowDepth = 3, BlurRadius = 5, Color = Colors.Black };
            TitleForeground = BrushTransformer.ToSolidColorBrush("#E2E1E6");
            TitleForegroundMouseOver = BrushTransformer.ToSolidColorBrush("#FFD700");

            SystemButtonItemBackground = BrushTransformer.ToSolidColorBrush("#F7F6DA");

            MinimizeWindowButtonBackground = BrushTransformer.ToSolidColorBrush("#464775");
            MinimizeWindowButtonBackgroundMouseOver = BrushTransformer.ToSolidColorBrush("#5B6BCD");
            MinimizeWindowButtonBackgroundPressed = BrushTransformer.ToSolidColorBrush("#4457CC");

            MaximizeWindowButtonBackground = BrushTransformer.ToSolidColorBrush("#464775");
            MaximizeWindowButtonBackgroundMouseOver = BrushTransformer.ToSolidColorBrush("#5B6BCD");
            MaximizeWindowButtonBackgroundPressed = BrushTransformer.ToSolidColorBrush("#4457CC");

            CloseWindowButtonBackground = BrushTransformer.ToSolidColorBrush("#464775");
            CloseWindowButtonBackgroundMouseOver = BrushTransformer.ToSolidColorBrush("#CD5B5B");
            CloseWindowButtonBackgroundPressed = BrushTransformer.ToSolidColorBrush("#853C3C");

            IconButtonItemBackground = BrushTransformer.ToSolidColorBrush("#303038");
            IconButtonBackgroundMouseOver = BrushTransformer.ToSolidColorBrush("#BABABB");
            IconButtonBackgroundPressed = BrushTransformer.ToSolidColorBrush("#8B9BCF");

            SettingsFieldTextBlocksForeground = BrushTransformer.ToSolidColorBrush("#000000");
            SettingsFieldBottomBarBackground = BrushTransformer.ToSolidColorBrush("#464775");

            GroupsFieldBorderBrush = BrushTransformer.ToSolidColorBrush("#696969");
            GroupsFieldSubborderBrush = BrushTransformer.ToSolidColorBrush("#000000");
            GroupsFieldTextBlocksForeground = BrushTransformer.ToSolidColorBrush("#212123");

            GroupsFieldIconButtonItemBackground = BrushTransformer.ToSolidColorBrush("#303038");
            GroupsFieldIconButtonBackground = BrushTransformer.ToSolidColorBrush("#DCE0DF");
            GroupsFieldIconButtonBackgroundMouseOver = BrushTransformer.ToSolidColorBrush("#BABABB");
            GroupsFieldIconButtonBackgroundPressed = BrushTransformer.ToSolidColorBrush("#8B9BCF");

            GroupEditorBackground = BrushTransformer.ToSolidColorBrush("#DCE0DF");

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
