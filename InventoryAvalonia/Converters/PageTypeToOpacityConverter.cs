using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Repository.Core;

namespace InventoryAppAvalonia.Converters
{
    public class PageTypeToOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PageType activePage && parameter is string targetPage)
            {
                if (Enum.TryParse(targetPage, out PageType targetPageType))
                {
                    return activePage == targetPageType ? 1.0 : 0.0;
                }
            }
            return 0.5;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
