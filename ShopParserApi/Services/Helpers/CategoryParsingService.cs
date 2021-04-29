using System.Collections.Generic;
using AngleSharp;
using System.Linq;
using AngleSharp.Dom;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShopParserApi.Models;
using ShopParserApi.Services.Extensions;

namespace ShopParserApi.Services.Helpers
{
    public static class CategoryParsingService
    {
        public static IEnumerable<Category> ParseCategories(ProductData product, IDocument productPage)
        {
            var externalId = product.Url
                .Split("/").Last().Split('-').First().Replace("p", "");

            var jsonString = productPage.ToHtml().SubstringJson("window.ApolloCacheState =", "window.SPAConfig");
            var productJson = JObject.Parse(jsonString);

            var productBreadCrumbsObjectsList =
                productJson[$"$Product:{externalId}.breadCrumbs"]?["items"]?.Select(a => a["id"].ToString());
            var categoryList = new List<Category>();
            if (productBreadCrumbsObjectsList == null) return new List<Category>();
            Category higherLevelCategory = null;
            foreach (var productBreadCrumbsObject in productBreadCrumbsObjectsList)
            {
                var categoryObject = productJson[productBreadCrumbsObject]?.ToString();
                if (categoryObject == null) continue;
                var category = JsonConvert.DeserializeObject<Category>(categoryObject);
                category.SupCategory = higherLevelCategory;
                higherLevelCategory = category;
                categoryList.Add(category);
            }

            categoryList.Remove(categoryList.Last());
            return categoryList;
        }
    }
}
