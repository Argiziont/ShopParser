using System.Collections.Generic;

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