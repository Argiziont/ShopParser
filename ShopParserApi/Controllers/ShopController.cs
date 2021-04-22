using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema.Generation;
using ShopParserApi.Models;
using ShopParserApi.Models.Helpers;
using ShopParserApi.Models.Hubs;
using ShopParserApi.Models.Hubs.Clients;
using ShopParserApi.Models.Json_DTO;
using ShopParserApi.Models.ResponseModels;
using ShopParserApi.Service;
using ShopParserApi.Service.Helpers;

namespace ShopParserApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IBrowsingContext _context;
        private readonly ApplicationDb _dbContext;
        private readonly IHubContext<ApiHub, IApiClient> _shopHub;

        public ShopController(ApplicationDb db, IHubContext<ApiHub, IApiClient> shopHub)
        {
            var config = Configuration.Default.WithDefaultLoader();
            _context = BrowsingContext.New(config);
            _dbContext = db;
            _shopHub = shopHub;
        }

        [HttpPost]
        [Route("ParseSellerPageProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AddProductsListFromSellerAsync(string sellerName)
        {
            try
            {
                var shop = _dbContext.Shops.FirstOrDefault(s => s.Name == sellerName);
                if (shop == null) return BadRequest("Database doesn't contains this seller");

                var sellerUrl = _dbContext.Shops.FirstOrDefault(u => u.Name == sellerName)?.Url;
                var sellerPage = await _context.OpenAsync(sellerUrl);

                await ShopService.AddProductsFromSellerPageToDb(shop, sellerPage, _dbContext, _shopHub);


                _dbContext.Entry(shop).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();

                return Ok(shop.Products);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        [Route("AddShopByUrl")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseShop), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AddShopByUrlAsync(
            string sellerUrl)
        {
            try
            {
                var sellerPage = await _context.OpenAsync(sellerUrl);

                //Example
                //https://prom.ua/ua/c1036196-ibabykievua-internet-magazin.html
                //c1036196-ibabykievua-internet-magazin.html
                //c1036196

                //Seller data
                var externalId = sellerUrl.Split("/").Last().Split('-').First();
                var sellerName = sellerPage.QuerySelector("span[class='ek-text ek-text_weight_bold ek-text_wrap_ellipsis']")?.InnerHtml ?? "";

                if (_dbContext.Shops.FirstOrDefault(s => s.ExternalId == externalId) != null)
                    return BadRequest("Database already contains this seller");

                var jsonSellerDat = new ShopJson
                {
                    ExternalId = externalId,
                    Url = sellerUrl,
                    SyncDate = DateTime.Now,
                    Name = sellerName
                };
                var generator = new JSchemaGenerator();

                var fullCategorySchema = generator.Generate(typeof(ShopJson)).ToString();
                var fullCategoryJson = JsonConvert.SerializeObject(jsonSellerDat);

                var seller = new ShopData
                {
                    Url = jsonSellerDat.Url,
                    Products = new List<ProductData>(),
                    ExternalId = jsonSellerDat.ExternalId,
                    SyncDate = jsonSellerDat.SyncDate,
                    Name = jsonSellerDat.Name,
                    JsonData = fullCategoryJson,
                    JsonDataSchema = fullCategorySchema,
                    ShopState = ShopState.Idle
                };
                await _dbContext.Shops.AddAsync(seller);
                await _dbContext.SaveChangesAsync();

                return Ok(new ResponseShop
                {
                    ExternalId = seller.ExternalId,
                    Id = seller.Id,
                    Url = seller.Url,
                    SyncDate = seller.SyncDate,
                    Name = seller.Name
                });
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        [HttpGet]
        [Route("GetShopById")]
        [ProducesResponseType(typeof(ResponseShop), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult GetShopById(int id)
        {
            try
            {
                var shop = _dbContext.Shops.FirstOrDefault(s => s.Id == id);

                if (shop != null)
                    return Ok(new ResponseShop
                    {
                        ExternalId = shop.ExternalId,
                        Id = shop.Id,
                        Url = shop.Url,
                        SyncDate = shop.SyncDate,
                        Name = shop.Name,
                        ProductCount = _dbContext.Products.Count(p =>
                            p.ShopId == shop.Id && p.ProductState == ProductState.Success)
                    });
                return NotFound();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet]
        [Route("GetShops")]
        [ProducesResponseType(typeof(IEnumerable<ResponseShop>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult GetShops()
        {
            try
            {
                var shop = _dbContext.Shops.ToList();
                if (shop.Count > 0)
                    return Ok(shop.Select(s => new ResponseShop
                    {
                        ExternalId = s.ExternalId,
                        Id = s.Id,
                        Url = s.Url,
                        SyncDate = s.SyncDate,
                        Name = s.Name,
                        ProductCount = _dbContext.Products.Count(p =>
                            p.ShopId == s.Id && p.ProductState == ProductState.Success)
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