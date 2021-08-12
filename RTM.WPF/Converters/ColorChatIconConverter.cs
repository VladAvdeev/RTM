using RTM.WPF.Helpers;
using RTM.WPF.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RTM.WPF.Converters
{
    public class ColorChatIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<UserClient> users = (value as List<UserClient>).Where(x => x.Id != UserManager.User.Id).ToList();
            if (users.Any(x => x.IsOnline.HasValue && x.IsOnline.Value))
                return "#28B463";
            else
                return "#E74C3C";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
