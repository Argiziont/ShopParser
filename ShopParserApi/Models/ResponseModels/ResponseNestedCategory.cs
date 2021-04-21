using System.Collections.Generic;

namespace ShopParserApi.Models.ResponseModels
{
    public class ResponseNestedCategory : ResponseCategory
    {
        public List<ResponseNestedCategory> SubCategories { get; set; }
    }
}