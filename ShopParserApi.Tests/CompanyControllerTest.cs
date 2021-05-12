using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using ShopParserApi.Controllers;
using ShopParserApi.Models;
using ShopParserApi.Models.ResponseModels;
using ShopParserApi.Services;
using ShopParserApi.Services.Interfaces;
using ShopParserApi.Services.TimedHostedServices.BackgroundWorkItems;
using Xunit;

namespace ShopParserApi.Tests
{
    public class CompanyControllerTest
    {
        #region CanParseCompanyPageProductsProducts

        [Fact]
        public async Task Can_parseCompanyPageProducts_products()
        {
            //Arrange
            await using var context = new ApplicationDb(ContextOptions);
            var companyServiceMock = new Mock<ICompanyService>();
            var browsingContextServiceMock = new Mock<IBrowsingContextService>();
            var logger = Mock.Of<ILogger<CompanyController>>();
            


            var fistCompany = context.Companies.First();
            companyServiceMock.Setup(service => service.InsertCompanyIntoDb(fistCompany))
                .ReturnsAsync(MockInsertCompanyIntoDb(fistCompany));

            var controller =
                new CompanyController(context, companyServiceMock.Object, browsingContextServiceMock.Object, logger, null);

            //Act
            var result = await controller.ParseCompanyPageProducts("One");
            var okResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);

            var okResultValue = okResult.Value as IEnumerable<ProductData>;
            Assert.NotNull(okResultValue);

            var responseProducts = okResultValue as ProductData[] ?? okResultValue.ToArray();

            Assert.Equal(3, responseProducts.Length);
            Assert.Equal("One", responseProducts[0].Title);
            Assert.Equal("Two", responseProducts[1].Title);
            Assert.Equal("Three", responseProducts[2].Title);
        }

        #endregion

        #region CanAddByUrlAsyncResponseCompany

        [Fact]
        public async Task Can_addByUrlAsync_responseCompany()
        {
            //Arrange
            await using var context = new ApplicationDb(ContextOptions);
            var browsingContextServiceMock = new Mock<IBrowsingContextService>();
            var logger = Mock.Of<ILogger<CompanyController>>();
            var backgroundTaskQueueMock = new BackgroundCompaniesQueue(10);

            browsingContextServiceMock
                .Setup(service => service.OpenPageAsync("https://prom.ua/c3502019-toppoint-tvoj-internet.html"))
                .ReturnsAsync(await MockOpenPageAsync());

            var controller = new CompanyController(context, null, new BrowsingContextService(), logger, backgroundTaskQueueMock);

            //Act
            var result = await controller.AddByUrlAsync("https://prom.ua/c3502019-toppoint-tvoj-internet.html");
            var okResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);

            var okResultValue = okResult.Value as ResponseCompany;
            Assert.NotNull(okResultValue);

            Assert.Equal("3502019", okResultValue.ExternalId);
        }

        #endregion

        #region CanGetByIdResponseCompany

        [Fact]
        public async Task Can_getById_responseCompany()
        {
            //Arrange
            await using var context = new ApplicationDb(ContextOptions);
            var logger = Mock.Of<ILogger<CompanyController>>();

            var controller = new CompanyController(context, null, null, logger,null);

            //Act
            var result = controller.GetById(1);
            var okResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);

            var okResultValue = okResult.Value as ResponseCompany;
            Assert.NotNull(okResultValue);


            Assert.Equal("One", okResultValue.Name);
        }

        #endregion

        #region CanGetAllResponseCompanyList

        [Fact]
        public async Task Can_getAll_responseCompanyList()
        {
            //Arrange
            await using var context = new ApplicationDb(ContextOptions);
            var logger = Mock.Of<ILogger<CompanyController>>();

            var controller = new CompanyController(context, null, null, logger,null);

            //Act
            var result = controller.GetAll();
            var okResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);

            var okResultValue = okResult.Value as IEnumerable<ResponseCompany>;
            Assert.NotNull(okResultValue);


            var responseCompanies = okResultValue as ResponseCompany[] ?? okResultValue.ToArray();
            Assert.Single(responseCompanies);
            Assert.Equal("One", responseCompanies.First().Name);
        }

        #endregion

        #region Seeding

        public CompanyControllerTest()
        {
            ContextOptions = new DbContextOptionsBuilder<ApplicationDb>()
                .UseInMemoryDatabase("TestDatabaseCompanies")
                .Options;

            Seed();
        }

        private DbContextOptions<ApplicationDb> ContextOptions { get; }

        private void Seed()
        {
            using var context = new ApplicationDb(ContextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var company1 = new CompanyData {Name = "One", Url = "CompanyPageTest.html"};
            context.Add(company1);
            context.SaveChanges();
        }

        #endregion

        #region snippet_Mocks

        private static CompanyData MockInsertCompanyIntoDb(CompanyData company)
        {
            company.Products.Add(new ProductData {Company = company, Title = "One"});
            company.Products.Add(new ProductData {Company = company, Title = "Two"});
            company.Products.Add(new ProductData {Company = company, Title = "Three"});


            return company;
        }

        private static async Task<IDocument> MockOpenPageAsync()
        {
            var browsingContext = new BrowsingContextService();

            return await browsingContext.OpenPageAsync("CompanyPageTest.html");
        }

        #endregion
    }
}