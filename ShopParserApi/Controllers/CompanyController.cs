using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NJsonSchema;
using ShopParserApi.Models;
using ShopParserApi.Models.Helpers;
using ShopParserApi.Models.Json_DTO;
using ShopParserApi.Models.ResponseModels;
using ShopParserApi.Services;
using ShopParserApi.Services.Extensions;
using ShopParserApi.Services.Helpers;

namespace ShopParserApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IBrowsingContext _context;
        private readonly ApplicationDb _dbContext;

        public CompanyController(ApplicationDb db)
        {
            var config = Configuration.Default.WithDefaultLoader().WithJs();
            _context = BrowsingContext.New(config);
            _dbContext = db;
        }

        [HttpPost]
        [Route("ParseCompanyPageProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AddProductsListFromCompanyAsync(string companyName)
        {
            try
            {
                var company = _dbContext.Companies.FirstOrDefault(s => s.Name == companyName);
                if (company == null) return BadRequest("Database doesn't contains this company");

                var companyUrl = _dbContext.Companies.FirstOrDefault(u => u.Name == companyName)?.Url;
                var companyPage = await _context.OpenAsync(companyUrl);

                await CompanyService.AddProductsFromCompanyPageToDb(company, companyPage, _dbContext);


                _dbContext.Entry(company).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();

                return Ok(company.Products);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        [Route("AddCompanyByUrl")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseCompany), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AddCompanyByUrlAsync(
            string companyUrl)
        {
            try
            {
                var companyPage = await _context.OpenAsync(companyUrl);

                var externalId = companyUrl.Split("/").Last().Split('-').First().Replace("c", "");

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
        [Route("GetCompanyById")]
        [ProducesResponseType(typeof(ResponseCompany), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult GetCompanyById(int id)
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
        [Route("GetCompanies")]
        [ProducesResponseType(typeof(IEnumerable<ResponseCompany>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult GetCompanies()
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