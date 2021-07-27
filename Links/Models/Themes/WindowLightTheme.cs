using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Links.Models.Themes
{
    internal sealed class WindowLightTheme : WindowTheme
    {
        public WindowLightTheme()
        {
            WindowBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EFF8FF"));
            TopBarBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#464775"));

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
        }
    }
}
