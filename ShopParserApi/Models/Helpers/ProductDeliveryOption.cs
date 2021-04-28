using Newtonsoft.Json;

namespace ShopParserApi.Models.Helpers
{
    public class ProductDeliveryOption
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        [JsonProperty("id")] public int ExternalId { get; set; }

        [JsonProperty("name")] public string OptionName { get; set; }
        [JsonProperty("comment")] public string OptionsComment { get; set; }

        public ProductData Product { get; set; }
    }
}