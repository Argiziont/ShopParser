using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using ShopParserApi.Controllers;
using ShopParserApi.Models;
using ShopParserApi.Models.Hubs;
using ShopParserApi.Models.Hubs.Clients;
using ShopParserApi.Models.Json_DTO;
using ShopParserApi.Services;
using ShopParserApi.Services.Interfaces;
using Xunit;

namespace ShopParserApi.Tests
{
    public class ProductControllerTest
    {
        #region Seeding
        public ProductControllerTest()
        {
            ContextOptions = new DbContextOptionsBuilder<ApplicationDb>()
                .UseInMemoryDatabase("TestDatabaseProducts")
                .Options;
        }

        private DbContextOptions<ApplicationDb> ContextOptions { get; }
        private void Seed()
        {
            using var context = new ApplicationDb(ContextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var company1 = new CompanyData() { Name = "One" };

            var productJson1 = new ProductJson { Title = "One" };
            var productJson2 = new ProductJson { Title = "Two" };
            var productJson3 = new ProductJson { Title = "Three" };

            var product1 = new ProductData { Title = "One", JsonData = JsonConvert.SerializeObject(productJson1),Url = "Url1",ExternalId = "1"};
            var product2 = new ProductData { Title = "Two", JsonData = JsonConvert.SerializeObject(productJson2), Url = "Url2", ExternalId = "2" };
            var product3 = new ProductData { Title = "Three", JsonData = JsonConvert.SerializeObject(productJson3), Url = "Url3", ExternalId = "3" };

            var category1 = new Category { Name = "One" };
            var category2 = new Category { Name = "Two", SupCategory = category1 };
            var category3 = new Category { Name = "Three", SupCategory = category2 };

            product1.Categories= new List<Category> { category1, category2, category3 };
            product2.Categories = new List<Category> { category1, category2 };
            product3.Categories = new List<Category> { category1 };

            company1.Products = new List<ProductData> {product1, product2, product3};

            context.Add(company1);
            
            context.SaveChanges();

        }
        #endregion

        #region CanGetParsedProductPageProductData
        [Fact]
        public async Task Can_getParsedProductPage_productData()
        {
            //Arrange
            var productServiceMock = new Mock<IProductService>();

            productServiceMock.Setup(service => service.InsertProductPageIntoDb("Url")).ReturnsAsync((() => new ProductData {Id = 1,Title = "One"}));

            var controller = new ProductController(null, null, productServiceMock.Object);

            //Act
            var result = await controller.ParseDataInsideProductPageAsync("Url");
            var okResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);

            var okResultValue = okResult.Value as ProductData;
            Assert.NotNull(okResultValue);

            Assert.Equal("One", okResultValue.Title);
            Assert.Equal(1, okResultValue.Id);
        }
        #endregion

        #region CanParseAllProductUrlsInsideCompanyPageAsyncProductData
        [Fact]
        public async Task Can_parseAllProductUrlsInsideCompanyPageAsync_productData()
        {
            //Arrange
            await using var context = new ApplicationDb(ContextOptions);
            Seed();

            var productServiceMock = new Mock<IProductService>();

            productServiceMock.Setup(service => service.InsertProductPageIntoDb("Url1")).ReturnsAsync(() =>  context.Products.FirstOrDefault(p=>p.Url== "Url1"));
            productServiceMock.Setup(service => service.InsertProductPageIntoDb("Url2")).ReturnsAsync((() => context.Products.FirstOrDefault(p => p.Url == "Url2")));
            productServiceMock.Setup(service => service.InsertProductPageIntoDb("Url3")).ReturnsAsync((() => context.Products.FirstOrDefault(p => p.Url == "Url3")));

            var controller = new ProductController(context, null, productServiceMock.Object);

            //Act
            var result = await controller.ParseAllProductUrlsInsideCompanyPageAsync("One");
            var okResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);

            var okResultValue = okResult.Value as ProductData[];
            Assert.NotNull(okResultValue);

            Assert.Equal("One", okResultValue[0].Title);
            Assert.Equal(1, okResultValue[0].Id);
        }
        #endregion

        #region CanParseSingleProductInsideCompanyPageAsyncProductData
        [Fact]
        public async Task Can_parseSingleProductInsideCompanyPageAsync_productData()
        {
            //Arrange
            var mockHubContext = new Mock<IHubContext<ApiHub, IApiClient>>();
            var mockClients = new Mock<IHubClients<IApiClient>>();
            var mockClient = new Mock<IApiClient>();


            await using var context = new ApplicationDb(ContextOptions);
            Seed();

            var productServiceMock = new Mock<IProductService>();

            productServiceMock.Setup(service => service.InsertProductPageIntoDb("Url1")).ReturnsAsync(() => context.Products.FirstOrDefault(p => p.Url == "Url1"));

            mockHubContext.Setup(service => service.Clients).Returns(mockClients.Object);
            mockClients.Setup(service => service.All).Returns(mockClient.Object);
            mockClient.Setup(service => service.ReceiveMessage($"Product with name id: 1 was updated successfully"));

            var controller = new ProductController(context, mockHubContext.Object, productServiceMock.Object);

            //Act
            var result = await controller.ParseSingleProductInsideCompanyPageAsync("1");
            var okResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);

            var okResultValue = okResult.Value as ProductData;
            Assert.NotNull(okResultValue);

            Assert.Equal("One", okResultValue.Title);
            Assert.Equal(1, okResultValue.Id);
        }
        #endregion

        #region CanGetFullProductsByIdProductJson
        [Fact]
        public async Task Can_getFullProductsById_productJson()
        {
            //Arrange
            await using var context = new ApplicationDb(ContextOptions);
            Seed();

            var controller = new ProductController(context, null, null);

            //Act
            var result = controller.GetFullProductsById(2);
            var okResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);

            var okResultValue = okResult.Value as ProductJson;
            Assert.NotNull(okResultValue);

            Assert.Equal("Two", okResultValue.Title);
        }
        #endregion

    }
}