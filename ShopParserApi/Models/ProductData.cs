using System;
using System.Collections;
using System.Collections.Generic;
using PrjModule25_Parser.Models.Helpers;

namespace PrjModule25_Parser.Models
{
    public class ProductData
    {
        //Ids
        public int Id { get; set; }
        public int? ShopId { get; set; }
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
        public ShopData Shop { get; set; }
        public ICollection<Category> Categories { get; set; } = new List<Category>();
        public ICollection<ProductAttribute> ProductAttribute { get; set; } = new List<ProductAttribute>();
    }
}