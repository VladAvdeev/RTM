using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RTM.Server.Models
{
    public class TestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
        public int Year { get => CalculateYear(); }

        public TestModel(int id, string name, DateTime birthday)
        {
            Id = id;
            Name = name;
            BirthDay = birthday;
        }

        private int CalculateYear()
        {
            int year = DateTime.Now.Year - BirthDay.Year;
            if (BirthDay > DateTime.Now.AddYears(-year))
                --year;
            return year;

        }
    }
}
