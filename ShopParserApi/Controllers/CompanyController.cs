using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NJsonSchema;
using ShopParserApi.Models;
using ShopParserApi.Models.Helpers;
using ShopParserApi.Models.Json_DTO;
using ShopParserApi.Models.ResponseModels;
using ShopParserApi.Services;
using ShopParserApi.Services.Extensions;
using ShopParserApi.Services.Interfaces;

namespace ShopParserApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IBrowsingContext _browsingContext;
        private readonly ICompanyService _companyService;
        private readonly ApplicationDb _dbContext;

        public CompanyController(ApplicationDb db, ICompanyService companyService)
        {
            var config = Configuration.Default.WithDefaultLoader().WithJs();
            _browsingContext = BrowsingContext.New(config);
            _dbContext = db;
            _companyService = companyService;
        }

        [HttpPost]
        [Route("ParseCompanyPageProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<ProductData>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ParseCompanyPageProducts(string companyName)
        {
            try
            {
                var company = _dbContext.Companies.FirstOrDefault(s => s.Name == companyName);
                if (company == null) return BadRequest("Database doesn't contains this company");

                await _companyService.InsertCompanyIntoDb(company);

                return Ok(company.Products);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        [Route("AddByUrl")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseCompany), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AddByUrlAsync(
            string companyUrl)
        {
           
            try
            {
                var companyPage = await _browsingContext.OpenPageAsync(companyUrl);

                var externalId = companyUrl.SplitCompanyUrl();

                var jsonString = companyPage.ToHtml().SubstringJson("window.ApolloCacheState =", "window.SPAConfig");
                var json = JObject.Parse(jsonString);

                var companyJsonObject = json[$"Company:{externalId}"]?.ToString();
                if (companyJsonObject == null) throw new NullReferenceException(nameof(companyJsonObject));

                var company = JsonConvert.DeserializeObject<CompanyJson>(companyJsonObject);

                var serializedCompany = JsonConvert.SerializeObject(company);
                var companyScheme = JsonSchema.FromType<CompanyJson>().ToJson();

                var companyData = new CompanyData
                {
                    Url = company.PortalPageUrl,
                    CompanyState = CompanyState.Idle,
                    ExternalId = company.ExternalId,
                    Name = company.Name,
                    Products = new List<ProductData>(),
                    SyncDate = DateTime.Now,
                    JsonData = serializedCompany,
                    JsonDataSchema = companyScheme
                };

                await _dbContext.Companies.AddAsync(companyData);
                await _dbContext.SaveChangesAsync();

                return Ok(new ResponseCompany
                {
                    ExternalId = companyData.ExternalId,
                    Id = companyData.Id,
                    Url = companyData.Url,
                    SyncDate = companyData.SyncDate,
                    Name = companyData.Name
                });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        [Route("GetById")]
        [ProducesResponseType(typeof(ResponseCompany), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult GetById(int id)
        {
            try
            {
                var company = _dbContext.Companies.FirstOrDefault(s => s.Id == id);

                if (company != null)
                    return Ok(new ResponseCompany
                    {
                        ExternalId = company.ExternalId,
                        Id = company.Id,
                        Url = company.Url,
                        SyncDate = company.SyncDate,
                        Name = company.Name,
                        ProductCount = _dbContext.Products.Count(p =>
                            p.CompanyId == company.Id && p.ProductState == ProductState.Success)
                    });
                return NotFound();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(typeof(IEnumerable<ResponseCompany>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult GetAll()
        {
            try
            {
                var company = _dbContext.Companies.ToList();
                if (company.Count > 0)
                    return Ok(company.Select(s => new ResponseCompany
                    {
                        ExternalId = s.ExternalId,
                        Id = s.Id,
                        Url = s.Url,
                        SyncDate = s.SyncDate,
                        Name = s.Name,
                        ProductCount = _dbContext.Products.Count(p =>
                            p.CompanyId == s.Id && p.ProductState == ProductState.Success)
                    }));

                return NotFound();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}