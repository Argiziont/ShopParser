using System;

namespace PrjModule25_Parser.Models.ResponseModels
{
    public class ResponseShop
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public int ProductCount { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime SyncDate { get; set; }
    }
}