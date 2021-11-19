using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Models
{
    public class CropSale
    {
        public int ID { get; set; }

        [Required]
        public int Quantity { get; set; }

        public DateTime SaleDate { get; set; }

        public int CropID { get; set; }

        [Required]
        public Crop Crop { get; set; }
    }
}
