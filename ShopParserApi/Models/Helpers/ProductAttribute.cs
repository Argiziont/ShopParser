using Newtonsoft.Json;

namespace ShopParserApi.Models.Helpers
{
    public class ProductAttribute
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        [JsonProperty("id")] public int ExternalId { get; set; }

        [JsonProperty("name")] public string AttributeName { get; set; }
        [JsonProperty("group")] public string AttributeGroup { get; set; }
        public string AttributeValues { get; set; }

        public ProductData Product { get; set; }
    }
}