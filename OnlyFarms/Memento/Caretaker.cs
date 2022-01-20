using OnlyFarms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Memento
{
    public class Caretaker
    {
        private Snapshot backup;

        public void MakeBackup(Procedure procedure)
        {
            backup = procedure.CreateSnapshot();
        }

        public void Undo()
        {
            if(backup != null)
            {
                backup.Restore();
            }
        }
    }
}
