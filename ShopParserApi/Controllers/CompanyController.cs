using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NJsonSchema;
using ShopParserApi.Models;
using ShopParserApi.Models.Helpers;
using ShopParserApi.Models.Json_DTO;
using ShopParserApi.Models.ResponseModels;
using ShopParserApi.Services.Extensions;
using ShopParserApi.Services.Interfaces;
using ShopParserApi.Services.Repositories.Interfaces;

namespace ShopParserApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IBrowsingContextService _browsingContextService;
        private readonly ICompanyService _companyService;
        private readonly ILogger<CompanyController> _logger;
        private readonly ICompanyRepository _companyRepository;
        private readonly IProductRepository _productRepository;

        public CompanyController(ICompanyService companyService,
            IBrowsingContextService browsingContextService, ILogger<CompanyController> logger,
            IBackgroundTaskQueue<CompanyData> taskQueue, ICompanyRepository companyRepository, IProductRepository productRepository)
        {
            _companyService = companyService;
            _browsingContextService = browsingContextService;
            _logger = logger;
            _companyRepository = companyRepository;
            _productRepository = productRepository;
            TaskQueue = taskQueue;
        }

        private IBackgroundTaskQueue<CompanyData> TaskQueue { get; }

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
                var company = await _companyRepository.GetByName(companyName);
                if (company == null) return BadRequest("Database doesn't contains this company");

                await _companyService.InsertCompanyIntoDb(company);

                _logger.LogInformation(
                    "ParseCompanyPageProducts method inside CompanyController was called successfully");
                return Ok(company.Products);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
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
                var companyPage = await _browsingContextService.OpenPageAsync(companyUrl);

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

                await _companyRepository.Add(companyData);
                await TaskQueue.QueueBackgroundWorkItemAsync(companyData);

               _logger.LogInformation("AddByUrlAsync method inside CompanyController was called successfully");
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
                _logger.LogError(e.Message);
                return BadRequest(e);
            }
        }

        [HttpGet]
        [Route("GetById")]
        [ProducesResponseType(typeof(ResponseCompany), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var company = await  _companyRepository.GetById(id);

                if (company != null)
                {
                    _logger.LogInformation("GetById method inside CompanyController was called successfully");

                    var productCount = await _productRepository.GetCountByCompanyId(company.Id);
                    return Ok(new ResponseCompany
                    {
                        ExternalId = company.ExternalId,
                        Id = company.Id,
                        Url = company.Url,
                        SyncDate = company.SyncDate,
                        Name = company.Name,
                        ProductCount = productCount
                    });
                }

                _logger.LogWarning("GetById method inside CompanyController returned NotFound");
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet]
        [Route("GetByName")]
        [ProducesResponseType(typeof(ResponseCompany), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetByNameAsync(string name)
        {
            try
            {
                var company = await _companyRepository.GetByName(name);

                if (company != null)
                {
                    _logger.LogInformation("GetById method inside CompanyController was called successfully");

                    var productCount = await _productRepository.GetCountByCompanyId(company.Id);
                    return Ok(new ResponseCompany
                    {
                        ExternalId = company.ExternalId,
                        Id = company.Id,
                        Url = company.Url,
                        SyncDate = company.SyncDate,
                        Name = company.Name,
                        ProductCount = productCount
                    });
                }

                _logger.LogWarning("GetById method inside CompanyController returned NotFound");
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(typeof(IEnumerable<ResponseCompany>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var company = await _companyRepository.GetAll();

                var companyDataArray = company as CompanyData[] ?? company.ToArray();
                if (companyDataArray.Any())
                {
                    _logger.LogInformation("GetAll method inside CompanyController was called successfully");

                    var responseCompanyArray = new List<ResponseCompany>();

                    foreach (var companyData in companyDataArray)
                    {
                        var productCount = await _productRepository.GetCountByCompanyId(companyData.Id);

                        responseCompanyArray.Add(new ResponseCompany
                        {
                            ExternalId = companyData.ExternalId,
                            Id = companyData.Id,
                            Url = companyData.Url,
                            SyncDate = companyData.SyncDate,
                            Name = companyData.Name,
                            ProductCount = productCount
                        });
                    }


                    return Ok(responseCompanyArray);
                }

                _logger.LogWarning("GetAll method inside CompanyController returned NotFound");
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}