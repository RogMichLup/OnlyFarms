using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Models {
    public class Machine {
        public int ID { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public double AmortizationCost { get; set; }

        public string Status { get; set; }

        [Required]
        [Range(1, 100)]
        public double FuelUsageRate { get; set; }
    }
}
