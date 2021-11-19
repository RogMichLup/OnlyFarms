using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlyFarms.Models
{
    public class Field
    {
        public int ID { get; set; }

        public string Tag { get; set; }

        [Required]
        public string Location { get; set; }

        public string Street { get; set; }

        [Required]
        public int FieldSurface { get; set; }
    }
}
