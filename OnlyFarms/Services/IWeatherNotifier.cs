using OnlyFarms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Services
{
    public interface IWeatherNotifier
    {
        // Attach a weather observer to the subject.
        void Attach(IWeatherObserver observer);

        // Detach a weather observer from the subject.
        void Detach(IWeatherObserver observer);

        // Notify all weather observers about an event.
        void Notify(Weather weather);
    }
}
