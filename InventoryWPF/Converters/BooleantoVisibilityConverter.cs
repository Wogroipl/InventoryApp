using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace InventoryWPF.Converters;

public class BooleantoVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool)
        {
            bool isInverse = parameter?.ToString() == "Inverse";
            if (isInverse)
            {
                return (bool)value ? Visibility.Collapsed : Visibility.Visible;
            }

            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }
        return Visibility.Collapsed;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Visibility)
        {
            bool isInverse = parameter?.ToString() == "Inverse";
            if (isInverse)
            {
                return Visibility.Collapsed;
            }

            return (Visibility)value == Visibility.Visible;
        }
        return false;
    }
}
