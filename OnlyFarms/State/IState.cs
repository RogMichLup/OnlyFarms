using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlyFarms.Models;

namespace OnlyFarms.State
{
    public interface IState
    {
        public string IsAvailableForWork(Machine machine);
        public string FuelConsumption(Machine machine);
        public string WhyUnavailable(Machine machine);
    }
    class AvailableState : IState
    {
        // TODO change return values to differ from NotAvailable
        public string IsAvailableForWork(Machine machine)
        {
            if (string.Equals(machine.Status, "For repair") || string.Equals(machine.Status, "For cleanup"))
            {
                machine.ChangeState(new NotAvailableState());
                return new NotAvailableState().IsAvailableForWork(machine);
            }
            else
                return "Machine is available for work";
        }

        public string FuelConsumption(Machine machine)
        {
            if (string.Equals(machine.Status, "For repair") || string.Equals(machine.Status, "For cleanup"))
            {
                machine.ChangeState(new NotAvailableState());
                return new NotAvailableState().FuelConsumption(machine);
            }
            else
                return "Machine is current fuel consumption is: " + machine.FuelUsageRate.ToString();
        }

        public string WhyUnavailable(Machine machine)
        {
            if (machine.Status == "For repair" || string.Equals(machine.Status, "For cleanup"))
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
        public string IsAvailableForWork(Machine machine)
        {
            if (string.Equals(machine.Status, "For repair") || string.Equals(machine.Status, "For cleanup"))
            {
                return "Machine is not available for work";
            }
            else
            {
                machine.ChangeState(new AvailableState());
                return new AvailableState().IsAvailableForWork(machine);
            }
        }

        public string FuelConsumption(Machine machine)
        {
            if (string.Equals(machine.Status, "For repair") || string.Equals(machine.Status, "For cleanup"))
            {
                return "Machine is not consuming any fuel at the moment";
            }
            else
            {
                machine.ChangeState(new AvailableState());
                return new AvailableState().FuelConsumption(machine);
            }
        }

        public string WhyUnavailable(Machine machine)
        {
            if (string.Equals(machine.Status, "For repair"))
            {
                return "Machine is not available because of repair";
            }
            else if (string.Equals(machine.Status, "For cleanup"))
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
