using System;
using System.Globalization;
using System.Windows.Data;

namespace Core.Settings.Converters
{
    public class ConvertNameTheme : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString() == "Dark" ? "Tемная" : "Cветлая";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}