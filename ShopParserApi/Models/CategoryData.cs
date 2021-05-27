using System.Collections.Generic;
using Newtonsoft.Json;

namespace ShopParserApi.Models
{
    public class CategoryData
    {
        public int Id { get; set; }
        [JsonProperty("caption")] public string Name { get; set; }
        [JsonProperty("url")] public string Url { get; set; }

        public ICollection<ProductData> Products { get; } = new List<ProductData>();
        public CategoryData SupCategoryData { get; set; }
    }
}