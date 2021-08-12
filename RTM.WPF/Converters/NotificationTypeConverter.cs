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
    public class NotificationTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value != null)
                switch ((NotificationType)value)
                {
                    case NotificationType.Successfully:
                        return "Выполнено";
                    case NotificationType.Info:
                        return "Инфо";
                    case NotificationType.Error:
                        return "Ошибка";
                    default: return "Оповещение";
                }
            else
                return "Оповещение";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
