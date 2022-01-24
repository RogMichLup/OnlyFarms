using OnlyFarms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Services
{
    public interface IWeatherObserver
    {
        void Update(WeatherUnit weather);
    }
}
