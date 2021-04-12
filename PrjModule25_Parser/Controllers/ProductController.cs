using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NJsonSchema;
using PrjModule25_Parser.Models;
using PrjModule25_Parser.Models.Helpers;
using PrjModule25_Parser.Models.Hubs;
using PrjModule25_Parser.Models.Hubs.Clients;
using PrjModule25_Parser.Models.JSON_DTO;
using PrjModule25_Parser.Models.ResponseModels;
using PrjModule25_Parser.Service;
using PrjModule25_Parser.Service.Exceptions;

namespace PrjModule25_Parser.Controllers
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

            //External id from url
            var externalId = productUrl
                .Split("//")[1]
                .Split('/')[1].Split('-').First();

            
            var companyName = productPage.QuerySelector("*[data-qaid='company_name']")?.InnerHtml ?? "";
            var shop = _dbContext.Shops.FirstOrDefault(s => s.Name == companyName);
            var title = productPage.QuerySelector("*[data-qaid='product_name']")?.InnerHtml ?? "";
            var sku = productPage.QuerySelector("span[data-qaid='product-sku']")?.InnerHtml ?? "";
            var presence =
                productPage.QuerySelector("span[data-qaid='product_presence']")?.FirstElementChild?.InnerHtml ??
                "";

            //Parsing description 
            var description = productPage.QuerySelector("div[data-qaid='descriptions']")?.Children?.Aggregate("",
                (current, descriptionTag) => current + "\n" + ExtractContentFromHtml(descriptionTag.Html()));


            var priceSelector = (IHtmlSpanElement) productPage.QuerySelector("span[data-qaid='product_price']");
            var fullPriceSelector =
                (IHtmlSpanElement) productPage.QuerySelector("span[data-qaid='price_without_discount']");
            var optPriceSelector = (IHtmlSpanElement) productPage.QuerySelector("span[data-qaid='opt_price']");
            
            var shortCompanyRating =
                (IHtmlDivElement) productPage.QuerySelector("div[data-qaid='short_company_rating']");
            var breadcrumbsSeo = (IHtmlDivElement) productPage.QuerySelector("div[data-qaid='breadcrumbs_seo']");

            var fullCategory = UnScrubCategory(breadcrumbsSeo);
            foreach (var category in fullCategory.Where(category => _dbContext.Categories.FirstOrDefault(cat=> cat.Name== category.Name)==null))
            {
                if (category.SupCategory?.SupCategory != null)
                    category.SupCategory.SupCategory = null;

                await _dbContext.Categories.AddAsync(new Category()
                {
                    Href = category.Href,
                    Name = category.Name,
                    SupCategory = category.SupCategory == null
                        ? null
                        : _dbContext.Categories.FirstOrDefault(c => c.Name == category.SupCategory.Name)
                });

                await _dbContext.SaveChangesAsync();

            }

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
            var fullCategoryJson = JsonConvert.SerializeObject(fullCategory[0]);

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
                ExternalId = externalId,
                StringCategory = CategoryToString(fullCategory)
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
                Shop = shop,
                ProductState = ProductState.Success,
                Categories = fullCategory
            };
            return Ok(product);
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
                var productOkObject = await ParseDataInsideProductPageAsync(productsList[i].Url) as OkObjectResult;
                var parsedProduct = (ProductData) productOkObject?.Value;
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
                var productOkObject = await ParseDataInsideProductPageAsync(currentProduct.Url) as OkObjectResult;
                var parsedProduct = (ProductData)productOkObject?.Value;


                if (parsedProduct != null)
                {
                    currentProduct.ProductState = parsedProduct.ProductState;
                    currentProduct.Description = parsedProduct.Description;
                    currentProduct.ExpirationDate = parsedProduct.ExpirationDate;
                    currentProduct.JsonData = parsedProduct.JsonData;
                    currentProduct.ExternalId = parsedProduct.ExternalId;
                    currentProduct.JsonDataSchema = parsedProduct.JsonDataSchema;
                    currentProduct.Price = parsedProduct.Price;
                    currentProduct.SyncDate = parsedProduct.SyncDate;
                    currentProduct.Title = parsedProduct.Title;
                    currentProduct.Url = parsedProduct.Url;
                }

                if (parsedProduct?.Categories != null)
                    foreach (var currentCategory in parsedProduct.Categories)
                    {
                        currentProduct.Categories.Add(_dbContext.Categories.FirstOrDefault(c => c.Name == currentCategory.Name));
                    }

                await _dbContext.SaveChangesAsync();
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
                var currentCategory =await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
                if (currentCategory == null)
                    return NotFound();
                
                var productList = await _dbContext.Products
                    .Where(p => p.Categories.Contains(currentCategory) && p.ProductState == ProductState.Success).ToListAsync();
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
                    .Where(p => p.ShopId == id && p.ProductState == ProductState.Success)//Take products which owned by current shop and was parsed successfully
                    .OrderBy(p=>p.Id)//Order by internal DB id
                    .Skip(page * rowsPerPage).Take(rowsPerPage).ToListAsync();//Take products by page
                
                if(productSource.Count==0)
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
                    .Where(p => p.Categories.Contains(currentCategory) && p.ProductState == ProductState.Success)//Take products which owned by current shop and was parsed successfully
                    .OrderBy(p => p.Id)//Order by internal DB id
                    .Skip(page * rowsPerPage).Take(rowsPerPage).ToListAsync();//Take products by page

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

        private static List<Category> UnScrubCategory(IParentNode divElement)
        {
            var categories = new List<Category>();
            Category higherLevelCategory = null;
            for (var i = 0; i < divElement.Children.Length - 1; i++)
            {
                var childCategory = (IHtmlAnchorElement)divElement.Children[i].Children.First();
                var currentCategory = new Category {SupCategory = higherLevelCategory};
                higherLevelCategory = currentCategory;

                currentCategory.Href = childCategory.Href;
                currentCategory.Name = childCategory.Title;
                categories.Add(currentCategory);
            }

            return categories;
        }
       
        private static string CategoryToString(IEnumerable<Category> categories)
        {
            var categoryString= categories.Aggregate("", (current, category) => current + (category.Name + " > "));
            return categoryString.Remove(categoryString.Length - 3);
        }

        private static string ExtractContentFromHtml(string input)
        {
           var hp = new HtmlParser();
            var hpResult = hp.ParseFragment(input, null);
            return string.Concat(hpResult.Select(x => x.Text()));
        }
    }
}

