using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrjModule25_Parser.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Href { get; set; }
        
        public Category SubCategory { get; set; }
    }
}
