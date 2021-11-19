using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Models {
    public class Equipment {
        public int ID { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [Range(1, 10000)]
        public double AmortizationCost { get; set; }

        public string Status { get; set; }
    }
}
