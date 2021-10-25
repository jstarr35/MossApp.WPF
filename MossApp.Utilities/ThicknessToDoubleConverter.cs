using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MossApp.Utilities
{
#nullable enable
    [ValueConversion(typeof(Thickness), typeof(double), ParameterType = typeof(ThicknessSideType))]
    public class ThicknessToDoubleConverter : IValueConverter
    {
        public ThicknessSideType TakeThicknessSide { get; set; } = ThicknessSideType.None;

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is Thickness thickness)
            {
                ThicknessSideType takeThicknessSide = TakeThicknessSide;

                // yes, we can override it with the parameter value
                if (parameter is ThicknessSideType sideType)
                {
                    takeThicknessSide = sideType;
                }

                return takeThicknessSide switch
                {
                    ThicknessSideType.Left => thickness.Left,
                    ThicknessSideType.Top => thickness.Top,
                    ThicknessSideType.Right => thickness.Right,
                    ThicknessSideType.Bottom => thickness.Bottom,
                    ThicknessSideType.None => throw new NotImplementedException(),
                    _ => default
                };
            }

            return default(double);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    public enum ThicknessSideType
    {
        /// <summary>
        /// Use all sides.
        /// </summary>
        None,
        /// <summary>
        /// Ignore the left side.
        /// </summary>
        Left,
        /// <summary>
        /// Ignore the top side.
        /// </summary>
        Top,
        /// <summary>
        /// Ignore the right side.
        /// </summary>
        Right,
        /// <summary>
        /// Ignore the bottom side.
        /// </summary>
        Bottom
    }
}