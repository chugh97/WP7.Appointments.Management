using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace WP7.Appointments.Management.Converters
{
    public class AmPmTimeConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            TimeSpan ts = (TimeSpan)value;
            if (ts.Hours >=0 && ts.Hours < 12)
            {
                return string.Format("{0}:{1} {2}", ts.Hours.ToString("00"), ts.Minutes.ToString("00"), "AM");
            }
            else
            {
                return string.Format("{0}:{1} {2}", ts.Hours.ToString("00"), ts.Minutes.ToString("00"), "PM");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
