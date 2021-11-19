using System;
using System.ComponentModel.DataAnnotations;

namespace OnlyFarms.Models
{
    public class Worker {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public string Surname { get; set; }

        [Required]
        [Range (1, 500)]
        public double HourlyPay { get; set; }

        public DateTime? HiringDate { get; set; }
    }
}
