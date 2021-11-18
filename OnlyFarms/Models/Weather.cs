using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Models
{
    public class Weather
    {
        public int Id { get; set; }
        public int Temperature { get; set; }
        public int Moisture { get; set; }
        public int AirPressure { get; set; }
        public int RainfallAmount { get; set; }
        public string WindDirection { get; set; }
        public int WindSpeed { get; set; }
        public DateTime Date { get; set; }
        public Field Field { get; set; }
    }
}
