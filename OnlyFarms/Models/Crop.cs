using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Models
{
    public class Crop
    {
        public int ID { get; set; }

        [Required]
        public string CropName { get; set; }

        public int Price { get; set; }

        [Required]
        public int ExpectedYield { get; set; }
    }
}
