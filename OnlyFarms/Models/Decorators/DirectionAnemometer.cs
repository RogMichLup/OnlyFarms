using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Models.Decorators {
    public class DirectionAnemometer : StationDecorator {
        string windDirection;
        public DirectionAnemometer(StationPrototype decoratedStation) : base(decoratedStation) { }
        public DirectionAnemometer(DirectionAnemometer stationToManipulate, bool wantsToClone) : base(stationToManipulate, wantsToClone) {
            if (wantsToClone)
                this.windDirection = stationToManipulate.windDirection;
        }
        public override void UpdateWeather() {
            Random rnd = new Random();
            int i = rnd.Next(0, 3);
            switch (i) {
                case 0:
                    windDirection = "E";
                    break;
                case 1:
                    windDirection = "W";
                    break;
                case 2:
                    windDirection = "N";
                    break;
                case 3:
                    windDirection = "S";
                    break;
            }
            decoratedStation.UpdateWeather();
        }
        public override string GetWeather() {
            if (windDirection.Length>0)
                return decoratedStation.GetWeather() + " directionAnemometer;" + this.windDirection.ToString();
            return decoratedStation.GetWeather() + " directionAnemometer;unmeasured";
        }
        public override StationPrototype Clone() {
            return new DirectionAnemometer(this, true);
        }

    }
}
