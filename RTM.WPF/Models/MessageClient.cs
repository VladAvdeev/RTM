﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RTM.WPF.Models
{
    public class MessageClient
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("user")]
        public UserClient User { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
    }
}
