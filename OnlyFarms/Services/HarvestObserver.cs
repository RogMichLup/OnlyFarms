using Microsoft.AspNetCore.Http;
using OnlyFarms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Services
{
    public class HarvestObserver : IWeatherObserver
    {
        HttpContextAccessor httpContextAccessor = new HttpContextAccessor();
        
        //Some action with notifying of the harvest status.
        public void Update(WeatherUnit weather)
        {
            string rainfallAmount = "";
            string temperature = "";
            string communicate = "";

            if (weather.RainfallAmount != 2000000)
            {
                rainfallAmount = "Rain: " + weather.RainfallAmount.ToString() + "mm";
            }
            if(weather.Temperature != 2000000)
            {
                temperature = "Temperature: " + weather.Temperature.ToString() + "°C";
            }
            if(weather.Temperature == 2000000 && weather.RainfallAmount == 2000000)
            {
                communicate = "Rain gauge or thermometer needed";
            }
            else if (weather.Temperature > 10 && weather.RainfallAmount < 10)
            {
                communicate+= "Good weather for harvest!";
            }
            else if (weather.Temperature < 10 || weather.RainfallAmount > 10)
            {
                communicate += "Sorry, you should wait with harving...";
            }

            string field = weather.Field.Tag;
            httpContextAccessor.HttpContext.Session.SetString("HarvestObserverField", field);

            httpContextAccessor.HttpContext.Session
            .SetString("HarvestObserverDetails", "Newest weather: " + 
            rainfallAmount + " " + temperature + " " + communicate);

        }
    }
}
