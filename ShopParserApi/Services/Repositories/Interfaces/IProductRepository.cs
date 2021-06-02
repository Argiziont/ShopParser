using System.Collections.Generic;
using System.Threading.Tasks;
using ShopParserApi.Models;

namespace ShopParserApi.Services.Repositories.Interfaces
{
    public interface IProductRepository
    {
        public Task<IEnumerable<ProductData>> GetAll();

        public Task<IEnumerable<ProductData>> GetByCategoryId(int categoryId);

        public Task<int> GetCountByCategoryId(int categoryId);

        public Task<int> GetCountByCategoryIdAndCompanyId(int categoryId, int companyId);
    }
}