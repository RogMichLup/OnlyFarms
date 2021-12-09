using System;
using System.ComponentModel.DataAnnotations;

namespace OnlyFarms.Models
{
    public class Contract
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(30)]
        public string ClientName { get; set; }

        [Required]
        public DateTime DeliveryDate { get; set; }


    }
}
