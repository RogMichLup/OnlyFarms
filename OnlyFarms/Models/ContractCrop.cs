using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlyFarms.Models
{
    public class ContractCrop
    {
        [Key]
        public int ID { get; set; }

        public int ContractID { get; set; }

        [ForeignKey("Contract")]
        public Contract Contract { get; set; }
        public int CropID { get; set; }

        [ForeignKey("Crop")]
        public Crop Crop { get; set; }

        [Required]
        [Range(0, 1000)]
        public int Quantity { get; set; }
    }
}
