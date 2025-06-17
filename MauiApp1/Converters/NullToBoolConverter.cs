using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace MauiApp1.Converters
{
    /// <summary>
    /// Converts a null reference to bool:
    /// - If parameter="false", returns true when value IS null (invert logic).
    /// - Otherwise returns true when value != null.
    /// </summary>
    public class NullToBoolConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            bool invert = (parameter as string)?.ToLower() == "false";
            bool isNotNull = value != null;
            return invert ? !isNotNull : isNotNull;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
