using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PrjModule25_Parser.Models;
using PrjModule25_Parser.Models.ResponseModels;
using PrjModule25_Parser.Service;

namespace PrjModule25_Parser.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDb _dbContext;

        public CategoryController(ApplicationDb dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpGet]
        [Route("GetCategories")]
        [ProducesResponseType(typeof(IEnumerable<ResponseCategory>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            try
            {
                var categoryList = await _dbContext.Categories.ToListAsync();
                return Ok(categoryList.Select(c => new ResponseCategory
                {
                    Id = c.Id,
                    Href = c.Href,
                    Name = c.Name,
                    ProductsCount = _dbContext.Products.Count(cat => cat.Categories.Contains(c)).ToString()
                }));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet]
        [Route("GetPagedCategories")]
        [ProducesResponseType(typeof(IEnumerable<ResponseCategory>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetPagedCategoriesAsync(int page, int rowsPerPage)
        {
            try
            {
                var categorySource = await _dbContext.Categories
                    .OrderBy(p => p.Id)
                    .Skip(page * rowsPerPage).Take(rowsPerPage).ToListAsync();
                
                return Ok(categorySource.Select(c => new ResponseCategory
                {
                    Id=c.Id,
                    Href = c.Href,
                    Name = c.Name,
                    ProductsCount = _dbContext.Products.Count(cat => cat.Categories.Contains(c)).ToString()
                }));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
        
        [HttpGet]
        [Route("GetSubCategories")]
        [ProducesResponseType(typeof(ResponseNestedCategory), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetSubCategoriesAsync()
        {
            try
            {
                var currentCategory = await _dbContext.Categories.FirstOrDefaultAsync(cat => cat.Name == "Prom.ua");
                if (currentCategory==null)
                    return NotFound();

                var reversedCategory = ReverseCategoryListRecursive(currentCategory, _dbContext);
                return Ok(reversedCategory);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
        
        private static ResponseNestedCategory ReverseCategoryListRecursive(Category mainCategory, ApplicationDb dbContext)
        {
            return new ResponseNestedCategory()
            { 
                Id= mainCategory.Id,
                Name = mainCategory.Name,
                Href = mainCategory.Href,
                ProductsCount = dbContext.Products.Count(cat => cat.Categories.Contains(mainCategory)).ToString(),
                SubCategories = dbContext.Categories.Where(cat => cat.SupCategory == mainCategory).Select(cat=>ReverseCategoryListRecursive(cat, dbContext)).ToList()
            };
        }
    }
}
