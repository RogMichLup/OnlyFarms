using Microsoft.AspNetCore.Http;
using OnlyFarms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Services
{
    public class FertilizationObserver : IWeatherObserver
    {
        HttpContextAccessor httpContextAccessor = new HttpContextAccessor();
        
        //Some action with notifying of the fertilization status.
        public void Update(WeatherUnit weather)
        {
            string rainfallAmount = "";
            string windSpeed = "";
            string communicate = "";

            if (weather.RainfallAmount != 2000000)
            {
                rainfallAmount = "Rain: " + weather.RainfallAmount.ToString() + "mm";
            }
            if (weather.WindSpeed != 2000000)
            {
                windSpeed = "Wind speed: " + weather.WindSpeed.ToString() + "km/h";
            }
            if (weather.WindSpeed == 2000000 && weather.RainfallAmount == 2000000)
            {
                communicate = "Rain gauge or speedanemometer needed";
            }
            else if (weather.WindSpeed < 40 && weather.RainfallAmount < 10)
            {
                communicate += "Good weather for fertilization!";
            }
            else if (weather.WindSpeed > 40 || weather.RainfallAmount > 10)
            {
                communicate += "Sorry, you should wait with fertilization...";
            }

            string field = weather.Field.Tag;

            httpContextAccessor.HttpContext.Session.SetString("FertilizationObserverField", field);

            httpContextAccessor.HttpContext.Session.SetString("FertilizationObserverDetails","Newest weather: " +
                                                                rainfallAmount + " " + windSpeed + " " + communicate);

        }
    }
    
}
