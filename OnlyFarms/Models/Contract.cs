using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Models
{
    public class Contract
    {
        public int ID { get; set; }
        public int Quantity { get; set; }
        public DateTime DeliveryDate { get; set; }

        public int CropID { get; set; }
        public Crop Crop { get; set; }    
    }
}
