using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using RTM.Common.Models;
using RTM.WPF.Helpers;

namespace RTM.WPF.Converters
{
    class MessageAligConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            User user = value as User;
            if (user.Id == UserManager.User.Id)
                return 1;
            else
                return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
