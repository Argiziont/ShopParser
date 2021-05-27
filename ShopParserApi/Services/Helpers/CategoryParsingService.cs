using System.Collections.Generic;
using System.Linq;
using AngleSharp;
using AngleSharp.Dom;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShopParserApi.Models;
using ShopParserApi.Services.Extensions;

namespace ShopParserApi.Services.Helpers
{
    public static class CategoryParsingService
    {
        public static IEnumerable<CategoryData> ParseCategories(ProductData product, IDocument productPage)
        {
            var externalId = product.Url
                .Split("/").Last().Split('-').First().Replace("p", "");

            var jsonString = productPage.ToHtml().SubstringJson("window.ApolloCacheState =", "window.SPAConfig");
            var productJson = JObject.Parse(jsonString);

            var productBreadCrumbsObjectsList =
                productJson[$"$Product:{externalId}.breadCrumbs"]?["items"]?.Select(a => a["id"].ToString());
            var categoryList = new List<CategoryData>();
            if (productBreadCrumbsObjectsList == null) return new List<CategoryData>();
            CategoryData higherLevelCategoryData = null;
            foreach (var productBreadCrumbsObject in productBreadCrumbsObjectsList)
            {
                var categoryObject = productJson[productBreadCrumbsObject]?.ToString();
                if (categoryObject == null) continue;
                var category = JsonConvert.DeserializeObject<CategoryData>(categoryObject);
                category.SupCategoryData = higherLevelCategoryData;
                higherLevelCategoryData = category;
                categoryList.Add(category);
            }

            categoryList.Remove(categoryList.Last());
            return categoryList;
        }
    }
}