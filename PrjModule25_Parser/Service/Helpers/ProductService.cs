using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Newtonsoft.Json;
using NJsonSchema;
using PrjModule25_Parser.Models;
using PrjModule25_Parser.Models.Helpers;
using PrjModule25_Parser.Models.JSON_DTO;
using PrjModule25_Parser.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PrjModule25_Parser.Service.Helpers
{
    public static class ProductService
    {
        public static async Task ParseSinglePageAndInsertToDb(IDocument page, string productUrl,
            ApplicationDb dbContext)
        {
            var currentProduct = dbContext.Products.FirstOrDefault(s => s.Url == productUrl);

            if (page.StatusCode == HttpStatusCode.TooManyRequests)
                throw new TooManyRequestsException();

            if (currentProduct != null)
            {
                var parsedProduct = await ProductService.ParseSinglePage(page, currentProduct.Url, dbContext);

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
                    currentProduct.KeyWords = parsedProduct.KeyWords;
                    currentProduct.ProductAttribute = parsedProduct.ProductAttribute;
                }

                if (parsedProduct?.Categories != null)
                    foreach (var currentCategory in parsedProduct.Categories)
                    {
                        currentProduct.Categories.Add(dbContext.Categories.FirstOrDefault(c => c.Name == currentCategory.Name));
                    }

                if (currentProduct?.ProductAttribute.Count > 0)
                {
                    foreach (var attribute in currentProduct.ProductAttribute)
                    {
                        attribute.Product = currentProduct;
                    }
                   // await dbContext.AddRangeAsync()
                }
                  
            }

            await dbContext.SaveChangesAsync();

        }

        public static async Task<ProductData> ParseSinglePage(IDocument page,string productUrl, ApplicationDb dbContext)
        {

            var companyName = page.QuerySelector("*[data-qaid='company_name']")?.InnerHtml ?? "";
            var shop = dbContext.Shops.FirstOrDefault(s => s.Name == companyName);
            var title = page.QuerySelector("*[data-qaid='product_name']")?.InnerHtml ?? "";

            var attributesBlock = page.QuerySelector("li[data-qaid='attributes']");
            var attributesList = new List<ProductAttribute>();
            if (attributesBlock!=null)
            {
                var attributeNames = attributesBlock.QuerySelectorAll("span[class='ek-text ek-text_color_black-600 ek-text_wrap_break']").Select(a => a.InnerHtml).ToList();
                var attributeValues = attributesBlock.QuerySelectorAll("span[class='ek-text ek-text_wrap_break']").Select(a => a.InnerHtml).ToList();

                attributesList.AddRange(attributeNames.Select((t, i) => new ProductAttribute() {AttributeName = t, AttributeValue = attributeValues[i]}));
            }


            var keyWordsBlock= page.QuerySelector("meta[name='keywords']") as IHtmlMetaElement;
            var keyWords = keyWordsBlock?.Content;

            var sku = page.QuerySelector("span[data-qaid='product-sku']")?.InnerHtml ?? "";
            var presence =
                page.QuerySelector("span[data-qaid='product_presence']")?.FirstElementChild?.InnerHtml ??
                "";

            //Parsing description 
            var description = page.QuerySelector("div[data-qaid='descriptions']")?.Children?.Aggregate("",
                (current, descriptionTag) => current + "\n" + ExtractContentFromHtml(descriptionTag.Html()));

            //External id from url
            var externalId = productUrl
                .Split("/").Last().Split('-').First();

            var priceSelector = (IHtmlSpanElement)page.QuerySelector("span[data-qaid='product_price']");
            var fullPriceSelector =
                (IHtmlSpanElement)page.QuerySelector("span[data-qaid='price_without_discount']");
            var optPriceSelector = (IHtmlSpanElement)page.QuerySelector("span[data-qaid='opt_price']");

            var shortCompanyRating =
                (IHtmlDivElement)page.QuerySelector("div[data-qaid='short_company_rating']");

            var breadcrumbsSeo = (IHtmlDivElement)page.QuerySelector("div[data-qaid='breadcrumbs_seo']");

            var fullCategory = UnScrubCategory(breadcrumbsSeo);

            foreach (var category in fullCategory.Where(category => dbContext.Categories.FirstOrDefault(cat => cat.Name == category.Name) == null))
            {
                if (category.SupCategory?.SupCategory != null)
                    category.SupCategory.SupCategory = null;

                await dbContext.Categories.AddAsync(new Category()
                {
                    Href = category.Href,
                    Name = category.Name,
                    SupCategory = category.SupCategory == null
                        ? null
                        : dbContext.Categories.FirstOrDefault(c => c.Name == category.SupCategory.Name)
                });

                await dbContext.SaveChangesAsync();

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
            var imageSrcList = page.QuerySelector("div[data-qaid='image_block']") //<Upper div>
                ?.Children //<Lower divs>
                ?.First(m => m.ClassList
                    .Contains("ek-grid__item_width_expand") == false) //<Lower div with thumbnails>
                ?.Children //<Ul>
                ?.First()
                ?.Children //<Li>
                ?.Select(i => ((IHtmlImageElement)i //<Img>
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
                SyncDate = DateTime.Now,
                JsonCategory = fullCategoryJson,
                JsonCategorySchema = fullCategorySchema,
                StringCategory = CategoryToString(fullCategory),
                Url = page.Url,
                ExternalId = externalId,
                KeyWords = keyWords,
                ProductAttribute = attributesList
            };


            var productSchema = JsonSchema.FromType<ProductJson>().ToJson();
            var productJson = JsonConvert.SerializeObject(jsonProductDat);
            return new ProductData
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
                Categories = fullCategory,
                KeyWords = keyWords,
                ProductAttribute = attributesList,
            };

        }
       
        private static List<Category> UnScrubCategory(IParentNode divElement)
        {
            var categories = new List<Category>();
            Category higherLevelCategory = null;
            for (var i = 0; i < divElement.Children.Length - 1; i++)
            {
                var childCategory = (IHtmlAnchorElement)divElement.Children[i].Children.First();
                var currentCategory = new Category { SupCategory = higherLevelCategory };
                higherLevelCategory = currentCategory;

                currentCategory.Href = childCategory.Href;
                currentCategory.Name = childCategory.Title;
                categories.Add(currentCategory);
            }

            return categories;
        }

        private static string CategoryToString(IEnumerable<Category> categories)
        {
            var categoryString = categories.Aggregate("", (current, category) => current + (category.Name + " > "));
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