using System;
using System.Globalization;
using System.Windows.Data;

namespace Links.Infrastructure.Converters
{
    internal class DeleteGroupParamMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return new Tuple<object, object>(values[0], values[1]);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            var tuple = value as Tuple<object, object>;
            return new object[] { tuple.Item1, tuple.Item2 };
        }
    }
}
