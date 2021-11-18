using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Models
{
    public class Crop
    {
        public int ID { get; set; }
        public string CropName { get; set; }
        public int Price { get; set; }
        public int ExpectedYield { get; set; }
    }
}
