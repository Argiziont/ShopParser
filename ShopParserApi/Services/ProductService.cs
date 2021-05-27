using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ShopParserApi.Models;
using ShopParserApi.Services.Exceptions;
using ShopParserApi.Services.Helpers;
using ShopParserApi.Services.Interfaces;

namespace ShopParserApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IBrowsingContextService _browsingContextService;
        private readonly ApplicationDb _dbContext;

        public ProductService(ApplicationDb dbContext, IBrowsingContextService browsingContextService)
        {
            _dbContext = dbContext;
            _browsingContextService = browsingContextService;
        }

        public async Task<ProductData> InsertProductPageIntoDb(string productUrl)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.Url == productUrl);
            return await InsertProductPageIntoDb(product);
        }

        public async Task<ProductData> InsertProductPageIntoDb(ProductData product)
        {
            var productPage = await _browsingContextService.OpenPageAsync(product.Url);

            if (productPage == null)
                throw new NullReferenceException(nameof(productPage));


            if (productPage.StatusCode == HttpStatusCode.TooManyRequests)
                throw new TooManyRequestsException();

            var parsedProduct = ProductParsingService.ParseSinglePage(product, productPage);

            var categories = CategoryParsingService.ParseCategories(product, productPage);
            var enumerableCategories = categories as CategoryData[] ?? categories.ToArray();

            parsedProduct.Categories = enumerableCategories.ToList();

            foreach (var category in enumerableCategories.Where(category =>
                _dbContext.Categories.FirstOrDefault(cat => cat.Name == category.Name) == null))
            {
                if (category.SupCategoryData?.SupCategoryData != null)
                    category.SupCategoryData.SupCategoryData = null;

                await _dbContext.Categories.AddAsync(new CategoryData
                {
                    Url = category.Url,
                    Name = category.Name,
                    SupCategoryData = category.SupCategoryData == null
                        ? null
                        : _dbContext.Categories.FirstOrDefault(c => c.Name == category.SupCategoryData.Name)
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