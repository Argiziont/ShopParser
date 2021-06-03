using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ShopParserApi.Models;
using ShopParserApi.Models.Helpers;
using ShopParserApi.Models.Hubs;
using ShopParserApi.Models.Hubs.Clients;
using ShopParserApi.Models.Json_DTO;
using ShopParserApi.Models.ResponseModels;
using ShopParserApi.Services;
using ShopParserApi.Services.Exceptions;
using ShopParserApi.Services.Extensions;
using ShopParserApi.Services.Interfaces;
using ShopParserApi.Services.Repositories.Interfaces;

namespace ShopParserApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ApplicationDb _dbContext;
        private readonly ILogger<ProductController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;
        private readonly IHubContext<ApiHub, IApiClient> _productsHub;

        public ProductController(ApplicationDb db, IHubContext<ApiHub, IApiClient> productsHub,
            IProductService productService, ILogger<ProductController> logger, IProductRepository productRepository,
            ICompanyRepository companyRepository, ICategoryRepository categoryRepository)
        {
            _dbContext = db;
            _productsHub = productsHub;
            _productService = productService;
            _logger = logger;
            _productRepository = productRepository;
            _companyRepository = companyRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [Route("GetParsedProductPage")]
        [ProducesResponseType(typeof(ProductData), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ParseDataInsideProductPageAsync(string productUrl)
        {
            var parsedProduct = await _productService.InsertProductPageIntoDb(productUrl);

            _logger.LogInformation(
                "ParseDataInsideProductPageAsync method inside ProductController was called successfully");
            return Ok(parsedProduct);
        }

        [HttpPost]
        [Route("ParseAllProductUrlsInsideCompanyPage")]
        [ProducesResponseType(typeof(ProductData[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ParseAllProductUrlsInsideCompanyPageAsync(string companyName)
        {
            var company = await _companyRepository.GetByName(companyName);
            if (company == null)
            {
                _logger.LogWarning(
                    "ParseAllProductUrlsInsideCompanyPageAsync method inside ProductController returned BadRequest because \"This company doesn't exist in database\"");
                return BadRequest("This company doesn't exist in database");
            }

            var productsList = await _productRepository.GetAllByCompanyId(company.Id);
            var productDataArray = productsList as ProductData[] ?? productsList.ToArray();

            for (var i = 0; i < productDataArray.Length; i++)
            {
                var parsedProduct = await _productService.InsertProductPageIntoDb(productDataArray[i].Url);

                if (parsedProduct != null)
                {
                    parsedProduct.Id = productDataArray[i].Id;
                    productDataArray[i] = parsedProduct;
                    await _productRepository.Update(productDataArray[i].Id, parsedProduct);
                }


                productDataArray[i].ProductState = ProductState.Success;
                await _productRepository.UpdateProductState(productDataArray[i].Id, (int) ProductState.Success);
            }

            _logger.LogInformation(
                "ParseAllProductUrlsInsideCompanyPageAsync method inside ProductController was called successfully");
            return Ok(productsList);
        }

        [HttpPost]
        [Route("ParseSingleProductInsideCompanyPage")]
        [ProducesResponseType(typeof(ProductData), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ParseSingleProductInsideCompanyPageAsync(string productId)
        {
            var currentProduct =await _productRepository.GetById(Convert.ToInt32(productId));

            if (currentProduct == null)
            {
                _logger.LogWarning(
                    "ParseAllProductUrlsInsideCompanyPageAsync method inside ProductController returned BadRequest because \"This product doesn't exist in database\"");
                return BadRequest("This product doesn't exist in database");
            }

            if (currentProduct.ProductState == ProductState.Success)
            {
                _logger.LogWarning(
                    "ParseAllProductUrlsInsideCompanyPageAsync method inside ProductController returned Accepted because \"This product already up to date\"");
                return Accepted("This product already up to date");
            }

            try
            {
                await _productService.InsertProductPageIntoDb(currentProduct.Url);
            }
            catch (TooManyRequestsException)
            {
                currentProduct.ProductState = ProductState.Failed;
                _logger.LogWarning(
                    "ParseAllProductUrlsInsideCompanyPageAsync method inside ProductController returned BadRequest because \"Product couldn't be updated\"");
                return BadRequest("Product couldn't be updated");
            }

            await _productsHub.Clients.All.ReceiveMessage(
                $"Product with name categoryId: {currentProduct.ExternalId} was updated successfully");
            _logger.LogInformation(
                "ParseSingleProductInsideCompanyPageAsync method inside ProductController was called successfully");
            return Ok(currentProduct);
        }

        [HttpGet]
        [Route("GetFullProductsById")]
        [ProducesResponseType(typeof(ProductJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetFullProductsByIdAsync(int id)
        {
            try
            {
                var product = await _productRepository.GetById(id);
                var categoriesFromProducts = await _categoryRepository.GetByProductId(id);
                var jsonData = product?.JsonData;
                var deserializeJson =
                    JsonConvert.DeserializeObject<ProductJson>(jsonData ?? throw new InvalidOperationException());
                deserializeJson.StringCategory = categoriesFromProducts.CategoryToString();

                _logger.LogInformation("GetFullProductsById method inside ProductController was called successfully");
                return Ok(deserializeJson);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet]
        [Route("GetProductsByCompanyId")]
        [ProducesResponseType(typeof(IEnumerable<ResponseProduct>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetProductsByCompanyIdAsync(int id)
        {
            try
            {
                var productList = await _productRepository.GetSuccessfulByCompanyId(id);

                _logger.LogInformation(
                    "GetProductsByCompanyIdAsync method inside ProductController was called successfully");
                return Ok(productList.Select(p => new ResponseProduct
                {
                    Description = p.Description,
                    ExternalId = p.ExternalId,
                    Id = p.Id,
                    Url = p.Url,
                    SyncDate = p.SyncDate,
                    Price = p.Price,
                    Title = p.Title
                }));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet]
        [Route("GetProductsByCategoryId")]
        [ProducesResponseType(typeof(IEnumerable<ResponseProduct>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetProductsByCategoryIdAsync(int id)
        {
            try
            {
                var currentCategory = await _categoryRepository.GetById(id);
                if (currentCategory == null)
                {
                    _logger.LogWarning(
                        "GetProductsByCategoryIdAsync method inside ProductController returned NotFound");
                    return NotFound();
                }


                var productList = await _productRepository.GetByCategoryId(currentCategory.Id);

                _logger.LogInformation(
                    "GetProductsByCategoryIdAsync method inside ProductController was called successfully");
                return Ok(productList.Select(p => new ResponseProduct
                {
                    Description = p.Description,
                    ExternalId = p.ExternalId,
                    Id = p.Id,
                    Url = p.Url,
                    SyncDate = p.SyncDate,
                    Price = p.Price,
                    Title = p.Title
                }));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet]
        [Route("GetPagedProductsByCompanyId")]
        [ProducesResponseType(typeof(IEnumerable<ResponseProduct>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetPagedProductsByCompanyIdAsync(int id, int page, int rowsPerPage)
        {
            try
            {
                var productSource = await _dbContext.Products
                    .Where(p => p.CompanyId == id &&
                                p.ProductState ==
                                ProductState
                                    .Success) //Take products which owned by current company and was parsed successfully
                    .OrderBy(p => p.Id) //Order by internal DB categoryId
                    .Skip(page * rowsPerPage).Take(rowsPerPage).ToListAsync(); //Take products by page

                if (productSource.Count == 0)
                {
                    _logger.LogWarning(
                        "GetPagedProductsByCompanyIdAsync method inside ProductController returned NotFound");
                    return NotFound();
                }


                _logger.LogInformation(
                    "GetPagedProductsByCompanyIdAsync method inside ProductController was called successfully");
                return Ok(productSource.Select(p => new ResponseProduct
                {
                    Description = p.Description,
                    ExternalId = p.ExternalId,
                    Id = p.Id,
                    Url = p.Url,
                    SyncDate = p.SyncDate,
                    Price = p.Price,
                    Title = p.Title
                }));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet]
        [Route("GetPagedProducts")]
        [ProducesResponseType(typeof(IEnumerable<ResponseProduct>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetPagedProductsAsync(int page, int rowsPerPage)
        {
            try
            {
                var productSource = await _dbContext.Products
                    .Where(p => p.ProductState ==
                                ProductState
                                    .Success) //Take products which owned by current company and was parsed successfully
                    .OrderBy(p => p.Id) //Order by internal DB categoryId
                    .Skip(page * rowsPerPage).Take(rowsPerPage).ToListAsync(); //Take products by page

                if (productSource.Count == 0)
                {
                    _logger.LogWarning(
                        "GetPagedProductsAsync method inside ProductController returned NotFound");
                    return NotFound();
                }


                _logger.LogInformation(
                    "GetPagedProductsAsync method inside ProductController was called successfully");
                return Ok(productSource.Select(p => new ResponseProduct
                {
                    Description = p.Description,
                    ExternalId = p.ExternalId,
                    Id = p.Id,
                    Url = p.Url,
                    SyncDate = p.SyncDate,
                    Price = p.Price,
                    Title = p.Title
                }));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet]
        [Route("GetPagedProductsByCategoryId")]
        [ProducesResponseType(typeof(IEnumerable<ResponseProduct>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetPagedProductsByCategoryIdAsync(int id, int page, int rowsPerPage)
        {
            try
            {
                var currentCategory = await _dbContext.Categories.Include(c => c.Products)
                    .FirstOrDefaultAsync(c => c.Id == id);
                if (currentCategory == null)
                {
                    _logger.LogWarning(
                        "GetPagedProductsByCategoryIdAsync method inside ProductController returned NotFound");
                    return NotFound();
                }


                var productSource = currentCategory.Products
                    .Where(p => p.ProductState ==
                                ProductState
                                    .Success) //Take products which owned by current company and was parsed successfully
                    .OrderBy(p => p.Id) //Order by internal DB categoryId
                    .Skip(page * rowsPerPage).Take(rowsPerPage); //Take products by page

                _logger.LogInformation(
                    "GetPagedProductsByCategoryIdAsync method inside ProductController was called successfully");
                return Ok(productSource.Select(p => new ResponseProduct
                {
                    Description = p.Description,
                    ExternalId = p.ExternalId,
                    Id = p.Id,
                    Url = p.Url,
                    SyncDate = p.SyncDate,
                    Price = p.Price,
                    Title = p.Title
                }));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet]
        [Route("GetPagedProductsByCategoryIdAndCompanyId")]
        [ProducesResponseType(typeof(IEnumerable<ResponseProduct>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetPagedProductsByCategoryIdAndCompanyIdAsync(int categoryId, int companyId,
            int page, int rowsPerPage)
        {
            try
            {
                var currentCategory = await _dbContext.Categories.Include(c => c.Products)
                    .FirstOrDefaultAsync(c => c.Id == categoryId);

                if (currentCategory == null)
                {
                    _logger.LogWarning(
                        "GetPagedProductsByCategoryIdAndCompanyId method inside ProductController returned NotFound");
                    return NotFound();
                }

                var productSource = currentCategory.Products.Where(p => p.ProductState == ProductState.Success &&
                                                                        p.CompanyId ==
                                                                        companyId) //Take products which owned by current company and was parsed successfully
                    .OrderBy(p => p.Id) //Order by internal DB categoryId
                    .Skip(page * rowsPerPage).Take(rowsPerPage); //Take products by page


                _logger.LogInformation(
                    "GetPagedProductsByCategoryIdAndCompanyId method inside ProductController was called successfully");
                return Ok(productSource.Select(p => new ResponseProduct
                {
                    Description = p.Description,
                    ExternalId = p.ExternalId,
                    Id = p.Id,
                    Url = p.Url,
                    SyncDate = p.SyncDate,
                    Price = p.Price,
                    Title = p.Title
                }));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet]
        [Route("GetTotalProductCount")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetTotalProductCount()
        {
            try
            {
                var productsCount = await _dbContext.Products.Where(p => p.ProductState == ProductState.Success)
                    .CountAsync();

                _logger.LogInformation(
                    "GetTotalProductCount method inside ProductController was called successfully");
                return Ok(productsCount);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}