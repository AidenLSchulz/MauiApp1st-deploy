using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace MauiApp1.Converters
{
    // Converts a current/target pair into a 0–1 ratio for a ProgressBar.
    public class ProgressConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            // Developer note: 'value' comes from CurrentValue, 'parameter' is TargetValue
            if (value is double current && parameter is double target && target > 0)
            {
                // calculate ratio and clamp between 0 and 1
                var ratio = current / target;
                if (ratio < 0.0) return 0.0;
                if (ratio > 1.0) return 1.0;
                return ratio;
            }

            // Developer note: if inputs invalid, default to zero progress
            return 0.0;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            // Developer note: one-way binding only, so we don't implement ConvertBack
            throw new NotImplementedException();
        }
    }
}
