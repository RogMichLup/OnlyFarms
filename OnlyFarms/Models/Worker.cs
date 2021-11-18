using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Models {
    public class Worker {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public double HourlyPay { get; set; }
        public DateTime HiringDate { get; set; }
    }
}
