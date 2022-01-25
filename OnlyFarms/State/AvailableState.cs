using OnlyFarms.Models;

namespace OnlyFarms.State
{
    class AvailableState : IState
    {
        public string IsAvailableForWork(Machine machine)
        {
            if (string.Equals(machine.Status, "For repair") || string.Equals(machine.Status, "For clean up"))
            {
                machine.ChangeState(new NotAvailableState());
                return new NotAvailableState().IsAvailableForWork(machine);
            }
            else
                return "Machine is available for work. ";
        }

        public string FuelConsumption(Machine machine)
        {
            if (string.Equals(machine.Status, "For repair") || string.Equals(machine.Status, "For clean up"))
            {
                machine.ChangeState(new NotAvailableState());
                return new NotAvailableState().FuelConsumption(machine);
            }
            else
                return "Machine current fuel consumption is: " + machine.FuelUsageRate.ToString() + ". ";
        }

        public string WhyUnavailable(Machine machine)
        {
            if (machine.Status == "For repair" || string.Equals(machine.Status, "For clean up"))
            {
                machine.ChangeState(new NotAvailableState());
                return new NotAvailableState().WhyUnavailable(machine);
            }
            else
            {
                return "There is no reason for the machine to be unavailable. ";
            }
        }
    }
}
