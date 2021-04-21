using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrjModule25_Parser.Models.ResponseModels
{
    public class ResponseNestedCategory:ResponseCategory
    {
        public List<ResponseNestedCategory> SubCategories { get; set; }
    }
}
