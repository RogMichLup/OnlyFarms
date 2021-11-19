using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlyFarms.Models
{
    public class Procedure
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Label { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Range(0, 100)]
        [MaxLength(50)]
        public double? DurationInHours { get; set; }

        [Required]
        public string Status { get; set; }
        public int FieldID { get; set; }
        public int EquipmentID { get; set; }
        public int MachineID { get; set; }
        public int WorkerID { get; set; }
        
        public Field Field { get; set; }
        
        public Equipment Equipment { get; set; }

        public Machine Machine { get; set; }

        public Worker Worker { get; set; }
        public ICollection<Supply> Supplies { get; set; }
    }
}
