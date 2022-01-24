using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Models.Decorators {
    public class Thermometer : StationDecorator {
        double? temperature;
        public Thermometer(StationPrototype decoratedStation) : base(decoratedStation) { }
        public Thermometer(StationDecorator stationToManipulate, bool wantsToClone) : base(stationToManipulate, wantsToClone) { }
        public override void UpdateWeather() {
            Random rnd = new Random();
            if (temperature == null) {
                temperature = rnd.Next(-20, 40);
                temperature += rnd.Next(0, 10) / 10;
            }
            else {
                temperature = temperature + rnd.Next(-5, 5);
                temperature += rnd.Next(0, 10) / 10;
            }
            decoratedStation.UpdateWeather();
        }
        public override string GetWeather() {
            if(temperature!=null)
                return decoratedStation.GetWeather() + " thermometer;" + this.temperature.ToString();
            return decoratedStation.GetWeather() + " thermometer;unmeasured";
        }
        public override StationPrototype Clone() {
            return new Thermometer(this, true);
        }
    }
}
