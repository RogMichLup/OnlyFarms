using OnlyFarms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Services
{
    public class HarvestObserver : IWeatherObserver
    {
        public void Update(Weather weather)
        {
            //TODO: Some action with notifying of the harvest date change.
            Console.WriteLine("Date of harvest is updated");
        }
    }
}
