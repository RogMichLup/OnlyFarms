using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Models {
    public class Worker {
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
        public DateTime HiringDate { get; set; }
    }
}
