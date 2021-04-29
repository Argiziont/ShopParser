using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AngleSharp;
using AngleSharp.Dom;
using Newtonsoft.Json;
using ShopParserApi.Models;
using ShopParserApi.Models.Helpers;
using ShopParserApi.Services.Exceptions;
using ShopParserApi.Services.Helpers;

namespace ShopParserApi.Services
{
    public class ProductService
    {
        private readonly ApplicationDb _dbContext;
        private readonly IBrowsingContext _browsingContext;
        public ProductService(ApplicationDb dbContext)
        {
            var config = Configuration.Default.WithDefaultLoader().WithJs().WithCss();
            _browsingContext = BrowsingContext.New(config);
            _dbContext = dbContext;
        }

        public async Task<ProductData> InsertPageIntoDb(string productUrl)
        {
            var product= _dbContext.Products.FirstOrDefault(p => p.ProductState == ProductState.Idle);
            return await InsertPageIntoDb(product);
        }
        public async Task<ProductData> InsertPageIntoDb(ProductData product)
        {
            IDocument productPage=null;
            if (Uri.IsWellFormedUriString(product.Url, UriKind.Absolute))
            {
                productPage = await _browsingContext.OpenAsync(product.Url);
            }
            if (product.Url ==HttpUtility.HtmlEncode(product.Url))
            {
                productPage = await _browsingContext.OpenAsync(req => req.Content(product.Url));
            }

            if (productPage==null)
                throw new NullReferenceException(nameof(productPage));


            if (productPage.StatusCode == HttpStatusCode.TooManyRequests)
                throw new TooManyRequestsException();

            var parsedProduct = ProductParsingService.ParseSinglePage(product, productPage);

            var categories = CategoryParsingService.ParseCategories(product, productPage);
            var enumerableCategories = categories as Category[] ?? categories.ToArray();
            parsedProduct.Categories = enumerableCategories.ToList();

            foreach (var category in enumerableCategories.Where(category =>
                _dbContext.Categories.FirstOrDefault(cat => cat.Name == category.Name) == null))
            {
                if (category.SupCategory?.SupCategory != null)
                    category.SupCategory.SupCategory = null;

                await _dbContext.Categories.AddAsync(new Category
                {
                    Url = category.Url,
                    Name = category.Name,
                    SupCategory = category.SupCategory == null
                        ? null
                        : _dbContext.Categories.FirstOrDefault(c => c.Name == category.SupCategory.Name)
                });

                await _dbContext.SaveChangesAsync();
            }

            product.ProductState = parsedProduct.ProductState;
            product.Description = parsedProduct.Description;
            product.ExpirationDate = parsedProduct.ExpirationDate;
            product.JsonData = parsedProduct.JsonData;
            product.ExternalId = parsedProduct.ExternalId;
            product.JsonDataSchema = parsedProduct.JsonDataSchema;
            product.Price = parsedProduct.Price;
            product.SyncDate = parsedProduct.SyncDate;
            product.Title = parsedProduct.Title;
            product.Url = parsedProduct.Url;
            product.KeyWords = parsedProduct.KeyWords;
            product.ProductAttribute = parsedProduct.ProductAttribute;
            product.ProductDeliveryOptions = parsedProduct.ProductDeliveryOptions;
            product.ProductPaymentOptions = parsedProduct.ProductPaymentOptions;
            product.Presence = parsedProduct.Presence;

            if (product.ProductAttribute.Count > 0)
                foreach (var attribute in product.ProductAttribute)
                    attribute.Product = product;

            if (product.ProductDeliveryOptions.Count > 0)
                foreach (var deliveryOption in product.ProductDeliveryOptions)
                    deliveryOption.Product = product;

            if (product.ProductPaymentOptions.Count > 0)
                foreach (var paymentOption in product.ProductPaymentOptions)
                    paymentOption.Product = product;

            if (parsedProduct.Categories != null)
                foreach (var currentCategory in parsedProduct.Categories)
                    product.Categories.Add(
                        _dbContext.Categories.FirstOrDefault(c => c.Name == currentCategory.Name));
            await _dbContext.SaveChangesAsync();
            return product;
        }
    }
}