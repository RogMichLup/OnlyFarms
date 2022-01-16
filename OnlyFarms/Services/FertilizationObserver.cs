using OnlyFarms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Services
{
    public class FertilizationObserver : IWeatherObserver
    {
        public void Update(Weather weather)
        {
            //TODO: Some action with notifying of the fertilization date change.
            Console.WriteLine("Date of fertilization is updated");
        }
    }
}
