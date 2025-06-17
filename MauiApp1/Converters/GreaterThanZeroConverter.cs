using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace MauiApp1.Converters
{
    /// <summary>
    /// Returns true if the bound integer (Workouts.Count) is > 0; otherwise false.
    /// Used in MainPage.xaml to show/hide the header and CollectionView only when Workouts.Count &gt; 0.
    /// </summary>
    public class GreaterThanZeroConverter : IValueConverter
    {
        /// <summary>
        /// Converts an integer (Workouts.Count) to a bool: true if >0, false otherwise.
        /// </summary>
        /// <param name="value">The value from the binding (object? but expected int).</param>
        /// <param name="targetType">The target type (bool).</param>
        /// <param name="parameter">Not used (object?).</param>
        /// <param name="culture">Culture info (not used).</param>
        /// <returns>A bool indicating whether the integer is greater than zero.</returns>
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int intValue)
                return intValue > 0;

            return false;
        }

        /// <summary>
        /// ConvertBack is not needed here. We throw by design.
        /// </summary>
        /// <param name="value">The value from the target (object?).</param>
        /// <param name="targetType">The source type (int).</param>
        /// <param name="parameter">Not used (object?).</param>
        /// <param name="culture">Culture info (not used).</param>
        /// <returns>Never returns—throws.</returns>
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
