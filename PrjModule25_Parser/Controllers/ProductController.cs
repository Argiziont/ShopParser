using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NJsonSchema;
using PrjModule25_Parser.Models;
using PrjModule25_Parser.Models.Helpers;
using PrjModule25_Parser.Models.JSON_DTO;
using PrjModule25_Parser.Service;

namespace PrjModule25_Parser.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IBrowsingContext _context;
        private readonly ApplicationDb _dbContext;

        public ProductController(ApplicationDb db)
        {
            var config = Configuration.Default.WithDefaultLoader();
            _context = BrowsingContext.New(config);
            _dbContext = db;
        }

        [HttpGet]
        [Route("ParseProductPage")]
        [ProducesResponseType(typeof(ProductData), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ParseDataInsideProductPageAsync(string productUrl)
        {
            var productPage = await _context.OpenAsync(productUrl);
            var externalId = productUrl
                .Split("//")[1]
                .Split('/')[1].Split('-').First();

            if (_dbContext.Products.FirstOrDefault(s => s.ExternalId == externalId) != null)
            {
                return BadRequest("Database already contains this product");
            }

            var title = productPage.QuerySelector("*[data-qaid='product_name']")?.InnerHtml ?? "";
            var companyName = productPage.QuerySelector("*[data-qaid='company_name']")?.InnerHtml ?? "";
            var shop = _dbContext.Shops.FirstOrDefault(s => s.Name == companyName);

            var sku = productPage.QuerySelector("span[data-qaid='product-sku']")?.InnerHtml ?? "";

            var presence = productPage.QuerySelector("span[data-qaid='product_presence']")?.FirstElementChild?.InnerHtml ??
                           "";

            var descriptionChildren = productPage.QuerySelector("div[data-qaid='descriptions']")?.Children;
            
            var description = descriptionChildren !=null? UnScrubDiv(descriptionChildren).Replace(@"&nbsp;", ""):"";

            var priceSelector = (IHtmlSpanElement) productPage.QuerySelector("span[data-qaid='product_price']");
            var fullPriceSelector =
                (IHtmlSpanElement) productPage.QuerySelector("span[data-qaid='price_without_discount']");
            var optPriceSelector = (IHtmlSpanElement) productPage.QuerySelector("span[data-qaid='opt_price']");
            var shortCompanyRating = (IHtmlDivElement) productPage.QuerySelector("div[data-qaid='short_company_rating']");
            var breadcrumbsSeo = (IHtmlDivElement) productPage.QuerySelector("div[data-qaid='breadcrumbs_seo']");

            var fullCategory = UnScrubCategory(breadcrumbsSeo);
            var price = priceSelector?.Dataset["qaprice"] ?? "";
            var currency = priceSelector?.Dataset["qacurrency"] ?? "";

            var fullPrice = fullPriceSelector?.Dataset["qaprice"] ?? "";
            var fullCurrency = fullPriceSelector?.Dataset["qacurrency"] ?? "";

            var optPrice = optPriceSelector?.Dataset["qaprice"] ?? "";
            var optCurrency = optPriceSelector?.Dataset["qacurrency"] ?? "";

            var posPercent = shortCompanyRating?.Dataset["qapositive"] + "%";
            var lastYrReply = shortCompanyRating?.Dataset["qacount"] ?? "";


            //Picking image list
            var imageSrcList = productPage.QuerySelector("div[data-qaid='image_block']") //<Upper div>
                ?.Children //<Lower divs>
                ?.First(m => m.ClassList
                    .Contains("ek-grid__item_width_expand") == false) //<Lower div with thumbnails>
                ?.Children //<Ul>
                ?.First()
                ?.Children //<Li>
                ?.Select(i => ((IHtmlImageElement) i //<Img>
                    .QuerySelector("img[data-qaid='image_thumb']"))?.Source) //Src="Urls"
                .ToList();
            
            var fullCategorySchema = JsonSchema.FromType<Category>().ToJson();
            var fullCategoryJson = JsonConvert.SerializeObject(fullCategory);

            var jsonProductDat = new ProductJson
            {
                Currency = currency,
                Price = price,
                FullCurrency = fullCurrency,
                FullPrice = fullPrice,
                OptCurrency = optCurrency,
                OptPrice = optPrice,
                Description = description,
                Presence = presence,
                ScuCode = sku,
                Title = title,
                CompanyName = companyName,
                ImageUrls = imageSrcList,
                PositivePercent = posPercent,
                RatingsPerLastYear = lastYrReply,
                Url = productUrl,
                SyncDate = DateTime.Now,
                JsonCategory = fullCategoryJson,
                JsonCategorySchema = fullCategorySchema,
                ExternalId = externalId
            };

            
            var productSchema = JsonSchema.FromType<ProductJson>().ToJson();
            var productJson = JsonConvert.SerializeObject(jsonProductDat);
            var product = new ProductData
            {
                SyncDate = jsonProductDat.SyncDate,
                Url = jsonProductDat.Url,
                Description = jsonProductDat.Description,
                ExternalId = jsonProductDat.ExternalId,
                JsonData = productJson,
                JsonDataSchema = productSchema,
                Price = jsonProductDat.Price,
                Title = jsonProductDat.Title,
                Shop = shop
            };
            return Ok(product);
        }

        [HttpGet]
        [Route("ParseSellerPage")]
        [ProducesResponseType(typeof(ProductData), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ParseDataInsideSellerPageAsync(string shopName)
        {
            var seller = _dbContext.Shops.FirstOrDefault(s => s.Name == shopName);
            if (seller == null)
            {
                return BadRequest("This shop doesn't exist in database");
            }
            
            var productsList = _dbContext.Products.Where(p=>p.Shop.Id== seller.Id).ToArray();
            for (var i = 0; i < productsList.Length; i++)
            {
                var productOkObject = (await ParseDataInsideProductPageAsync(productsList[i].Url)) as OkObjectResult;
                var parsedProduct = (ProductData)productOkObject?.Value;
                if (parsedProduct != null)
                {
                    parsedProduct.Id = productsList[i].Id;
                    productsList[i] = parsedProduct;
                }

                productsList[i].ProductState = ProductState.Success;
                //_dbContext.Entry(productsList[i]).State = EntityState.Modified;
            }
            
            await _dbContext.SaveChangesAsync();
            
            return Ok(productsList);
        }


        private static string UnScrubDiv(IEnumerable<IElement> divElements)
        {
            var unScrubText = "";
            foreach (var div in divElements)
                if (div.Children.Length > 0)
                    unScrubText += UnScrubDiv(div.Children);
                else
                    unScrubText += "\n" + div.InnerHtml;

            return unScrubText;
        }

        private static Category UnScrubCategory(IParentNode divElement)
        {
            var topLevelCategory = new Category();
            var currentCategory = topLevelCategory;
            for (var i = 0; i < divElement.Children.Length - 1; i++)
            {
                var childCategory = (IHtmlAnchorElement) divElement.Children[i].Children.First();
                var subCategory = new Category();

                currentCategory.Href = childCategory.Href;
                currentCategory.Name = childCategory.Title;
                currentCategory.SubCategory = subCategory;
                if (i != divElement.Children.Length - 2)
                    currentCategory = subCategory;
                else
                    currentCategory.SubCategory = null;
            }

            return topLevelCategory;
        }
    }
}