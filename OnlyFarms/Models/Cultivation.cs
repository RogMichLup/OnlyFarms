using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Models {
    public class Cultivation {
        public int ID { get; set; }

        [Required]
        [Range (1, 1000)]
        public int AreaInHectar { get; set; }

        public string CropStatus { get; set; }

        public int CropID { get; set; }

        public int FieldID { get; set; }

        [Required]
        public Crop Crop { get; set; }

        [Required]
        public Field Field { get; set; }
    }
}
