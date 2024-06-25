using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AG.WPF.Resources.Converters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public sealed class InvertedVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not bool val) return null;
            return val ? Visibility.Hidden : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility.Hidden) return true;
            if (value is Visibility.Visible) return false;
            return null;
        }
    }
}
