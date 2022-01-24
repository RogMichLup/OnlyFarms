using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Models {
    public abstract class StationDecorator : StationPrototype {
        protected StationPrototype decoratedStation;
        public StationDecorator(StationPrototype decoratedStation) {
            this.decoratedStation = decoratedStation;
        }
        public StationDecorator(StationDecorator stationToManipulate, bool wantsToClone) {
            if(wantsToClone == true) {
                this.decoratedStation = stationToManipulate.decoratedStation.Clone();
            }
            else {
                this.decoratedStation = stationToManipulate;
            }
        }

        public virtual string GetWeather() {
            return " decorator?";
        }

        public virtual void UpdateWeather() {

        }
        public int GetFieldID() {
            return decoratedStation.GetFieldID();
        }

        public virtual StationPrototype Clone() {
            throw new Exception();
            return this;
        }
    }
}
