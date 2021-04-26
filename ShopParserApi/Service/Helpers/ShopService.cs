using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema.Generation;
using ShopParserApi.Models;
using ShopParserApi.Models.Helpers;
using ShopParserApi.Models.Json_DTO;

namespace ShopParserApi.Service.Helpers
{
    public static class ShopService
    {
        public static async Task AddProductsFromSellerPageToDb(ShopData shop, IDocument sellerPage,
            ApplicationDb dbContext)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            //Number of pages   
            shop.ShopState = ShopState.Processing;
            
            await dbContext.SaveChangesAsync();

            var counter = 1;
            
            var page = await context.OpenAsync(shop.Url.Replace(".html", $";{counter}.html"));
            
            //Get all pages for current seller
            while (page.Url.Contains(';'))
            {
                page = await context.OpenAsync(shop.Url.Replace(".html", $";{counter}.html"));

                var linksSublistPage = page.QuerySelectorAll("*[data-qaid='product_link']").ToList()
                    .Cast<IHtmlAnchorElement>()
                    .Select(u => u.Href).ToList();

                var emptyProducts = linksSublistPage.Select(url => new ProductData
                {
                    Url = url,
                    Shop = shop,
                    ProductState = ProductState.Idle
                }).ToList();

                foreach (var emptyProduct in emptyProducts) shop.Products.Add(emptyProduct);

                await dbContext.Products.AddRangeAsync(emptyProducts);
                
                counter++;
                Thread.Sleep(5000);
            }

            shop.ShopState = ShopState.Success;
            shop.SyncDate = DateTime.Now;

            var jsonSellerDat = new ShopJson
            {
                ExternalId = shop.ExternalId,
                Url = shop.Url,
                SyncDate = shop.SyncDate,
                Name = shop.Name
            };

            var generator = new JSchemaGenerator();
            var fullCategorySchema = generator.Generate(typeof(ShopJson)).ToString();
            var fullCategoryJson = JsonConvert.SerializeObject(jsonSellerDat);


            shop.JsonData = fullCategoryJson;
            shop.JsonDataSchema = fullCategorySchema;
        }
    }
}