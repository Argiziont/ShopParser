using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using ShopParserApi.Controllers;
using ShopParserApi.Models;
using ShopParserApi.Services;
using ShopParserApi.Services.Interfaces;
using Xunit;

namespace ShopParserApi.Tests
{
    public class CompanyServiceTest
    {
        #region Seeding
        public CompanyServiceTest()
        {
            ContextOptions = new DbContextOptionsBuilder<ApplicationDb>()
                .UseInMemoryDatabase("TestDatabaseCompaniesService")
                .Options;

            Seed();
        }

        private DbContextOptions<ApplicationDb> ContextOptions { get; }
        private void Seed()
        {
            using var context = new ApplicationDb(ContextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var company1 = new CompanyData { Name = "One", Url = "CompanyPageTest.html" };
            context.Add(company1);
            context.SaveChanges();

        }
        #endregion

        #region CanInsertCompanyIntoDbCompanyData
        [Fact]
        public async Task Can_InsertCompanyIntoDb_companyData()
        {
            //Arrange
            await using var context = new ApplicationDb(ContextOptions);
            Seed();
            var browsingContextServiceMock = new Mock<IBrowsingContextService>();

            browsingContextServiceMock.Setup(service => service.OpenPageAsync("CompanyPageTest;1.html"))
                .ReturnsAsync(await MockOpenPageAsync());

            var controller = new CompanyService(context, browsingContextServiceMock.Object);
            var targetProduct = context.Companies.FirstOrDefault();
            //Act
            var result = await controller.InsertCompanyIntoDb(targetProduct);

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Products);
        }
        #endregion

        #region snippet_Mocks
        private static async Task<IDocument> MockOpenPageAsync()
        {
            var browsingContext = new BrowsingContextService();

            return await browsingContext.OpenPageAsync("CompanyPageTest.html");

        }
        #endregion
    }
}