using System;
using System.Globalization;
using System.Windows.Data;

namespace Common.Settings.Converters
{ 
    public class ConvertNameTheme : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString() == "Dark" ? "Темная" : "Светлая";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}