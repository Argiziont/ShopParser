using System;
using Newtonsoft.Json;

namespace ShopParserApi.Models.Json_DTO
{
    public class CompanyJson
    {
        //Internal data
        [JsonProperty("id")] public string ExternalId { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("city")] public string City { get; set; }
        [JsonProperty("contactPerson")] public string ContactPerson { get; set; }
        [JsonProperty("contactEmail")] public string ContactEmail { get; set; }
        [JsonProperty("ageYears")] public int? YearsOnPortal { get; set; }
        [JsonProperty("opinionTotalInRating")] public int? TotalInRating { get; set; }

        [JsonProperty("opinionPositivePercent")]
        public int? PositivePercent { get; set; }

        [JsonProperty("opinionTotal")] public int? TotalFeedBack { get; set; }
        [JsonProperty("webSiteUrl")] public string WebSiteUrl { get; set; }
        [JsonProperty("portalPageURL")] public string PortalPageUrl { get; set; }
        public DateTime SyncDate { get; set; }
    }
}