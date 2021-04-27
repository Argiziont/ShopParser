using System.Collections.Generic;
using Newtonsoft.Json;

namespace ShopParserApi.Models
{
    public class Category
    {
        public int Id { get; set; }
        [JsonProperty("caption")] public string Name { get; set; }
        [JsonProperty("url")]  public string Url { get; set; }

        public ICollection<ProductData> Products { get; set; } = new List<ProductData>();
        public Category SupCategory { get; set; }
    }
}