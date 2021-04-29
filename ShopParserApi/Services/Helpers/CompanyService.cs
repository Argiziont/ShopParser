using System;
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

namespace ShopParserApi.Services.Helpers
{
    public static class CompanyService
    {
        public static async Task AddProductsFromCompanyPageToDb(CompanyData company, IDocument companyPage,
            ApplicationDb dbContext)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            //Number of pages   
            company.CompanyState = CompanyState.Processing;

            await dbContext.SaveChangesAsync();

            var counter = 1;

            var page = await context.OpenAsync(company.Url.Replace(".html", $";{counter}.html"));

            //Get all pages for current company
            while (page.Url.Contains(';'))
            {
                page = await context.OpenAsync(company.Url.Replace(".html", $";{counter}.html"));

                var linksSublistPage = page.QuerySelectorAll("*[data-qaid='product_link']").ToList()
                    .Cast<IHtmlAnchorElement>()
                    .Select(u => u.Href).ToList();

                var emptyProducts = linksSublistPage.Select(url => new ProductData
                {
                    Url = url,
                    Company = company,
                    ProductState = ProductState.Idle
                }).ToList();

                foreach (var emptyProduct in emptyProducts) company.Products.Add(emptyProduct);

                await dbContext.Products.AddRangeAsync(emptyProducts);

                counter++;
                Thread.Sleep(5000);
            }

            company.CompanyState = CompanyState.Success;
            company.SyncDate = DateTime.Now;

            var jsonCompanyDat = new CompanyJson
            {
                ExternalId = company.ExternalId,
                PortalPageUrl = company.Url,
                SyncDate = company.SyncDate,
                Name = company.Name
            };

            var generator = new JSchemaGenerator();
            var fullCategorySchema = generator.Generate(typeof(CompanyJson)).ToString();
            var fullCategoryJson = JsonConvert.SerializeObject(jsonCompanyDat);


            company.JsonData = fullCategoryJson;
            company.JsonDataSchema = fullCategorySchema;
        }
    }
}