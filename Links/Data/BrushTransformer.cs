using System.Windows.Media;

namespace Links.Data
{
    internal static class BrushTransformer
    {
        public static SolidColorBrush ToSolidColorBrush(string value)
        {
            return new SolidColorBrush((Color)ColorConverter.ConvertFromString(value));
        }
    }
}
