using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using ShopParserApi.Models;
using ShopParserApi.Models.Helpers;
using ShopParserApi.Models.Json_DTO;
using ShopParserApi.Services;
using ShopParserApi.Services.Interfaces;
using Xunit;

namespace ShopParserApi.Tests
{
    public class ProductServiceTest
    {
        #region CanInsertProductPageIntoDbViaProductProductData

        [Fact]
        public async Task Can_insertProductPageIntoDbViaProduct_productData()
        {
            //Arrange
            await using var context = new ApplicationDb(ContextOptions);
            Seed();
            var browsingContextServiceMock = new Mock<IBrowsingContextService>();

            browsingContextServiceMock.Setup(service =>
                    service.OpenPageAsync("https://prom.ua/p1367019485-rozumni-smart-godinnik.html?"))
                .ReturnsAsync(await MockOpenPageAsync());

            var controller = new ProductService(context, browsingContextServiceMock.Object);
            var targetProduct = context.Products.FirstOrDefault();
            if (targetProduct != null) targetProduct.Url = @"https://prom.ua/p1367019485-rozumni-smart-godinnik.html?";
            //Act
            var result = await controller.InsertProductPageIntoDb(targetProduct);

            //Assert
            Assert.NotNull(result);

            Assert.Equal("Розумні смарт годинник Smart watch DM08", result.Title);
        }

        #endregion

        #region CanInsertProductPageIntoDbViaUrlProductData

        [Fact]
        public async Task Can_insertProductPageIntoDbViaUrl_productData()
        {
            //Arrange
            await using var context = new ApplicationDb(ContextOptions);
            Seed();
            var browsingContextServiceMock = new Mock<IBrowsingContextService>();

            browsingContextServiceMock.Setup(service =>
                    service.OpenPageAsync("https://prom.ua/p1367019485-rozumni-smart-godinnik.html?"))
                .ReturnsAsync(await MockOpenPageAsync());

            var controller = new ProductService(context, browsingContextServiceMock.Object);
            var targetProduct = context.Products.FirstOrDefault();
            if (targetProduct != null) targetProduct.Url = @"https://prom.ua/p1367019485-rozumni-smart-godinnik.html?";
            await context.SaveChangesAsync();
            //Act
            var result = await controller.InsertProductPageIntoDb(targetProduct?.Url);

            //Assert
            Assert.NotNull(result);

            Assert.Equal("Розумні смарт годинник Smart watch DM08", result.Title);
        }

        #endregion

        #region snippet_Mocks

        private static async Task<IDocument> MockOpenPageAsync()
        {
            var browsingContext = new BrowsingContextService();

            return await browsingContext.OpenPageAsync("ProductPageTest.html");
        }

        #endregion

        #region Seeding

        public ProductServiceTest()
        {
            ContextOptions = new DbContextOptionsBuilder<ApplicationDb>()
                .UseInMemoryDatabase("TestDatabaseProductsService")
                .Options;
        }

        private DbContextOptions<ApplicationDb> ContextOptions { get; }

        private void Seed()
        {
            using var context = new ApplicationDb(ContextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var company1 = new CompanyData {Name = "One"};

            var productJson1 = new ProductJson {Title = "One"};
            var productJson2 = new ProductJson {Title = "Two"};
            var productJson3 = new ProductJson {Title = "Three"};

            var product1 = new ProductData
            {
                Title = "One", JsonData = JsonConvert.SerializeObject(productJson1), Url = "Url1", ExternalId = "1",
                ProductState = ProductState.Success
            };
            var product2 = new ProductData
                {Title = "Two", JsonData = JsonConvert.SerializeObject(productJson2), Url = "Url2", ExternalId = "2"};
            var product3 = new ProductData
                {Title = "Three", JsonData = JsonConvert.SerializeObject(productJson3), Url = "Url3", ExternalId = "3"};

            var category1 = new Category {Name = "One"};
            var category2 = new Category {Name = "Two", SupCategory = category1};
            var category3 = new Category {Name = "Three", SupCategory = category2};

            product1.Categories = new List<Category> {category1, category2, category3};
            product2.Categories = new List<Category> {category1, category2};
            product3.Categories = new List<Category> {category1};

            company1.Products = new List<ProductData> {product1, product2, product3};

            context.Add(company1);

            context.SaveChanges();
        }

        #endregion
    }
}