using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Models
{
    public class Contract
    {
        public int ID { get; set; }

        [Required]
        [Range (1, 1000)]
        public int Quantity { get; set; }

        [Required]
        public DateTime DeliveryDate { get; set; }

        public int CropID { get; set; }

        [Required]
        public Crop Crop { get; set; }    
    }
}
