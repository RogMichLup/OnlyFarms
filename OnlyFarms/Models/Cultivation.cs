using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Models {
    public class Cultivation {
        public int Id { get; set; }
        public int AreaInHectar { get; set; }
        public string CropStatus { get; set; }
        public Crop Crop { get; set; }
        public Field Field { get; set; }
    }
}
