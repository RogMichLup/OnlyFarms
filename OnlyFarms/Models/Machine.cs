using System;
using System.ComponentModel.DataAnnotations;

namespace OnlyFarms.Models
{
    public class Machine {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [Range(1, 10000)]
        public double UtilizationCost { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; }

        [Required]
        [Range(1, 100)]
        public double FuelUsageRate { get; set; }
    }
}
