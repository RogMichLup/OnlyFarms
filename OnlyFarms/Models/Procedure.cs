using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Models
{
    public class Procedure
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public DateTime Date { get; set; }
        public double DurationInHours { get; set; }
        public double SupplyAmountInKilo { get; set; }
        //public int SupplyID { get; set; }
        //public int FieldID { get; set; }
        //public int EquipmentID { get; set; }
        //public int MachineID { get; set; }
        //public int WorkerID { get; set; }
        public Supply Supply { get; set; }
        public Field Field { get; set; }
        public Equipment Equipment { get; set; }
        public Machine Machine { get; set; }
        public Worker Worker { get; set; }
        public string State { get; set; }
    }
}
