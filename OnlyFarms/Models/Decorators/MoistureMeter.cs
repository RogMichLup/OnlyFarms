using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Models.Decorators {
    public class MoistureMeter : StationDecorator {
        int? moisture;
        public MoistureMeter(StationPrototype decoratedStation) : base(decoratedStation) { }
        public MoistureMeter(StationDecorator stationToManipulate, bool wantsToClone) : base(stationToManipulate, wantsToClone) { }
        public override void UpdateWeather(){
            Random rnd = new Random();
            if (moisture == null) {
                moisture = rnd.Next(0, 100);
            }
            else {
                if (moisture > 10)
                    moisture = moisture + rnd.Next(-10, 10);
                else
                    moisture = moisture + rnd.Next(0, 10);
            }
            decoratedStation.UpdateWeather();
        }
        public override string GetWeather() {
            if (moisture != null)
                return decoratedStation.GetWeather() + " moistureMeter;" + this.moisture.ToString();
            return decoratedStation.GetWeather() + " moistureMeter;unmeasured";
        }
        public override StationPrototype Clone() {
            return new MoistureMeter(this, true);
        }

    }
}
