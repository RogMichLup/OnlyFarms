﻿using OnlyFarms.Models;
using System.Collections.Generic;

namespace OnlyFarms.Services
{
    public class WeatherService : IWeatherService
    {
        public List<IWeatherObserver> Observers = new List<IWeatherObserver>();

        public void UpdateWeather(Weather weather)
        {
            Notify(weather);
        }

        public void Attach(IWeatherObserver observer)
        {
            Observers.Add(observer);
        }

        public void Detach(IWeatherObserver observer)
        {
            Observers.Remove(observer);
        }

        public void Notify(Weather weather)
        {
            foreach (var observer in Observers)
            {
                observer.Update(weather);
            }
        }
    }
}