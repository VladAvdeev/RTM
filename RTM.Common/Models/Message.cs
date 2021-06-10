using System;
using System.Collections.Generic;
using System.Text;

namespace RTM.Common.Models
{
    public class Message
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}
