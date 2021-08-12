using RTM.WPF.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RTM.WPF.Models
{
    public class UserClient : NotifyPropertyChanged
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        private bool? isOnline;
        public bool? IsOnline { get => isOnline; set => SetProperty(ref isOnline, value); }
    }
}
