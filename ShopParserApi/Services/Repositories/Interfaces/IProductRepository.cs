using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopParserApi.Models;

namespace ShopParserApi.Services.Repositories.Interfaces
{
    public interface IProductRepository
    {
        public Task<IEnumerable<ProductData>> GetAll();

        public Task<IEnumerable<ProductData>> GetByCategoryId(int categoryId);
        public Task<IEnumerable<ProductData>> GetAllByCompanyId(int companyId);
        public Task<IEnumerable<ProductData>> GetSuccessfulByCompanyId(int companyId);

        public Task<ProductData> GetById(int id);

        public Task<int> GetCountByCategoryId(int categoryId);
        public Task<int> GetCountByCompanyId(int companyId);
        public Task<int> GetCountByCategoryIdAndCompanyId(int categoryId, int companyId);

        public Task Update(int productId, int? companyId, string externalId, string title, string url,
            DateTime syncDate,
            DateTime expirationDate, int productState, string description, string price, string keyWords,
            string jsonData, string jsonDataSchema);

        public Task Update(int productId, ProductData product);
        public Task UpdateProductState(int productId, int productState);
    }
}