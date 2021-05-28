using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopParserApi.Models;
using ShopParserApi.Models.Helpers;
using ShopParserApi.Models.ResponseModels;
using ShopParserApi.Services;
using ShopParserApi.Services.Repositories.Interfaces;

namespace ShopParserApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDb _dbContext;
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public CategoryController(ApplicationDb dbContext, ILogger<CategoryController> logger, ICategoryRepository categoryRepository , IProductRepository productRepository)
        {
            _dbContext = dbContext;
            _logger = logger;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(typeof(IEnumerable<ResponseCategory>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var categoriesList = await _categoryRepository.GetAll();

                _logger.LogInformation("GetAll method inside CategoryController was called successfully");

                var response = categoriesList.Select(c => new ResponseCategory
                {
                    Id = c.Id,
                    Href = c.Url,
                    Name = c.Name
                }).ToList();

                foreach (var responseCategory in response)
                {
                    var productsCount = await _productRepository.GetCountByCategoryId(responseCategory.Id);
                    responseCategory.ProductsCount = productsCount.ToString();
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet]
        [Route("GetPaged")]
        [ProducesResponseType(typeof(IEnumerable<ResponseCategory>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetPagedAsync(int page, int rowsPerPage)
        {
            try
            {
                var categoriesList = await _categoryRepository.GetPaged(page, rowsPerPage);

                _logger.LogInformation("GetPagedAsync method inside CategoryController was called successfully");

                var response = categoriesList.Select(c => new ResponseCategory
                {
                    Id = c.Id,
                    Href = c.Url,
                    Name = c.Name
                }).ToList();

                foreach (var responseCategory in response)
                {
                    var productsCount = await _productRepository.GetCountByCategoryId(responseCategory.Id);
                    responseCategory.ProductsCount = productsCount.ToString();
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet]
        [Route("GetAllNested")]
        [ProducesResponseType(typeof(ResponseNestedCategory), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAllNestedAsync()
        {
            try
            {
                var currentCategory = await _categoryRepository.GetAll();
                var topLevelCategory=  currentCategory.FirstOrDefault(cat => cat.SupCategoryData == null);

                if (topLevelCategory == null)
                    return NotFound();

                var reversedCategory = ReverseCategoryListRecursive(topLevelCategory, _dbContext);
                _logger.LogInformation("GetAllNestedAsync method inside CategoryController was called successfully");

                return Ok(reversedCategory);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet]
        [Route("GetNestedByParentId")]
        [ProducesResponseType(typeof(IEnumerable<ResponseCategory>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetNestedByParentIdAsync(int id)
        {
            try
            {
                var categoriesList = await _categoryRepository.GetNestedByParentId(id);

                var categoryDataArray = categoriesList as CategoryData[] ?? categoriesList.ToArray(); //Avoiding multiple itterations
                if (!categoryDataArray.Any())
                    return Ok(new List<ResponseCategory>());



                _logger.LogInformation("GetNestedByParentIdAsync method inside CategoryController was called successfully");

                var response = categoryDataArray.Select(c => new ResponseCategory
                {
                    Id = c.Id,
                    Href = c.Url,
                    Name = c.Name
                }).ToList();

                foreach (var responseCategory in response)
                {
                    var productsCount = await _productRepository.GetCountByCategoryId(responseCategory.Id);
                    responseCategory.ProductsCount = productsCount.ToString();
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet]
        [Route("GetNestedByParentIdAndCompanyId")]
        [ProducesResponseType(typeof(IEnumerable<ResponseCategory>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetNestedByParentIdAndCompanyIdAsync(int id, int companyId)
        {
            try
            {
                var categoriesList = await _dbContext.Categories.Where(cat => cat.SupCategoryData.Id == id&& cat.Products.Count(product=>product.Company.Id== companyId) !=0).ToListAsync();
                if (!categoriesList.Any())
                    return Ok(new List<ResponseCategory>());
                
                _logger.LogInformation("GetNestedByParentIdAsync method inside CategoryController was called successfully");

                var response = categoriesList.Select(c => new ResponseCategory
                {
                    Id = c.Id,
                    Href = c.Url,
                    Name = c.Name
                }).ToList();

                foreach (var responseCategory in response)
                {
                    var productsCount = await _productRepository.GetCountByCategoryId(responseCategory.Id);
                    responseCategory.ProductsCount = productsCount.ToString();
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet]
        [Route("GetProductCountByCategoryIdAndCompanyId")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetProductCountByCategoryIdAndCompanyIdAsync(int id, int companyId)
        {
            try
            {
                var currentCategory = await _dbContext.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);


                var productSource = currentCategory.Products.Where(p => p.ProductState == ProductState.Success &&
                                                                        p.CompanyId == companyId);

                _logger.LogInformation("GetNestedByParentIdAsync method inside CategoryController was called successfully");

                return Ok(productSource.Count());
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
        [HttpGet]
        [Route("GetProductCountByCategoryId")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetProductCountByCategoryIdAsync(int id)
        {
            try
            {
                try
                {
                    var currentCategory = await _dbContext.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);


                    var productSource = currentCategory.Products.Where(p => p.ProductState == ProductState.Success);

                    _logger.LogInformation("GetNestedByParentIdAsync method inside CategoryController was called successfully");

                    return Ok(productSource.Count());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
             
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
        private static ResponseNestedCategory ReverseCategoryListRecursive(CategoryData mainCategoryData,
            ApplicationDb dbContext)
        {
            var tmp = dbContext.Products.Count(cat => cat.Categories.Contains(mainCategoryData)).ToString();
            var tmp2 = dbContext.Categories.Where(cat => cat.SupCategoryData == mainCategoryData)
                .Select(cat => ReverseCategoryListRecursive(cat, dbContext)).ToList();

            return new ResponseNestedCategory
            {
                Id = mainCategoryData.Id,
                Name = mainCategoryData.Name,
                Href = mainCategoryData.Url,
                ProductsCount = dbContext.Products.Count(cat => cat.Categories.Contains(mainCategoryData)).ToString(),
                SubCategories = dbContext.Categories.Where(cat => cat.SupCategoryData == mainCategoryData)
                    .Select(cat => ReverseCategoryListRecursive(cat, dbContext)).ToList()
            };
        }
    }
}