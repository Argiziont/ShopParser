using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopParserApi.Controllers;
using ShopParserApi.Models;
using ShopParserApi.Models.ResponseModels;
using ShopParserApi.Services;
using Xunit;

namespace ShopParserApi.Tests
{
    public class CategoryControllerTest
    {
        #region CanGetAllCategories

        [Fact]
        public async Task Can_getAll_categories()
        {
            //Arrange
            await using var context = new ApplicationDb(ContextOptions);
            Seed();
            var controller = new CategoryController(context);

            //Act
            var result = await controller.GetAllAsync();
            var okResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);

            var okResultValue = okResult.Value as IEnumerable<ResponseCategory>;
            Assert.NotNull(okResultValue);

            var responseCategories = okResultValue as ResponseCategory[] ?? okResultValue.ToArray();

            Assert.Equal(3, responseCategories.Length);
            Assert.Equal("One", responseCategories[0].Name);
            Assert.Equal("Two", responseCategories[1].Name);
            Assert.Equal("Three", responseCategories[2].Name);
        }

        #endregion

        #region CanGetPagedCategories

        [Fact]
        public async Task Can_getPaged_categories()
        {
            //Arrange
            await using var context = new ApplicationDb(ContextOptions);
            Seed();
            var controller = new CategoryController(context);

            //Act
            var result = await controller.GetPagedAsync(1, 1);
            var okResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);

            var okResultValue = okResult.Value as IEnumerable<ResponseCategory>;
            Assert.NotNull(okResultValue);

            var responseCategories = okResultValue as ResponseCategory[] ?? okResultValue.ToArray();

            Assert.Single(responseCategories);
            Assert.Equal("Two", responseCategories[0].Name);
        }

        #endregion

        #region CanGetAllNestedCategories

        [Fact]
        public async Task Can_getAllNested_categories()
        {
            //Arrange
            await using var context = new ApplicationDb(ContextOptions);
            Seed();
            var controller = new CategoryController(context);

            //Act
            var result = await controller.GetAllNestedAsync();
            var okResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);

            var okResultValue = okResult.Value as ResponseNestedCategory;
            Assert.NotNull(okResultValue);

            var responseCategories = okResultValue;

            Assert.Equal("One", responseCategories.Name);
            Assert.Single(responseCategories.SubCategories);
        }

        #endregion

        #region Seeding

        public CategoryControllerTest()
        {
            ContextOptions = new DbContextOptionsBuilder<ApplicationDb>()
                .UseInMemoryDatabase("TestDatabaseCategories")
                .Options;
        }

        private DbContextOptions<ApplicationDb> ContextOptions { get; }

        private void Seed()
        {
            using var context = new ApplicationDb(ContextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var category1 = new Category {Name = "One"};
            var category2 = new Category {Name = "Two", SupCategory = category1};
            var category3 = new Category {Name = "Three", SupCategory = category2};
            context.AddRange(category1, category2, category3);
            context.SaveChanges();
        }

        #endregion
    }
}