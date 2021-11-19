using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlyFarms.Models
{
    public class Supply {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [Range(0, 1000)]
        public double PricePerKilo { get; set; }

        [Required]
        [Range(0, 10000)]
        public int SupplyAmountPerHectare { get; set; }

        public ICollection<Procedure> Procedures { get; set; }
    }
}
