using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Models {
    public class Equipment {
        public int Id { get; set; }
        public string Name { get; set; }
        public double AmortizationCost { get; set; }
        public string Status { get; set; }
    }
}
