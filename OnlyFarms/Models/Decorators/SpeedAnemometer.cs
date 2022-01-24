using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Models.Decorators {
    public class SpeedAnemometer : StationDecorator {
        int? windSpeed;
        public SpeedAnemometer(StationPrototype decoratedStation) : base(decoratedStation) { }
        public SpeedAnemometer(StationDecorator stationToManipulate, bool wantsToClone) : base(stationToManipulate, wantsToClone) { }
        public override void UpdateWeather() {
            Random rnd = new Random();
            if (windSpeed == null) {
                windSpeed = rnd.Next(0, 200);
            }
            else {
                if (windSpeed > 5)
                    windSpeed = windSpeed + rnd.Next(-5, 5);
                else
                    windSpeed = windSpeed + rnd.Next(0, 5);
            }
            decoratedStation.UpdateWeather();
        }
        public override string GetWeather() {
            if (windSpeed != null)
                return decoratedStation.GetWeather() + " speedAnemometer;" + this.windSpeed.ToString();
            return decoratedStation.GetWeather() + " speedAnemometer;unmeasured";
        }
        public override StationPrototype Clone() {
            return new SpeedAnemometer(this, true);
        }
    }
}
