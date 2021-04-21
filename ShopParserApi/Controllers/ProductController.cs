using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AngleSharp;
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
using ShopParserApi.Service;
using ShopParserApi.Service.Exceptions;
using ShopParserApi.Service.Helpers;

namespace ShopParserApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IBrowsingContext _context;
        private readonly ApplicationDb _dbContext;
        private readonly IHubContext<ApiHub, IApiClient> _productsHub;

        public ProductController(ApplicationDb db, IHubContext<ApiHub, IApiClient> productsHub)
        {
            var config = Configuration.Default.WithDefaultLoader();
            _context = BrowsingContext.New(config);
            _dbContext = db;
            _productsHub = productsHub;
        }

        [HttpGet]
        [Route("GetParsedProductPage")]
        [ProducesResponseType(typeof(ProductData), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ParseDataInsideProductPageAsync(string productUrl)
        {
            var productPage = await _context.OpenAsync(productUrl);
            if (productPage.StatusCode == HttpStatusCode.TooManyRequests)
                throw new TooManyRequestsException();

            var parsedProduct = await ProductService.ParseSinglePage(productPage, productUrl, _dbContext);

            return Ok(parsedProduct);
        }

        [HttpPost]
        [Route("ParseAllProductUrlsInsideSellerPage")]
        [ProducesResponseType(typeof(ProductData), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ParseAllProductUrlsInsideSellerPageAsync(string shopName)
        {
            var seller = _dbContext.Shops.FirstOrDefault(s => s.Name == shopName);
            if (seller == null) return BadRequest("This shop doesn't exist in database");

            var productsList = _dbContext.Products.Where(p => p.Shop.Id == seller.Id).ToArray();
            for (var i = 0; i < productsList.Length; i++)
            {
                var productPage = await _context.OpenAsync(productsList[i].Url);
                if (productPage.StatusCode == HttpStatusCode.TooManyRequests)
                    throw new TooManyRequestsException();

                var parsedProduct = await ProductService.ParseSinglePage(productPage, productsList[i].Url, _dbContext);

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
        [Route("ParseSingleProductInsideSellerPage")]
        [ProducesResponseType(typeof(ProductData), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ParseSingleProductInsideSellerPageAsync(string productId)
        {
            var currentProduct = _dbContext.Products.FirstOrDefault(s => s.Id == Convert.ToInt32(productId));

            if (currentProduct == null)
                return BadRequest("This product doesn't exist in database");
            if (currentProduct.ProductState == ProductState.Success)
                return Accepted("This product already up to date");
            try
            {
                var productPage = await _context.OpenAsync(currentProduct.Url);
                if (productPage.StatusCode == HttpStatusCode.TooManyRequests)
                    throw new TooManyRequestsException();

                await ProductService.ParseSinglePageAndInsertToDb(productPage, currentProduct.Url, _dbContext);
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
                var jsonData = _dbContext.Products.FirstOrDefault(p => p.Id == id)?.JsonData;
                var deserializeJson =
                    JsonConvert.DeserializeObject<ProductJson>(jsonData ?? throw new InvalidOperationException());

                return Ok(deserializeJson);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet]
        [Route("GetProductsByShopId")]
        [ProducesResponseType(typeof(IEnumerable<ResponseProduct>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetProductsByShopIdAsync(int id)
        {
            try
            {
                var productList = await _dbContext.Products
                    .Where(p => p.ShopId == id && p.ProductState == ProductState.Success).ToListAsync();
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
        [Route("GetPagedProductsByShopId")]
        [ProducesResponseType(typeof(IEnumerable<ProductData>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetPagedProductsByShopIdAsync(int id, int page, int rowsPerPage)
        {
            try
            {
                var productSource = await _dbContext.Products
                    .Where(p => p.ShopId == id &&
                                p.ProductState ==
                                ProductState
                                    .Success) //Take products which owned by current shop and was parsed successfully
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
                                    .Success) //Take products which owned by current shop and was parsed successfully
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