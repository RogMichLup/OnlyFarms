using OnlyFarms.Models;

namespace OnlyFarms.State
{
    class NotAvailableState : IState
    {
        public string IsAvailableForWork(Machine machine)
        {
            if (string.Equals(machine.Status, "For repair") || string.Equals(machine.Status, "For clean up"))
            {
                return "Machine is not available for work. ";
            }
            else
            {
                machine.ChangeState(new AvailableState());
                return new AvailableState().IsAvailableForWork(machine);
            }
        }

        public string FuelConsumption(Machine machine)
        {
            if (string.Equals(machine.Status, "For repair") || string.Equals(machine.Status, "For clean up"))
            {
                return "Machine is not consuming any fuel at the moment. ";
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
                return "Machine is not available because of repair. ";
            }
            else if (string.Equals(machine.Status, "For clean up"))
            {
                return "Machine is not available because of cleanup. ";
            }
            else
            {
                machine.ChangeState(new NotAvailableState());
                return new AvailableState().WhyUnavailable(machine);
            }
        }
    }
}
