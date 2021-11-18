using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Models
{
    class Procedure
    {
        public int ProcedureID { get; set; }
        public string Tag { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public int ProductsAmount { get; set; }
        public int ProductID { get; set; }
        public int FieldID { get; set; }
        public int EquipmentID { get; set; }
        public int MachineID { get; set; }
        public int WorkerID { get; set; }
        public string State { get; set; }
    }
}
