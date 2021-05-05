using System;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Diffing.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema.Generation;
using ShopParserApi.Models;
using ShopParserApi.Models.Helpers;
using ShopParserApi.Models.Json_DTO;
using ShopParserApi.Services.Helpers;
using ShopParserApi.Services.Interfaces;

namespace ShopParserApi.Services
{
    public class CompanyService:ICompanyService
    {
        private readonly ApplicationDb _dbContext;
        private readonly IBrowsingContext _browsingContext;
        public CompanyService(ApplicationDb dbContext)
        {
            _dbContext = dbContext;
            var config = Configuration.Default.WithDefaultLoader().WithJs().WithCss();
            _browsingContext = BrowsingContext.New(config);
        }
        public async Task<CompanyData> InsertCompanyIntoDb(CompanyData company)
        {
            //Number of pages   
            company.CompanyState = CompanyState.Processing;

            await _dbContext.SaveChangesAsync();

            var counter = 1;

            var page = await _browsingContext.OpenAsync(company.Url.Replace(".html", $";{counter}.html"));

            //Get all pages for current company
            while (page.Url.Contains(';'))
            {
                page = await _browsingContext.OpenAsync(company.Url.Replace(".html", $";{counter}.html"));

                company.Products.AddRange(CompanyParsingService.ParseCompanyProducts(company, page));

                //await _dbContext.Products.AddRangeAsync(emptyProducts);

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

            await _dbContext.SaveChangesAsync();

            return company;
        }
    }
}