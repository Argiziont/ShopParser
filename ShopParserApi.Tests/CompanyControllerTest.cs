using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using ShopParserApi.Controllers;
using ShopParserApi.Models;
using ShopParserApi.Services;
using ShopParserApi.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace ShopParserApi.Tests
{
    public class CompanyControllerTest
    {
        #region Seeding
        public CompanyControllerTest()
        {
            ContextOptions = new DbContextOptionsBuilder<ApplicationDb>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            Seed();
        }

        private DbContextOptions<ApplicationDb> ContextOptions { get; }
        private void Seed()
        {
            using var context = new ApplicationDb(ContextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var company1 = new CompanyData() { Name = "One", Url = "CompanyPageTest.html" };
            context.Add(company1);
            context.SaveChanges();

        }
        #endregion

        #region CanParseCompanyPageProductsProducts
        [Fact]
        public async Task Can_parseCompanyPageProducts_products()
        {
            //Arrange
            await using  var context = new ApplicationDb(ContextOptions);
            var companyServiceMock = new Mock<ICompanyService>();

            var fistCompany = context.Companies.First();
            companyServiceMock.Setup(service => service.InsertCompanyIntoDb(fistCompany)).ReturnsAsync(GetOneTestCompany(fistCompany));

            var controller = new CompanyController(context, companyServiceMock.Object);

            //Act
            var result = await controller.ParseCompanyPageProducts("One");
            var okResult = result as OkObjectResult;

            // assert
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
            var companyServiceMock = new Mock<ICompanyService>();
            
            var controller = new CompanyController(context, companyServiceMock.Object);

            //Act
            var result = await controller.AddByUrlAsync("CompanyPageTest.html");
            var okResult = result as OkObjectResult;

            // assert
            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);

            var okResultValue = okResult.Value as CompanyData;
            Assert.NotNull(okResultValue);


            //Assert.Equal(3, responseProducts.Length);
            //Assert.Equal("One", responseProducts[0].Title);
            //Assert.Equal("Two", responseProducts[1].Title);
            //Assert.Equal("Three", responseProducts[2].Title);
        }
        #endregion

        #region snippet_GetOneTestCompany
        private static CompanyData GetOneTestCompany(CompanyData company)
        {
            company.Products.Add(new ProductData() { Company = company, Title = "One" });
            company.Products.Add(new ProductData() { Company = company, Title = "Two" });
            company.Products.Add(new ProductData() { Company = company, Title = "Three" });


            return company;
        }
        #endregion
    }
}
