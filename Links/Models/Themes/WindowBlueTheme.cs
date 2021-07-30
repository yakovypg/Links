using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Links.Models.Themes
{
    internal sealed class WindowBlueTheme : WindowTheme
    {
        public WindowBlueTheme()
        {
            WindowBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333449"));
            WindowTopBarBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#464775"));
            WindowGridSplitterBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#696969"));

            TopBarBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#DCE0DF"));

            SettingsPageTextBlocksForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E2E1E6"));
            SettingsPageBottomBarBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#464775"));

            TitleEffect = new DropShadowEffect() { ShadowDepth = 3, BlurRadius = 5, Color = Colors.Black };
            TitleForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E2E1E6"));
            TitleForegroundMouseOver = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD700"));

            SystemButtonItemBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F7F6DA"));

            MinimizeWindowButtonBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#464775"));
            MinimizeWindowButtonBackgroundMouseOver = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5B6BCD"));
            MinimizeWindowButtonBackgroundPressed = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4457CC"));

            MaximizeWindowButtonBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#464775"));
            MaximizeWindowButtonBackgroundMouseOver = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5B6BCD"));
            MaximizeWindowButtonBackgroundPressed = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4457CC"));

            CloseWindowButtonBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#464775"));
            CloseWindowButtonBackgroundMouseOver = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CD5B5B"));
            CloseWindowButtonBackgroundPressed = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#853C3C"));

            IconButtonItemBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#303038"));
            IconButtonBackgroundMouseOver = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BABABB"));
            IconButtonBackgroundPressed = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8B9BCF"));

            GroupFieldBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#696969"));
            GroupFieldSubborderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
            GroupFieldTextBlocksForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E2E1E6"));

            GroupFieldIconButtonItemBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#303038"));
            GroupFieldIconButtonBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#DCE0DF"));
            GroupFieldIconButtonBackgroundMouseOver = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BABABB"));
            GroupFieldIconButtonBackgroundPressed = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8B9BCF"));

            LinkPresenterGridBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#AAAAAA"));
            LinkPresenterBottomBarBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F5DEB3"));
            LinkPresenterTextBlocksForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EAEAEA"));
            LinkPresenterInformationGridBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#AAAAAA"));
            LinkPresenterImageBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#696969"));
            LinkPresenterImageBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
        }
    }
}
