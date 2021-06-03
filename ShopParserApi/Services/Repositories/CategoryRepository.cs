using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using ShopParserApi.Models;
using ShopParserApi.Services.Repositories.Interfaces;

namespace ShopParserApi.Services.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly string _connectionString;

        public CategoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<CategoryData>> GetAll()
        {
            await using var connection = new SqlConnection(_connectionString);

            return await connection.QueryAsync<CategoryData>("sp_GetAllCategories",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<CategoryData>> GetPaged(int page, int rowsPerPage)
        {
            await using var connection = new SqlConnection(_connectionString);

            var values = new {page, rowsPerPage};

            return await connection.QueryAsync<CategoryData>("sp_GetPagedCategories", values,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<CategoryData>> GetNestedByParentId(int categoryId)
        {
            await using var connection = new SqlConnection(_connectionString);

            var values = new {categoryId};

            return await connection.QueryAsync<CategoryData>("sp_GetNestedCategoryByParentId", values,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<CategoryData>> GetNestedByParentIdAndCompanyId(int categoryId, int companyId)
        {
            await using var connection = new SqlConnection(_connectionString);

            var values = new {categoryId, companyId};

            return await connection.QueryAsync<CategoryData>("sp_GetNestedCategoryByParentIdAndCompanyId", values,
                commandType: CommandType.StoredProcedure);
        }
    }
}