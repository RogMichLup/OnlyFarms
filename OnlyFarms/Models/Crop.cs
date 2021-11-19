using System;
using System.ComponentModel.DataAnnotations;

namespace OnlyFarms.Models
{
    public class Crop
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(30)]
        public string CropName { get; set; }

        [Required]
        [Range(0, 10000)]
        public int SellPricePerTonne { get; set; }

        [Required]
        [Range(0, 500)]
        public int ExpectedYield { get; set; }

    }
}
