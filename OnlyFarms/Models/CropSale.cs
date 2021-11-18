using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Models
{
    public class CropSale
    {
        public int ID { get; set; }
        public int Quantity { get; set; }
        public DateTime SaleDate { get; set; }
        public Crop Crop { get; set; }
    }
}
