using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ShopParserApi.Models.Helpers;

namespace ShopParserApi.Models.Json_DTO
{
    public class ProductJson
    {
        //Internal data
        [JsonProperty("nameForCatalog")]public string Title { get; set; }
        [JsonProperty("id")] public string ExternalId { get; set; }
        [JsonProperty("urlForProductCatalog")] public string Url { get; set; }
        [JsonProperty("priceCurrency")] public string Currency { get; set; }
        [JsonProperty("keywords")] public string KeyWords { get; set; }
        [JsonProperty("descriptionPlain")] public string Description { get; set; }
        [JsonProperty("discountedPrice")] public string Price { get; set; }
        [JsonProperty("priceUSD")] public string PriceUsd { get; set; }
        [JsonProperty("sku")] public string ScuCode { get; set; }
        [JsonProperty("price")] public string FullPrice { get; set; }
        public PresenceData Presence { get; set; }
        public string OptPrice { get; set; }
        public List<string> ImageUrls { get; set; }
        public DateTime SyncDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string JsonCategory { get; set; }
        public string StringCategory { get; set; }
        public string JsonCategorySchema { get; set; }
        public ICollection<ProductAttribute> ProductAttribute { get; set; }
    }
}