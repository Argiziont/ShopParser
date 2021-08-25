using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ShopParserApi.Models;
using ShopParserApi.Models.ResponseModels;
using ShopParserApi.Services.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopParserApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryController> _logger;
        private readonly IProductRepository _productRepository;

        public CategoryController(ILogger<CategoryController> logger,
            ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _logger = logger;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        #region Obsolete Requests

        //[HttpGet]
        //[Route("GetPaged")]
        //[ProducesResponseType(typeof(IEnumerable<ResponseCategory>), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        //[ProducesDefaultResponseType]
        //public async Task<IActionResult> GetPagedAsync(int page, int rowsPerPage)
        //{
        //    try
        //    {
        //        var categoriesList = await _categoryRepository.GetPaged(page, rowsPerPage);

        //        _logger.LogInformation("GetPagedAsync method inside CategoryController was called successfully");

        //        var response = categoriesList.Select(c => new ResponseCategory
        //        {
        //            Id = c.Id,
        //            Href = c.Url,
        //            Name = c.Name
        //        }).ToList();

        //        foreach (var responseCategory in response)
        //        {
        //            var productsCount = await _productRepository.GetCountByCategoryId(responseCategory.Id);
        //            responseCategory.ProductsCount = productsCount.ToString();
        //        }

        //        return Ok(response);
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogError(e.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, e);
        //    }
        //}

        //[HttpGet]
        //[Route("GetAllNested")]
        //[ProducesResponseType(typeof(ResponseNestedCategory), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesDefaultResponseType]
        //public async Task<IActionResult> GetAllNestedAsync()
        //{
        //    try
        //    {
        //        var currentCategory = await _categoryRepository.GetAll();
        //        var topLevelCategory = currentCategory.FirstOrDefault(cat => cat.SupCategoryData == null);

        //        if (topLevelCategory == null)
        //            return NotFound();

        //        var reversedCategory = await ReverseCategoryListRecursive(topLevelCategory);
        //        _logger.LogInformation("GetAllNestedAsync method inside CategoryController was called successfully");

        //        return Ok(reversedCategory);
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogError(e.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, e);
        //    }
        //}
        #endregion

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

                foreach (ResponseCategory responseCategory in response)
                {
                    int productsCount = await _productRepository.GetCountByCategoryId(responseCategory.Id);
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

                var categoryDataArray =
                    categoriesList as CategoryData[] ?? categoriesList.ToArray(); //Avoiding multiple itterations
                if (!categoryDataArray.Any())
                    return Ok(new List<ResponseCategory>());

                var response = categoryDataArray.Select(c => new ResponseCategory
                {
                    Id = c.Id,
                    Href = c.Url,
                    Name = c.Name
                }).ToList();

                foreach (ResponseCategory responseCategory in response)
                {
                    int productsCount = await _productRepository.GetCountByCategoryId(responseCategory.Id);
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
                var categoriesList = await _categoryRepository.GetNestedByParentIdAndCompanyId(id, companyId);
                var categoryDataArray = categoriesList as CategoryData[] ?? categoriesList.ToArray();

                if (!categoryDataArray.Any())
                    return Ok(new List<ResponseCategory>());

                _logger.LogInformation(
                    "GetNestedByParentIdAsync method inside CategoryController was called successfully");

                var response = categoryDataArray.Select(c => new ResponseCategory
                {
                    Id = c.Id,
                    Href = c.Url,
                    Name = c.Name
                }).ToList();

                foreach (ResponseCategory responseCategory in response)
                {
                    int productsCount = await _productRepository.GetCountByCategoryId(responseCategory.Id);
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
                int productCount = await _productRepository.GetCountByCategoryIdAndCompanyId(id, companyId);

                _logger.LogInformation(
                    "GetNestedByParentIdAsync method inside CategoryController was called successfully");

                return Ok(productCount);
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
                    int productCount = await _productRepository.GetCountByCategoryId(id);


                    _logger.LogInformation(
                        "GetNestedByParentIdAsync method inside CategoryController was called successfully");

                    return Ok(productCount);
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

        private async Task<ResponseNestedCategory> ReverseCategoryListRecursive(CategoryData mainCategoryData)
        {
            var subCategory = await _categoryRepository.GetNestedByParentId(mainCategoryData.Id);
            int productCount = await _productRepository.GetCountByCategoryId(mainCategoryData.Id);

            var subCategoryList = subCategory.Select(cat => ReverseCategoryListRecursive(cat).Result).ToList();
            return new ResponseNestedCategory
            {
                Id = mainCategoryData.Id,
                Name = mainCategoryData.Name,
                Href = mainCategoryData.Url,
                ProductsCount = productCount.ToString(),
                SubCategories = subCategoryList
            };
        }
    }
}