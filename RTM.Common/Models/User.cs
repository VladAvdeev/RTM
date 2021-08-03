using System;
using System.Collections.Generic;
using System.Text;

namespace RTM.Common.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Grants { get; set; } = new List<string>();
        public string Token { get; set; }
        public User(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public User()
        { 
            
        }
    }
}
