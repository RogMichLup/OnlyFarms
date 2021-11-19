using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlyFarms.Models
{
    public class Weather
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Range (-50, 50)]
        public double Temperature { get; set; }

        [Required]
        [Range (0, 100)]
        public int Moisture { get; set; }

        [Required]
        [Range (900, 1100)]
        public int AirPressure { get; set; }

        [Required]
        [Range (0, 1825)]
        public int RainfallAmount { get; set; }

        [Required]
        [MaxLength(5)]
        public string WindDirection { get; set; }

        [Required]
        [Range (0, 200)]
        public int WindSpeed { get; set; }

        [Required]
        public DateTime Date { get; set; }
        public int FieldID { get; set; }

        public Field Field { get; set; }
    }
}
