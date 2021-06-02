using System.Collections.Generic;
using System.Threading.Tasks;
using ShopParserApi.Models;

namespace ShopParserApi.Services.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<IEnumerable<CategoryData>> GetAll();

        public Task<IEnumerable<CategoryData>> GetPaged(int page, int rowsPerPage);

        public Task<IEnumerable<CategoryData>> GetNestedByParentId(int categoryId);

        public Task<IEnumerable<CategoryData>> GetNestedByParentIdAndCompanyId(int categoryId, int companyId);
    }
}
