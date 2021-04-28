using System;
using System.Collections.Generic;
using ShopParserApi.Models.Helpers;

namespace ShopParserApi.Models
{
    public class CompanyData
    {
        //Ids
        public int Id { get; set; }
        public int? SourceId { get; set; }
        public string ExternalId { get; set; }

        //Internal data
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime SyncDate { get; set; }
        public CompanyState CompanyState { get; set; }

        //Json-serialized data
        public string JsonData { get; set; }
        public string JsonDataSchema { get; set; }

        //Data base connections
        public ICollection<ProductData> Products { get; set; } = new List<ProductData>();
        public CompanySource Source { get; set; }
    }
}