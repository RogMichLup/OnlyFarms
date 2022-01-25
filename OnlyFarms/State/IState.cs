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
}
