using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
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

namespace ShopParserApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDb _dbContext;
        private readonly IProductService _productService;
        private readonly IHubContext<ApiHub, IApiClient> _productsHub;

        public ProductController(ApplicationDb db, IHubContext<ApiHub, IApiClient> productsHub,
            IProductService productService)
        {
            _dbContext = db;
            _productsHub = productsHub;
            _productService = productService;
        }

        [HttpGet]
        [Route("GetParsedProductPage")]
        [ProducesResponseType(typeof(ProductData), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ParseDataInsideProductPageAsync(string productUrl)
        {
            var parsedProduct = await _productService.InsertProductPageIntoDb(productUrl);

            return Ok(parsedProduct);
        }

        [HttpPost]
        [Route("ParseAllProductUrlsInsideCompanyPage")]
        [ProducesResponseType(typeof(ProductData[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ParseAllProductUrlsInsideCompanyPageAsync(string companyName)
        {
            var company = _dbContext.Companies.FirstOrDefault(s => s.Name == companyName);
            if (company == null) return BadRequest("This company doesn't exist in database");

            var productsList = _dbContext.Products.Where(p => p.Company.Id == company.Id).ToArray();
            for (var i = 0; i < productsList.Length; i++)
            {
                var parsedProduct = await _productService.InsertProductPageIntoDb(productsList[i].Url);

                if (parsedProduct != null)
                {
                    parsedProduct.Id = productsList[i].Id;
                    productsList[i] = parsedProduct;
                }

                productsList[i].ProductState = ProductState.Success;
            }

            await _dbContext.SaveChangesAsync();

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
            var currentProduct = _dbContext.Products.FirstOrDefault(s => s.Id == Convert.ToInt32(productId));

            if (currentProduct == null)
                return BadRequest("This product doesn't exist in database");
            if (currentProduct.ProductState == ProductState.Success)
                return Accepted("This product already up to date");
            try
            {
                await _productService.InsertProductPageIntoDb(currentProduct.Url);
            }
            catch (TooManyRequestsException)
            {
                currentProduct.ProductState = ProductState.Failed;
                return BadRequest("Product couldn't be updated");
            }

            await _productsHub.Clients.All.ReceiveMessage(
                $"Product with name id: {currentProduct.ExternalId} was updated successfully");
            return Ok(currentProduct);
        }

        [HttpGet]
        [Route("GetFullProductsById")]
        [ProducesResponseType(typeof(ProductJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult GetFullProductsById(int id)
        {
            try
            {
                var product = _dbContext.Products.Include(db => db.Categories).FirstOrDefault(p => p.Id == id);

                var jsonData = product?.JsonData;
                var deserializeJson =
                    JsonConvert.DeserializeObject<ProductJson>(jsonData ?? throw new InvalidOperationException());
                deserializeJson.StringCategory = product.Categories.CategoryToString();
                return Ok(deserializeJson);
            }
            catch (Exception e)
            {
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
                var productList = await _dbContext.Products
                    .Where(p => p.CompanyId == id && p.ProductState == ProductState.Success).ToListAsync();
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
                var currentCategory = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
                if (currentCategory == null)
                    return NotFound();

                var productList = await _dbContext.Products
                    .Where(p => p.Categories.Contains(currentCategory) && p.ProductState == ProductState.Success)
                    .ToListAsync();
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
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet]
        [Route("GetPagedProductsByCompanyId")]
        [ProducesResponseType(typeof(IEnumerable<ProductData>), StatusCodes.Status200OK)]
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
                    .OrderBy(p => p.Id) //Order by internal DB id
                    .Skip(page * rowsPerPage).Take(rowsPerPage).ToListAsync(); //Take products by page

                if (productSource.Count == 0)
                    return NotFound();

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
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet]
        [Route("GetPagedProductsByCategoryId")]
        [ProducesResponseType(typeof(IEnumerable<ProductData>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetPagedProductsByCategoryIdAsync(int id, int page, int rowsPerPage)
        {
            try
            {
                var currentCategory = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
                if (currentCategory == null)
                    return NotFound();

                var productSource = await _dbContext.Products
                    .Where(p => p.Categories.Contains(currentCategory) &&
                                p.ProductState ==
                                ProductState
                                    .Success) //Take products which owned by current company and was parsed successfully
                    .OrderBy(p => p.Id) //Order by internal DB id
                    .Skip(page * rowsPerPage).Take(rowsPerPage).ToListAsync(); //Take products by page

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
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}