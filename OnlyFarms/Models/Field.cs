using System;
using System.ComponentModel.DataAnnotations;

namespace OnlyFarms.Models
{
    public class Field
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(50)]
        public string? Tag { get; set; }

        [Required]
        [MaxLength(30)]
        public string City { get; set; }

        [MaxLength(30)]
        public string? Street { get; set; }

        [Required]
        [Range(0, 1000)]
        public int FieldSurface { get; set; }
    }
}
