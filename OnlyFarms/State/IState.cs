using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlyFarms.Models;

namespace OnlyFarms.State
{
    public interface IState
    {
        //protected Machine machine { get; set; }
        
        public bool IsAvailableForWork(Machine machine);
        public int FuelConsumption(Machine machine);
        public string WhyUnavailable(Machine machine);
    }
    class AvailableState : IState
    {
        // TODO change return values to differ from NotAvailable
        public bool IsAvailableForWork(Machine machine)
        {
            if (string.Equals(machine.Status, "For Repair") || string.Equals(machine.Status, "For Cleanup"))
            {
                machine.ChangeState(new NotAvailableState());
                return new NotAvailableState().IsAvailableForWork(machine);
            }
            else
                return true;
        }

        public int FuelConsumption(Machine machine)
        {
            if (string.Equals(machine.Status, "For Repair") || string.Equals(machine.Status, "For Cleanup"))
            {
                machine.ChangeState(new NotAvailableState());
                return 0;
            }
            else
                return (int)machine.FuelUsageRate;
        }

        public string WhyUnavailable(Machine machine)
        {
            if (string.Equals(machine.Status, "For Repair") || string.Equals(machine.Status, "For Cleanup"))
            {
                machine.ChangeState(new NotAvailableState());
                return new NotAvailableState().WhyUnavailable(machine);
            }
            else
            {
                return "Machine is available for work";
            }
        }
    }
    class NotAvailableState : IState
    {
        // TODO change return values to differ from Available
        public bool IsAvailableForWork(Machine machine)
        {
            if (string.Equals(machine.Status, "For Repair") || string.Equals(machine.Status, "For Cleanup"))
            {
                return false;
            }
            else
            {
                machine.ChangeState(new AvailableState());
                return new AvailableState().IsAvailableForWork(machine);
            }
        }

        public int FuelConsumption(Machine machine)
        {
            if (string.Equals(machine.Status, "For Repair") || string.Equals(machine.Status, "For Cleanup"))
            {
                return 0;
            }
            else
            {
                machine.ChangeState(new AvailableState());
                return (int)machine.FuelUsageRate;
            }
        }

        public string WhyUnavailable(Machine machine)
        {
            if (string.Equals(machine.Status, "For Repair"))
            {
                return "Machine is not available because of repair";
            }
            else if (string.Equals(machine.Status, "For Cleanup"))
            {
                return "Machine is not available because of cleanup";
            }
            else
            {
                machine.ChangeState(new NotAvailableState());
                return new AvailableState().WhyUnavailable(machine);
            }
        }
    }
}
