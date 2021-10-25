using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace MossApp.Utilities
{
    public class BooleanToVisibilityConverter : MarkupExtension, IValueConverter
    {
        public Visibility FalseVisibility { get; set; }
        public bool Negate { get; set; }

        public BooleanToVisibilityConverter()
        {
            FalseVisibility = Visibility.Collapsed;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            return !bool.TryParse(value.ToString(), out bool bVal) ? value : (bVal && !Negate) || (!bVal && Negate) ? Visibility.Visible : (object)FalseVisibility;
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
