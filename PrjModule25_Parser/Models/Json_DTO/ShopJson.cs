using System;
using System.Collections.Generic;
using PrjModule25_Parser.Models.Helpers;

namespace PrjModule25_Parser.Models.JSON_DTO
{
    public class ShopJson
    {
        //Internal data
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime SyncDate { get; set; }
    }
}