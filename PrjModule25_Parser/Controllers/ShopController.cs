using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Dom;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema.Generation;
using PrjModule25_Parser.Models;
using PrjModule25_Parser.Models.JSON_DTO;
using PrjModule25_Parser.Service;

namespace PrjModule25_Parser.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IBrowsingContext _context;
        private readonly ApplicationDb _dbContext;

        public ShopController(ApplicationDb db)
        {
            var config = Configuration.Default.WithDefaultLoader();
            _context = BrowsingContext.New(config);
            _dbContext = db;
        }

        [HttpGet]
        [Route("ParseSellerPageProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetProductsListFromSellerAsync(string sellerName)
        {
            try
            {
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
                    var page = await _context.OpenAsync(sellerUrl + ";" + i);
                    productsLinkList.AddRange(page.All
                        .Where(m => m.LocalName == "a" && m.ClassList
                            .Contains("productTile__tileLink--204An"))
                        .Select(m => ((IHtmlAnchorElement) m).Href));
                }

                return Ok(productsLinkList);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        [Route("ParseSellerPage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ShopJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> FindDataInsideSellerPageAsync(
            string sellerUrl = "https://prom.ua/c1916220-internet-magazin-myjeans.html")
        {
            try
            {
                var sellerPage = await _context.OpenAsync(sellerUrl);

                //Seller data
                var externalId = sellerUrl.Split("//")[1].Split('/')[1].Split('-').First();
                var sellerName = sellerPage.QuerySelector("span[data-qaid='company_name']")?.InnerHtml ?? "";

                if (_dbContext.Shops.FirstOrDefault(s => s.ExternalId == externalId) != null)
                {
                    return BadRequest("Database already contains this seller");
                }

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

                return Ok(seller);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

    }
}