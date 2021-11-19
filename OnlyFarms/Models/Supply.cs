using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Models {
    public class Supply {
        public int ID { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public double PricePerKilo { get; set; }
    }
}
