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

namespace ShopParserApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDb _dbContext;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ApplicationDb dbContext, ILogger<CategoryController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
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
                var categoryList = await _dbContext.Categories.ToListAsync();

                _logger.LogInformation("GetAll method inside CategoryController was called successfully");

                return Ok(categoryList.Select(c => new ResponseCategory
                {
                    Id = c.Id,
                    Href = c.Url,
                    Name = c.Name,
                    ProductsCount = _dbContext.Products.Count(cat => cat.Categories.Contains(c)).ToString()
                }));
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
                var categorySource = await _dbContext.Categories
                    .OrderBy(p => p.Id)
                    .Skip(page * rowsPerPage).Take(rowsPerPage).ToListAsync();

                _logger.LogInformation("GetPagedAsync method inside CategoryController was called successfully");

                return Ok(categorySource.Select(c => new ResponseCategory
                {
                    Id = c.Id,
                    Href = c.Url,
                    Name = c.Name,
                    ProductsCount = _dbContext.Products.Count(cat => cat.Categories.Contains(c)).ToString()
                }));
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
                var currentCategory = await _dbContext.Categories.FirstOrDefaultAsync(cat => cat.SupCategory == null);
                if (currentCategory == null)
                    return NotFound();

                var reversedCategory = ReverseCategoryListRecursive(currentCategory, _dbContext);
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
                var categoriesList = await _dbContext.Categories.Where(cat => cat.SupCategory.Id == id).ToListAsync();
                if (!categoriesList.Any())
                    return Ok(new List<ResponseCategory>());



                _logger.LogInformation("GetNestedByParentIdAsync method inside CategoryController was called successfully");

                return Ok(categoriesList.Select(cat=> new ResponseCategory
                {
                    Name = cat.Name,
                    Href = cat.Url,
                    Id = cat.Id,
                    ProductsCount = _dbContext.Products.Count(c => c.Categories.Contains(cat)).ToString()
                }));
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
                var categoriesList = await _dbContext.Categories.Where(cat => cat.SupCategory.Id == id&& cat.Products.Count(product=>product.Company.Id== companyId) !=0).ToListAsync();
                if (!categoriesList.Any())
                    return Ok(new List<ResponseCategory>());
                
                _logger.LogInformation("GetNestedByParentIdAsync method inside CategoryController was called successfully");

                return Ok(categoriesList.Select(cat => new ResponseCategory
                {
                    Name = cat.Name,
                    Href = cat.Url,
                    Id = cat.Id,
                    ProductsCount = _dbContext.Products.Count(c => c.Categories.Contains(cat)).ToString()
                }));
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
                var currentCategory = await _dbContext.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);


                var productSource = currentCategory.Products.Where(p => p.ProductState == ProductState.Success);

                _logger.LogInformation("GetNestedByParentIdAsync method inside CategoryController was called successfully");

                return Ok(productSource.Count());
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
        private static ResponseNestedCategory ReverseCategoryListRecursive(Category mainCategory,
            ApplicationDb dbContext)
        {
            return new ResponseNestedCategory
            {
                Id = mainCategory.Id,
                Name = mainCategory.Name,
                Href = mainCategory.Url,
                ProductsCount = dbContext.Products.Count(cat => cat.Categories.Contains(mainCategory)).ToString(),
                SubCategories = dbContext.Categories.Where(cat => cat.SupCategory == mainCategory)
                    .Select(cat => ReverseCategoryListRecursive(cat, dbContext)).ToList()
            };
        }
    }
}