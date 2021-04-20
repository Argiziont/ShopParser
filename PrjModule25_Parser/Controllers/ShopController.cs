using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Dom;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema.Generation;
using PrjModule25_Parser.Models;
using PrjModule25_Parser.Models.Helpers;
using PrjModule25_Parser.Models.Hubs;
using PrjModule25_Parser.Models.Hubs.Clients;
using PrjModule25_Parser.Models.JSON_DTO;
using PrjModule25_Parser.Models.ResponseModels;
using PrjModule25_Parser.Service;

namespace PrjModule25_Parser.Controllers
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

                //Number of pages   
                    var pageCount = sellerPage.QuerySelectorAll("button[data-qaid='pages']")
                    .Select(m => int.Parse(m.InnerHtml))
                    .Max();


                //Get all pages for current seller
                var productsLinkList = new List<string>();
                for (var i = 1; i <= pageCount; i++)
                {
                    var page = await _context.OpenAsync(sellerUrl?.Replace(".html", "") + ";" + i + ".html");
                    productsLinkList.AddRange(
                        page.QuerySelectorAll("*[data-qaid='product_link']").ToList().Cast<IHtmlAnchorElement>()
                            .Select(u => u.Href));

                    if ((Math.Round((double)pageCount / i) * 100)%10==0)
                    {
                        await _shopHub.Clients.All.ReceiveMessage($"Currently parsing shop with name \"{sellerName}\" \n Already done \"{Math.Round((double)pageCount/i)*100}%\" pages");
                    }

                    Thread.Sleep(2000);
                }

                var emptyProducts = productsLinkList.Select(url => new ProductData
                {
                    Url = url,
                    Shop = shop,
                    ProductState = ProductState.Idle
                }).ToList();

                shop.SyncDate = DateTime.Now;

                var jsonSellerDat = new ShopJson
                {
                    ExternalId = shop.ExternalId,
                    Url = shop.Url,
                    SyncDate = shop.SyncDate,
                    Name = shop.Name
                };

                var generator = new JSchemaGenerator();
                var fullCategorySchema = generator.Generate(typeof(ShopJson)).ToString();
                var fullCategoryJson = JsonConvert.SerializeObject(jsonSellerDat);

                shop.Products = emptyProducts;
                shop.JsonData = fullCategoryJson;
                shop.JsonDataSchema = fullCategorySchema;

                await _dbContext.Products.AddRangeAsync(emptyProducts);
                _dbContext.Entry(shop).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();

                return Ok(productsLinkList);
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
                var sellerName = sellerPage.QuerySelector("span[data-qaid='company_name']")?.InnerHtml ?? "";

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
                    ExternalId = jsonSellerDat.ExternalId,
                    SyncDate = jsonSellerDat.SyncDate,
                    Name = jsonSellerDat.Name,
                    JsonData = fullCategoryJson,
                    JsonDataSchema = fullCategorySchema
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