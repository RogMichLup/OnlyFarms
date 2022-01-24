using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Models.Decorators {
    public class RainGauge : StationDecorator {
        int? rainfallAmount;
        public RainGauge(StationPrototype decoratedStation) : base(decoratedStation) { }
        public RainGauge(RainGauge stationToManipulate, bool wantsToClone) : base(stationToManipulate, wantsToClone) {
            if (wantsToClone)
                this.rainfallAmount = stationToManipulate.rainfallAmount;
        }
        public override void UpdateWeather() {
            Random rnd = new Random();
            if (rainfallAmount == null) {
                rainfallAmount = rnd.Next(0, 1825);
            }
            else {
                if (rainfallAmount > 20)
                    rainfallAmount = rainfallAmount + rnd.Next(-20, 20);
                else
                    rainfallAmount = rainfallAmount + rnd.Next(0, 20);
            }
            decoratedStation.UpdateWeather();
        }
        public override string GetWeather() {
            if (rainfallAmount != null)
                return decoratedStation.GetWeather() + " rainGauge;" + this.rainfallAmount.ToString();
            return decoratedStation.GetWeather() + " rainGauge;unmeasured";
        }
        public override StationPrototype Clone() {
            return new RainGauge(this, true);
        }
    }
}
