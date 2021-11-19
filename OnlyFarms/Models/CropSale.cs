using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlyFarms.Models
{
    public class CropSale
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Range(0, 1000)]
        public int Quantity { get; set; }

        public DateTime? SaleDate { get; set; }

        public int CropID { get; set; }

        [ForeignKey("Crop")]
        public Crop Crop { get; set; }
    }
}
