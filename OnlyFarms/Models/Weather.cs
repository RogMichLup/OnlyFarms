using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Models {
    public class Weather : StationPrototype {
        public int fieldID;
        private DateTime date;
        private DateTime time;
        public Weather(int fieldIndex) {
            fieldID = fieldIndex;
        }
        public Weather(Weather weatherToClone) {
            this.fieldID = weatherToClone.fieldID;
            this.date = weatherToClone.date;
            this.time = weatherToClone.time;
        }
        public string GetWeather() {
            UpdateWeather();
            string timeString = time.ToString();
            timeString = timeString.Split(" ").Last();
            string dateString = date.ToString();
            dateString = dateString.Split(" ").First();
            return " timestamp;"+dateString+";"+timeString;
        }

        public void UpdateWeather() {
            date = DateTime.Now.Date;
            time = DateTime.MinValue;
            time = time.AddHours(DateTime.Now.Hour);
            time = time.AddMinutes(DateTime.Now.Minute);
        }
        public int GetFieldID() {
            return fieldID;
        }

        public StationPrototype Clone() {
            return new Weather(this);
        }
    }
}
