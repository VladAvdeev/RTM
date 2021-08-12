using RTM.Common.Models;
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
    public class ChatIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IEnumerable<UserClient> users = value as IEnumerable<UserClient>;
            if (users.Count() > 2)
                return "People";
            else
                return "Person";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
