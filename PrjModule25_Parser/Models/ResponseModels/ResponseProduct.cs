using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrjModule25_Parser.Models.ResponseModels
{
    public class ResponseProduct
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }

        public string Title { get; set; }
        public string Url { get; set; }
        public DateTime SyncDate { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }

    }
}
