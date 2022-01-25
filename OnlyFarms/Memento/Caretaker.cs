using OnlyFarms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Memento
{
    public static class Caretaker
    {
        private static Snapshot backup;

        public static void MakeBackup(Procedure procedure)
        {
            backup = procedure.CreateSnapshot();
        }

        public static Procedure Undo()
        {
            if(backup != null)
            {
                return backup.Restore();
            }
            else
            {
                return null;
            }
        }
    }
}
