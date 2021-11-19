using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlyFarms.Models
{
    public class Cultivation {
        [Key]
        public int ID { get; set; }

        [Required]
        [Range (1, 1000)]
        public int AreaInHectar { get; set; }

        [Required]
        [MaxLength(50)]
        public string CropStatus { get; set; }

        public int CropID { get; set; }

        public int FieldID { get; set; }
        
        public Crop Crop { get; set; }

        public Field Field { get; set; }
    }
}
