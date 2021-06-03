using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using ShopParserApi.Models;
using ShopParserApi.Services;
using ShopParserApi.Services.Interfaces;
using ShopParserApi.Services.TimedHostedServices.BackgroundWorkItems;
using Xunit;

namespace ShopParserApi.Tests
{
    public class CompanyServiceTest
    {
        #region CanInsertCompanyIntoDbCompanyData

        [Fact]
        public async Task Can_InsertCompanyIntoDb_companyData()
        {
            //Arrange
            await using var context = new ApplicationDb(ContextOptions);

            Seed();

            var browsingContextServiceMock = new Mock<IBrowsingContextService>();
            var backgroundTaskQueueMock = new BackgroundProductsQueue(100);
            var logger = Mock.Of<ILogger<CompanyService>>();

            browsingContextServiceMock.Setup(service => service.OpenPageAsync("CompanyPageTest;1.html"))
                .ReturnsAsync(await MockOpenPageAsync());

            var controller = new CompanyService(context, browsingContextServiceMock.Object, backgroundTaskQueueMock,
                logger);
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

            var company1 = new CompanyData {Name = "One", Url = "CompanyPageTest.html"};
            context.Add(company1);
            context.SaveChanges();
        }

        #endregion
    }
}