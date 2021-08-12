using System;
using System.Collections.Generic;
using System.Text;

namespace RTM.Common.Models
{
    public class NotificationModel
    {
        public NotificationType NotificationType { get; set; }
        public string Message { get; set; }
    }

    public enum NotificationType
    {
        Successfully = 1,
        Info = 2,
        Error = 3
    }
}
