using System;

namespace ShopParserApi.Models.ResponseModels
{
    public class ResponseCompany
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public int ProductCount { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime SyncDate { get; set; }
    }
}