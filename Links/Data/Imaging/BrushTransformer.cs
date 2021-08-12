﻿using System.Windows.Media;

namespace Links.Data.Imaging
{
    internal static class BrushTransformer
    {
        public static SolidColorBrush ToSolidColorBrush(string value)
        {
            return new SolidColorBrush((Color)ColorConverter.ConvertFromString(value));
        }
    }
}
