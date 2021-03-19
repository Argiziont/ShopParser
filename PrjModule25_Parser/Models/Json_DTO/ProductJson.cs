using System;
using System.Collections.Generic;

namespace PrjModule25_Parser.Models.JSON_DTO
{
    public class ProductJson
    {
        //Internal data
        public string Title { get; set; }
        public string ExternalId { get; set; }
        public string Url { get; set; }
        public DateTime SyncDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string ScuCode { get; set; }
        public string Presence { get; set; }
        public string FullPrice { get; set; }
        public string OptPrice { get; set; }
        public string Currency { get; set; }
        public string FullCurrency { get; set; }
        public string OptCurrency { get; set; }
        public string CompanyName { get; set; }
        public string PositivePercent { get; set; }
        public string RatingsPerLastYear { get; set; }
        public List<string> ImageUrls { get; set; }
        public string JsonCategory { get; set; }
        public string JsonCategorySchema { get; set; }
    }
}