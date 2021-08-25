using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using ShopParserApi.Models;
using ShopParserApi.Models.ResponseModels;
using ShopParserApi.Services.Interfaces;
using ShopParserApi.Services.Repositories.Interfaces;

namespace ShopParserApi.Services.Repositories
{
    public class CategoryRepositoryCacher: ICategoryRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly DistributedCacheEntryOptions _cacheExpirationOptions;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryRepositoryCacher(ICategoryRepository categoryRepository, IDistributedCache distributedCache)
        {
            _categoryRepository = categoryRepository;
            _distributedCache = distributedCache;

            _cacheExpirationOptions = new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddHours(10),
                SlidingExpiration = TimeSpan.FromMinutes(5)
            };

        }

        public async Task<IEnumerable<CategoryData>> GetAll()
        {
            const string cacheKey = nameof(GetAll) + nameof(CategoryData) + "All";
            byte[] cachedCategories = await _distributedCache.GetAsync(cacheKey);
            if (cachedCategories != null)
            {
                string encodedCategories = Encoding.UTF8.GetString(cachedCategories);
                var cachedCategoriesList = JsonConvert.DeserializeObject<IEnumerable<CategoryData>>(encodedCategories);
                return cachedCategoriesList;
            }

            var response = await _categoryRepository.GetAll();
            byte[] decodedCategories = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response));
            
            await _distributedCache.SetAsync(cacheKey, decodedCategories, _cacheExpirationOptions);

            return response;
        }

        public async Task<IEnumerable<CategoryData>> GetPaged(int page, int rowsPerPage)
        {
            string cacheKey = nameof(GetPaged) + nameof(CategoryData) + page+ rowsPerPage;
            byte[] cachedCategories = await _distributedCache.GetAsync(cacheKey);
            if (cachedCategories != null)
            {
                string encodedCategories = Encoding.UTF8.GetString(cachedCategories);
                var cachedCategoriesList = JsonConvert.DeserializeObject<IEnumerable<CategoryData>>(encodedCategories);
                return cachedCategoriesList;
            }

            var response = await _categoryRepository.GetPaged(page, rowsPerPage);
            byte[] decodedCategories = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response));

            await _distributedCache.SetAsync(cacheKey, decodedCategories, _cacheExpirationOptions);

            return response;
        }

        public async Task<IEnumerable<CategoryData>> GetNestedByParentId(int categoryId)
        {
            string cacheKey = nameof(GetNestedByParentId) + nameof(CategoryData) + "categoryId";
            byte[] cachedCategories = await _distributedCache.GetAsync(cacheKey);
            if (cachedCategories != null)
            {
                string encodedCategories = Encoding.UTF8.GetString(cachedCategories);
                var cachedCategoriesList = JsonConvert.DeserializeObject<IEnumerable<CategoryData>>(encodedCategories);
                return cachedCategoriesList;
            }

            var response = await _categoryRepository.GetNestedByParentId(categoryId);
            byte[] decodedCategories = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response));

            await _distributedCache.SetAsync(cacheKey, decodedCategories, _cacheExpirationOptions);

            return response;
        }

        public async Task<IEnumerable<CategoryData>> GetByProductId(int productId)
        {
            const string cacheKey = nameof(GetByProductId) + nameof(CategoryData) + "productId";
            byte[] cachedCategories = await _distributedCache.GetAsync(cacheKey);
            if (cachedCategories != null)
            {
                string encodedCategories = Encoding.UTF8.GetString(cachedCategories);
                var cachedCategoriesList = JsonConvert.DeserializeObject<IEnumerable<CategoryData>>(encodedCategories);
                return cachedCategoriesList;
            }

            var response = await _categoryRepository.GetByProductId(productId);
            byte[] decodedCategories = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response));

            await _distributedCache.SetAsync(cacheKey, decodedCategories, _cacheExpirationOptions);

            return response;
        }

        public async Task<IEnumerable<CategoryData>> GetNestedByParentIdAndCompanyId(int categoryId, int companyId)
        {
            string cacheKey = nameof(GetNestedByParentIdAndCompanyId) + nameof(CategoryData) + categoryId+ companyId;
            byte[] cachedCategories = await _distributedCache.GetAsync(cacheKey);
            if (cachedCategories != null)
            {
                string encodedCategories = Encoding.UTF8.GetString(cachedCategories);
                var cachedCategoriesList = JsonConvert.DeserializeObject<IEnumerable<CategoryData>>(encodedCategories);
                return cachedCategoriesList;
            }

            var response = await _categoryRepository.GetNestedByParentIdAndCompanyId(categoryId, companyId);
            byte[] decodedCategories = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response));

            await _distributedCache.SetAsync(cacheKey, decodedCategories, _cacheExpirationOptions);

            return response;
        }

        public async Task<CategoryData> GetById(int categoryId)
        {
            string cacheKey = nameof(GetById) + nameof(CategoryData) + categoryId;
            byte[] cachedCategories = await _distributedCache.GetAsync(cacheKey);
            if (cachedCategories != null)
            {
                string encodedCategories = Encoding.UTF8.GetString(cachedCategories);
                var cachedCategoriesList = JsonConvert.DeserializeObject<CategoryData>(encodedCategories);
                return cachedCategoriesList;
            }

            var response = await _categoryRepository.GetById(categoryId);
            byte[] decodedCategories = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response));

            await _distributedCache.SetAsync(cacheKey, decodedCategories, _cacheExpirationOptions);

            return response;
        }
    }
}