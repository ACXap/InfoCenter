using Common.Data;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Common.Settings.Converters
{
    public class ConvertNameEnumTypeView : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var type = (EnumTypeGridViewItem)value;

                switch (type)
                {
                    case EnumTypeGridViewItem.DataGrid:
                        return "Таблица";
                    case EnumTypeGridViewItem.Card:
                        return "Карточки";
                    default:
                        return "Непонятно";
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}