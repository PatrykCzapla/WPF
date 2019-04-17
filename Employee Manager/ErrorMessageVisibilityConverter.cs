using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Lab4
{
    /// <summary>
    /// Converts visibility of error textblock according to the occurence of validation error
    /// </summary>
    public class ErrorMessageVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ReadOnlyObservableCollection<ValidationError> collection = (ReadOnlyObservableCollection<ValidationError>)value;
            if (collection == null) return null;
            if (collection.Count > 0) return Visibility.Visible;
            else return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}