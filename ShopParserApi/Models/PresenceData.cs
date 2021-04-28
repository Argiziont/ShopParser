using Newtonsoft.Json;

namespace ShopParserApi.Models
{
    public class PresenceData
    {
        [JsonProperty("isPresenceSure")] public bool PresenceSureAvailable { get; set; }
        [JsonProperty("isOrderable")] public bool OrderAvailable { get; set; }
        [JsonProperty("isAvailable")] public bool Available { get; set; }
        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("isEnding")] public bool Ending { get; set; }
        [JsonProperty("isWait")] public bool Waiting { get; set; }

        [JsonIgnore] public int Id { get; set; }
        [JsonIgnore] public int ProductId { get; set; }
        [JsonIgnore] public ProductData Product { get; set; }
    }
}