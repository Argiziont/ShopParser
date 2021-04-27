using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using Newtonsoft.Json;
using NJsonSchema;
using ShopParserApi.Models;
using ShopParserApi.Models.Helpers;
using ShopParserApi.Models.Json_DTO;
using ShopParserApi.Service.Exceptions;
using Newtonsoft.Json.Linq;
using ShopParserApi.Service.Extensions;

namespace ShopParserApi.Service.Helpers
{
    public static class ProductService
    {
        public static async Task ParseSinglePageAndInsertToDb(IDocument page, string productUrl,
            ApplicationDb dbContext)
        {
            var currentProduct = dbContext.Products.FirstOrDefault(s => s.Url == productUrl);

            if (page.StatusCode == HttpStatusCode.TooManyRequests)
                throw new TooManyRequestsException();

            if (currentProduct != null)
            {
                var parsedProduct = await ParseSinglePage(page, currentProduct.Url, dbContext);

                if (parsedProduct != null)
                {
                    currentProduct.ProductState = parsedProduct.ProductState;
                    currentProduct.Description = parsedProduct.Description;
                    currentProduct.ExpirationDate = parsedProduct.ExpirationDate;
                    currentProduct.JsonData = parsedProduct.JsonData;
                    currentProduct.ExternalId = parsedProduct.ExternalId;
                    currentProduct.JsonDataSchema = parsedProduct.JsonDataSchema;
                    currentProduct.Price = parsedProduct.Price;
                    currentProduct.SyncDate = parsedProduct.SyncDate;
                    currentProduct.Title = parsedProduct.Title;
                    currentProduct.Url = parsedProduct.Url;
                    currentProduct.KeyWords = parsedProduct.KeyWords;
                    currentProduct.ProductAttribute = parsedProduct.ProductAttribute;
                }

                if (parsedProduct?.Categories != null)
                    foreach (var currentCategory in parsedProduct.Categories)
                        currentProduct.Categories.Add(
                            dbContext.Categories.FirstOrDefault(c => c.Name == currentCategory.Name));

                if (currentProduct?.ProductAttribute.Count > 0)
                    foreach (var attribute in currentProduct.ProductAttribute)
                        attribute.Product = currentProduct;
                await dbContext.AddRangeAsync();
            }

            await dbContext.SaveChangesAsync();
        }

        public static async Task<ProductData> ParseSinglePage(IDocument page, string productUrl,
            ApplicationDb dbContext)
        {
            //External id from url
            var externalId = productUrl
                .Split("/").Last().Split('-').First().Replace("p", "");
            
            var jsonString = page.ToHtml().SubstringJson("window.ApolloCacheState =", "window.SPAConfig");

            var json=JObject.Parse(jsonString);

            var product = json[$"Product:{externalId}"];
            if (product == null)
                throw new NullReferenceException(nameof(product));
            
            var productFromProm = JsonConvert.DeserializeObject<ProductJson>(product.ToString());

            var images = JsonConvert.DeserializeObject<string[]>(product["images({\"height\":640,\"width\":640})"]?["json"]?.ToString()??"[]");//Getting images

            productFromProm.ImageUrls = images.ToList();

            var productPresence = json[$"$Product:{externalId}.presence"];
            

            if (productPresence!=null)
                productFromProm.Presence = JsonConvert.DeserializeObject<PresenceData>(productPresence.ToString());

           


            var attributesObjectsList = product["attributes"]?.Select(a=> a["id"].ToString());
            if (attributesObjectsList != null)
            {
                productFromProm.ProductAttribute =  new List<ProductAttribute>();
                foreach (var attributeObject in attributesObjectsList)
                {
                    var attributeRawObject = json[attributeObject]?.ToString();
                    if (attributeRawObject == null)
                        break;

                    var attribute =
                        JsonConvert.DeserializeObject<ProductAttribute>(attributeRawObject);

                    var values = "";
                    var valuePathList = json[attributeObject]?["values"]?.Select(a => a["id"].ToString());
                    if (valuePathList != null)
                        values = valuePathList.Aggregate(values, (current, valuePath) => current + $"{json[valuePath]?["value"]},");

                    values.Remove(values.Length - 2);//Deleting last comma

                    attribute.AttributeValues = values;

                    productFromProm.ProductAttribute.Add(attribute);
                }
            }

            var productBreadCrumbsObjectsList = json[$"$Product:{externalId}.breadCrumbs"]?["items"]?.Select(a => a["id"].ToString());

            //var fullCategory = UnScrubCategory(breadcrumbsSeo);
            var categoryList = new List<Category>();
            if (productBreadCrumbsObjectsList != null)
            {
                Category higherLevelCategory = null;
                foreach (var productBreadCrumbsObject in productBreadCrumbsObjectsList)
                {
                    var categoryObject = json[productBreadCrumbsObject]?.ToString();
                    if (categoryObject == null) continue;
                    var category = JsonConvert.DeserializeObject<Category>(categoryObject);
                    category.SupCategory = higherLevelCategory;
                    higherLevelCategory = category;
                    categoryList.Add(category);

                }
                categoryList.Remove(categoryList.Last());


                foreach (var category in categoryList.Where(category =>
                    dbContext.Categories.FirstOrDefault(cat => cat.Name == category.Name) == null))
                {
                    if (category.SupCategory?.SupCategory != null)
                        category.SupCategory.SupCategory = null;

                    await dbContext.Categories.AddAsync(new Category
                    {
                        Url = category.Url,
                        Name = category.Name,
                        SupCategory = category.SupCategory == null
                            ? null
                            : dbContext.Categories.FirstOrDefault(c => c.Name == category.SupCategory.Name)
                    });

                    await dbContext.SaveChangesAsync();
                }

            }

            productFromProm.StringCategory = CategoryToString(categoryList);
            productFromProm.JsonCategory = JsonConvert.SerializeObject(categoryList);
            productFromProm.JsonCategorySchema = JsonSchema.FromType<Category>().ToJson();
            productFromProm.SyncDate= DateTime.Now;

            var productSchema = JsonSchema.FromType<ProductJson>().ToJson();
            var productJson = JsonConvert.SerializeObject(productFromProm);
            return new ProductData
            {
                SyncDate = productFromProm.SyncDate,
                Url = productFromProm.Url,
                Description = productFromProm.Description,
                ExternalId = productFromProm.ExternalId,
                JsonData = productJson,
                JsonDataSchema = productSchema,
                Price = productFromProm.Price,
                Title = productFromProm.Title,
                ProductState = ProductState.Success,
                Categories = categoryList,
                ProductAttribute = productFromProm.ProductAttribute,
                KeyWords = productFromProm.KeyWords
            };
        }

        private static string CategoryToString(IEnumerable<Category> categories)
        {
            var categoryString = categories.Aggregate("", (current, category) => current + category.Name + " > ");
            return categoryString.Remove(categoryString.Length - 3);
        }

       
    }
}