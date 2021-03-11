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

        public string Title { get; set; }
        public string ScuCode { get; set; }
        public string Presence { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string FullPrice { get; set; }
        public string OptPrice { get; set; }
        public string Currency { get; set; }
        public string FullCurrency { get; set; }
        public string OptCurrency { get; set; }
    }
}
