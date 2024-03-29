﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace RTM.Common.Models
{
    public class ChatGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
        private List<Message> messages;
        public List<Message> Messages { get; set; }
        public Message LastMessage { get => Messages.Where(x => x.Date == Messages.Max(y => y.Date)).FirstOrDefault(); }
    }
}
