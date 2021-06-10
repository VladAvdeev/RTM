using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RTM.WPF.Clients.Other.Models
{
    class TestModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("birthDay")]
        public DateTime BirthDay { get; set; }
        [JsonPropertyName("year")]
        public int Year { get; }

        public TestModel(int id, string name, DateTime birthday)
        {
            Id = id;
            Name = name;
            BirthDay = birthday;
        }
    }
}
