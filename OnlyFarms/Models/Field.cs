using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Models
{
    public class Field
    {
        public int ID { get; set; }
        public string Tag { get; set; }
        public string Location { get; set; }
        public string Street { get; set; }
        public int FieldSurface { get; set; }
    }
}
