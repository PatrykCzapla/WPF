using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Lab4
{
    /// <summary>
    /// Converts date to preferable format
    /// </summary>
    class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DateTime))
                return null;
            DateTime date = (DateTime)value;
            string finalDate = date.Day.ToString() + "/" + date.Month.ToString() + "/" + date.Year.ToString();
            return finalDate;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
