using RTM.Common.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RTM.WPF.Converters
{
    public class NotificationTypeColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
                switch ((NotificationType)value)
                {
                    case NotificationType.Successfully:
                        return "#77F5BA";
                    case NotificationType.Info:
                        return "#77E0F5";
                    case NotificationType.Error:
                        return "#F36E6E";
                    default: return "#D3DBDA";
                }
            else
                return "#D3DBDA";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
