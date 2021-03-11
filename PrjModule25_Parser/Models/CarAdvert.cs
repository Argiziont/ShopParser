using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrjModule25_Parser.Models
{
    public class CarAdvert
    {
        // [Key] defines a field as primary key
        [Key]
        public int AdvertId { get; set; }

        public string AdvertUrl { get; set; }
        public string MakeAndModel { get; set; }
        public uint Year { get; set; }
        public char Fuel { get; set; }
        public uint Price { get; set; }
    }
}
