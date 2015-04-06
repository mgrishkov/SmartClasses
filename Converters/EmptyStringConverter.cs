using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace SmartClasses.Converters
{
    public class EmptyStringConverter : MarkupExtension, IValueConverter
    {
        public EmptyStringConverter()
        {
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null || String.IsNullOrWhiteSpace(value.ToString()) || (value.GetType() == typeof(int) && (int)value < 0) ? (string)(parameter ?? "Нет данных") : value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
        
    }
}
