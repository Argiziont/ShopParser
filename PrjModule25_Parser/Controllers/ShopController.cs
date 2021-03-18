using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrjModule25_Parser.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using NJsonSchema;
using System.Threading.Tasks;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;

namespace PrjModule25_Parser.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetProductsListFromSellerAsync(string url = "https://prom.ua/Muzhskie-dzhinsy;c1916220")
        {
            try
            {

                var config = Configuration.Default.WithDefaultLoader();
                var context = BrowsingContext.New(config);

                var mainPage = await context.OpenAsync(url);
                
                    
                //Number of pages   
                var pageCount = mainPage.QuerySelectorAll("button[data-qaid='pages']")
                    .Select(m => int.Parse(m.InnerHtml))
                    .Max();

                //Get all pages for current seller
                var productsLinkList = new List<string>();
                for (var i = 1; i <= pageCount; i++)
                {
                    var page=await context.OpenAsync(url + ";" + i);
                    productsLinkList.AddRange(page.All
                        .Where(m => m.LocalName == "a" && m.ClassList
                            .Contains("productTile__tileLink--204An"))
                        .Select(m => ((IHtmlAnchorElement)m).Href));
                }
                return Ok(productsLinkList);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [Route("GetProducts")]
        [HttpGet]
        [ProducesResponseType(typeof(List<ProductData>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetProducts(string url = "https://prom.ua/Sportivnye-kostyumy")
        {
            var productController = new ProductController();
            try
            {
                var config = Configuration.Default.WithDefaultLoader();
                var context = BrowsingContext.New(config);
                var document = await context.OpenAsync(url);
                
                var linksToProducts = document.All
                    .Where(m => m.LocalName == "a" && m.ClassList
                        .Contains("productTile__tileLink--204An"))
                    .Select(m => ((IHtmlAnchorElement)m).Href)
                    .ToList();

                var elements = new List<ProductData>();
                foreach (var link in linksToProducts)
                {
                    if (await productController.FindDataInsideProductPageAsync(link) is OkObjectResult product) 
                        elements.Add((ProductData) product.Value);
                }

                return Ok(elements);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }

}
//https://prom.ua/Sportivnye-kostyumy
//data-qaid="variation_block"
//data-qaid="image_block"
