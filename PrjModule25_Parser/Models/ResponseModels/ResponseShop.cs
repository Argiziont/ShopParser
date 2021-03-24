using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrjModule25_Parser.Models.ResponseModels
{
    public class ResponseShop
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }

        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime SyncDate { get; set; }
    }
}
