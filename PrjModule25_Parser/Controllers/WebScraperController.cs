using System;
using AngleSharp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using PrjModule25_Parser.Models;

namespace PrjModule25_Parser.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WebScraperController : ControllerBase
    {
        private readonly ILogger<WebScraperController> _logger;

        // Constructor
        public WebScraperController(ILogger<WebScraperController> logger)
        {
            _logger = logger;
        }

        private async Task<IActionResult> GetPageData(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(url);

            // Debug
            var linksToProducts = document.All.Where(m => m.LocalName == "a" && m.ClassList.Contains("productTile__tileLink--204An")).Select(m => ((IHtmlAnchorElement)m).Href).ToList();
            //var elements = document.QuerySelectorAll("*[data-qaid='product-sku']");
            var elements = new List<CarAdvert>();
            foreach (var link in linksToProducts)
            {
                try
                {
                    var product = await context.OpenAsync(link);

                    var title = product.QuerySelector("*[data-qaid='product_name']")?.InnerHtml ?? "";
                    var sku = product.QuerySelector("span[data-qaid='product-sku']")?.InnerHtml ?? "";
                    var presence = product.QuerySelector("span[data-qaid='product_presence']")?.FirstElementChild?.InnerHtml ?? "";
                    var descriptionChildren = product.QuerySelector("div[data-qaid='descriptions']")?.Children;
                    var description = UnScrubDiv(descriptionChildren).Replace(@"&nbsp;","");

                    var price = Convert.ToDecimal(product.All.Select(m => m.GetAttribute("data-qaprice"))?.First(m => m != null).Replace(".",",") ?? "0");
                    var currency = product.All.Select(m => m.GetAttribute("data-qacurrency"))?.First(m => m != null) ?? "";

                    elements.Add(new CarAdvert()
                    {
                        Currency = currency,
                        Price = price,
                        Description = description,
                        Presence = presence,
                        ScuCode = sku,
                        Title = title
                    });
                }
                catch
                {
                    // ignored
                }
            }
            return Ok();

        }

        private string UnScrubDiv(IEnumerable<IElement> divElements)
        {
            var unScrubText = "";
            foreach (var div in divElements)
            {
                if (div.Children.Length > 0)
                    unScrubText += UnScrubDiv(div.Children);
                else
                    unScrubText += "\n" + div.InnerHtml;
            }

            return unScrubText;
        }

       //private async void CheckForUpdates(string url)
       //{
       //    // We create the container for the data we want
       //    var adverts = new List<dynamic>();

       //    /*
       //     * GetPageData will recursively fill the container with data
       //     * and the await keyword guarantees that nothing else is done
       //     * before that operation is complete.
       //     */
       //    await GetPageData(url, adverts);

       //    // TODO: Diff the data
       //}

       [Route("Get")]
        [HttpGet]
        public async Task<IActionResult> Get(string url= "https://prom.ua/Sportivnye-kostyumy")
        {
            await GetPageData(url);
            return Ok();
        }
    }

}
//https://prom.ua/Sportivnye-kostyumy
