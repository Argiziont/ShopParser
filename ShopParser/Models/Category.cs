using System.Collections.Generic;

namespace PrjModule25_Parser.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Href { get; set; }

        public ICollection<ProductData> Products { get; set; } = new List<ProductData>();
        public Category SupCategory { get; set; }
    }
}