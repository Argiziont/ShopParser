using System;
using System.Collections.Generic;
using ShopParserApi.Models.Helpers;

namespace ShopParserApi.Models
{
    public class ProductData
    {
        //Ids
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public string ExternalId { get; set; }


        //Internal data
        public string Title { get; set; }
        public string Url { get; set; }
        public DateTime SyncDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public ProductState ProductState { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string KeyWords { get; set; }


        //Json-serialized data
        public string JsonData { get; set; }
        public string JsonDataSchema { get; set; }


        //Data base connections
        public CompanyData Company { get; set; }
        public PresenceData Presence { get; set; }
        public ICollection<CategoryData> Categories { get; set; } = new List<CategoryData>();
        public ICollection<ProductPaymentOption> ProductPaymentOptions { get; set; } = new List<ProductPaymentOption>();

        public ICollection<ProductDeliveryOption> ProductDeliveryOptions { get; set; } =
            new List<ProductDeliveryOption>();

        public ICollection<ProductAttribute> ProductAttribute { get; set; } = new List<ProductAttribute>();
    }
}