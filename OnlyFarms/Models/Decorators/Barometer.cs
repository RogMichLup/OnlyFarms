using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Models.Decorators {
    public class Barometer : StationDecorator {
        int? airPressure;
        public Barometer(StationPrototype decoratedStation) : base(decoratedStation) { }
        public Barometer(Barometer stationToManipulate, bool wantsToClone) : base(stationToManipulate, wantsToClone) {
            if (wantsToClone)
                this.airPressure = stationToManipulate.airPressure;
        }
        public override void UpdateWeather() {
            Random rnd = new Random();
            if (airPressure == null) {
                airPressure = rnd.Next(900, 1100);
            }
            else {
                airPressure = airPressure + rnd.Next(-10, 10);
            }
            decoratedStation.UpdateWeather();
        }
        public override string GetWeather() {
            if (airPressure != null)
                return decoratedStation.GetWeather() + " barometer;" + this.airPressure.ToString();
            return decoratedStation.GetWeather() + " barometer;unmeasured";
        }
        public override StationPrototype Clone() {
            return new Barometer(this, true);
        }

    }
}
