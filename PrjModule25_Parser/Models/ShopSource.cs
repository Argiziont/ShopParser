using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrjModule25_Parser.Models
{
    public class ShopSource
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Data base connections
        public ICollection<ShopData> Shops { get; set; }
    }
}
