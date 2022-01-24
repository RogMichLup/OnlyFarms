using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Models {
    public interface StationPrototype {
        public StationPrototype SendUpdate() {
            return this;
        }
        public StationPrototype Clone();
        public string GetWeather();
        public void UpdateWeather();

        public int GetFieldID();

    }
}
