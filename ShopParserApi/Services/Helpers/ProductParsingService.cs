using AngleSharp;
using AngleSharp.Diffing.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NJsonSchema;
using ShopParserApi.Models;
using ShopParserApi.Models.Helpers;
using ShopParserApi.Models.Json_DTO;
using ShopParserApi.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;

namespace ShopParserApi.Services.Helpers
{
    public static class ProductParsingService
    {
        public static ProductData ParseSinglePage(ProductData product,IDocument productPage)
        {

            var externalId = product.Url.SplitProductUrl();

            var jsonString = productPage.ToHtml().SubstringJson("window.ApolloCacheState =", "window.SPAConfig");
            var json = JObject.Parse(jsonString);

            return ParseSinglePage(json, externalId);

         }

        private static ProductData ParseSinglePage(JObject productJson, string externalId)
        {
            var product = productJson[$"Product:{externalId}"];

            if (product == null)
                throw new NullReferenceException(nameof(product));

            var productFromProm = JsonConvert.DeserializeObject<ProductJson>(product.ToString());

            //Parsing images 
            var images =
                JsonConvert.DeserializeObject<string[]>(
                    product["images({\"height\":640,\"width\":640})"]?["json"]?.ToString() ?? "[]");
            productFromProm.ImageUrls = images.ToList();

            var productPresence = productJson[$"$Product:{externalId}.presence"];

            if (productPresence != null)
                productFromProm.Presence = JsonConvert.DeserializeObject<PresenceData>(productPresence.ToString());

            //Parsing Attributes 
            var attributesObjectsList = product["attributes"]?.Select(a => a["id"].ToString());
            if (attributesObjectsList != null)
            {
                productFromProm.ProductAttribute = new List<ProductAttribute>();
                foreach (var attributeObject in attributesObjectsList)
                {
                    var attributeRawObject = productJson[attributeObject]?.ToString();
                    if (attributeRawObject == null)
                        break;

                    var attribute =
                        JsonConvert.DeserializeObject<ProductAttribute>(attributeRawObject);

                    var values = "";
                    var valuePathList = productJson[attributeObject]?["values"]?.Select(a => a["id"].ToString());
                    if (valuePathList != null)
                        values = valuePathList.Aggregate(values,
                            (current, valuePath) => current + $"{productJson[valuePath]?["value"]},");

                    values.Remove(values.Length - 2); //Deleting last comma

                    attribute.AttributeValues = values;

                    productFromProm.ProductAttribute.Add(attribute);
                }
            }

            //Parsing delivery options
            var productDeliveryOptionsObjectsList = product["deliveryOptions"]?.Select(a => a["id"].ToString());
            if (productDeliveryOptionsObjectsList != null)
            {
                productFromProm.ProductDeliveryOptions = new List<ProductDeliveryOption>();
                productFromProm.ProductDeliveryOptions.AddRange(productDeliveryOptionsObjectsList
                    .Select(productDeliveryOptionsObject => productJson[productDeliveryOptionsObject]?.ToString())
                    .TakeWhile(productDeliveryOptionsRawObject => productDeliveryOptionsRawObject != null)
                    .Select(JsonConvert.DeserializeObject<ProductDeliveryOption>));
            }

            //Parsing payment options
            var productPaymentOptionsObjectsList = product["paymentOptions"]?.Select(a => a["id"].ToString());
            if (productPaymentOptionsObjectsList != null)
            {
                productFromProm.ProductPaymentOptions = new List<ProductPaymentOption>();
                productFromProm.ProductPaymentOptions.AddRange(productPaymentOptionsObjectsList
                    .Select(productPaymentOptionsObject => productJson[productPaymentOptionsObject]?.ToString())
                    .TakeWhile(productPaymentOptionsRawObject => productPaymentOptionsRawObject != null)
                    .Select(JsonConvert.DeserializeObject<ProductPaymentOption>));
            }

           
            productFromProm.JsonCategorySchema = JsonSchema.FromType<Category>().ToJson();
            productFromProm.SyncDate = DateTime.Now;

            var productSchema = JsonSchema.FromType<ProductJson>().ToJson();
            var serializedProduct = JsonConvert.SerializeObject(productFromProm);
            return new ProductData
            {
                SyncDate = productFromProm.SyncDate,
                Url = productFromProm.Url,
                Description = productFromProm.Description,
                ExternalId = productFromProm.ExternalId,
                JsonData = serializedProduct,
                JsonDataSchema = productSchema,
                Price = productFromProm.Price,
                Title = productFromProm.Title,
                ProductState = ProductState.Success,
                ProductAttribute = productFromProm.ProductAttribute,
                KeyWords = productFromProm.KeyWords,
                Presence = productFromProm.Presence,
                ProductPaymentOptions = productFromProm.ProductPaymentOptions,
                ProductDeliveryOptions = productFromProm.ProductDeliveryOptions,
            };
        }

       
    }
}