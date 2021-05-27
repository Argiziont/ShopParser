using System;
using ShopParserApi.Models;
using ShopParserApi.Services.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ShopParserApi.Services.Repositories
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly string _connectionString;

        public CategoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<CategoryData>> GetAll()
        {
            await using var connection = new SqlConnection(_connectionString);

            return await connection.QueryAsync<CategoryData>("sp_GetAllCategories", commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<CategoryData>> GetPaged(int page, int rowsPerPage)
        {
            await using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            var values = new{page, rowsPerPage };

            return await connection.QueryAsync<CategoryData>("sp_GetPagedCategories", param: values, commandType: CommandType.StoredProcedure);
            
        }

    }
}