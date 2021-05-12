using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Diffing.Extensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema.Generation;
using ShopParserApi.Models;
using ShopParserApi.Models.Helpers;
using ShopParserApi.Models.Json_DTO;
using ShopParserApi.Services.Helpers;
using ShopParserApi.Services.Interfaces;

namespace ShopParserApi.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IBrowsingContextService _browsingContextService;
        private readonly ApplicationDb _dbContext;
        private readonly ILogger<CompanyService> _logger;
        private IBackgroundTaskQueue<ProductData> TaskQueue { get; }
        public CompanyService(ApplicationDb dbContext, IBrowsingContextService browsingContextService, IBackgroundTaskQueue<ProductData> taskQueue, ILogger<CompanyService> logger)
        {
            _dbContext = dbContext;
            _browsingContextService = browsingContextService;
            TaskQueue = taskQueue;
            _logger = logger;
        }

        public async Task<CompanyData> InsertCompanyIntoDb(CompanyData company)
        {
            //Number of pages   
            company.CompanyState = CompanyState.Processing;

            await _dbContext.SaveChangesAsync();

            var counter = 1;

            var page = await _browsingContextService.OpenPageAsync(company.Url.Replace(".html", $";{counter}.html"));

            //Get all pages for current company
            while (page.Url != company.Url || counter==1)
            {
                var products = CompanyParsingService.ParseCompanyProducts(company, page).ToArray();
                company.Products.AddRange(products);
                await TaskQueue.QueueBackgroundWorkItemsRangeAsync(products);

                _logger.LogInformation("CompanyService new products chunk was added");

                counter++;
                Thread.Sleep(5000);

                page = await _browsingContextService.OpenPageAsync(company.Url.Replace(".html", $";{counter}.html"));

                if (page != null) continue;
                company.CompanyState = CompanyState.Failed;
                await _dbContext.SaveChangesAsync();
                return company;
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