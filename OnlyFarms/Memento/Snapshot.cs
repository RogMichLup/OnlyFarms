using OnlyFarms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Memento
{
    public class Snapshot
    {
        private Procedure procedure { get; }

        public int ID { get;}

        public string Label { get;}

        public DateTime StartDate { get;}

        public double? DurationInHours { get;}

        public string Status { get;}
        public int FieldID { get;}
        public int EquipmentID { get;}
        public int MachineID { get;}
        public int WorkerID { get;}

        public Field Field { get;}

        public Equipment Equipment { get;}

        public Machine Machine { get;}

        public Worker Worker { get;}
        public ICollection<Supply> Supplies { get;}

        public Snapshot(Procedure procedure, 
                        int ID,
                        string Label,
                        DateTime StartDate,
                        double? DurationInHours,
                        string Status,
                        int FieldID,
                        int EquipmentID,
                        int MachineID,
                        int WorkerID,
                        Field Field,
                        Equipment Equipment,
                        Machine Machine,
                        Worker Worker,
                        ICollection<Supply> Supplies)
        {
            this.procedure = procedure;
            this.ID = ID;
            this.Label = Label;
            this.StartDate = StartDate;
            this.DurationInHours = DurationInHours;
            this.Status = Status;
            this.FieldID = FieldID;
            this.EquipmentID = EquipmentID;
            this.MachineID = MachineID;
            this.WorkerID = WorkerID;
            this.Field = Field;
            this.Equipment = Equipment;
            this.Machine = Machine;
            this.Worker = Worker;
            this.Supplies = Supplies;
        }

        public void Restore()
        {
            procedure.ID = ID;
            procedure.Label = Label;
            procedure.StartDate = StartDate;
            procedure.DurationInHours = DurationInHours;
            procedure.Status = Status;
            procedure.FieldID = FieldID;
            procedure.EquipmentID = EquipmentID;
            procedure.MachineID = MachineID;
            procedure.WorkerID = WorkerID;
            procedure.Field = Field;
            procedure.Equipment = Equipment;
            procedure.Machine = Machine;
            procedure.Worker = Worker;
            procedure.Supplies = Supplies;
        }
    }
}
