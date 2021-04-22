using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema.Generation;
using ShopParserApi.Models;
using ShopParserApi.Models.Helpers;
using ShopParserApi.Models.Hubs;
using ShopParserApi.Models.Hubs.Clients;
using ShopParserApi.Models.Json_DTO;

namespace ShopParserApi.Service.Helpers
{
    public class ShopService
    {
        public static async Task AddProductsFromSellerPageToDb(ShopData shop, IDocument sellerPage,
            ApplicationDb dbContext, IHubContext<ApiHub, IApiClient> shopHub)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            //Number of pages   
            var pageCount = sellerPage.QuerySelectorAll("button[data-qaid='pages']")
                .Select(m => int.Parse(m.InnerHtml))
                .Max();
            shop.ShopState = ShopState.Processing;
            
            await dbContext.SaveChangesAsync();
            //Get all pages for current seller
            for (var i = 1; i <= pageCount; i++)
            {
                var page = await context.OpenAsync(shop.Url.Replace(".html", "") + ";" + i + ".html");

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

                var currentParsingPercent = (int) (i / (double) pageCount * 100);
                if (currentParsingPercent % 10 == 0 && currentParsingPercent != 0)
                    await shopHub.Clients.All.ReceiveMessage(
                        $"Currently parsing shop with name \"{shop.Name}\" \n Already done \"{currentParsingPercent}%\" pages");

                Thread.Sleep(2000);
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