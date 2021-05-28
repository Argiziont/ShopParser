using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using ShopParserApi.Models;
using ShopParserApi.Services.Repositories.Interfaces;

namespace ShopParserApi.Services.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<IEnumerable<ProductData>> GetAll()
        {
            await using var connection = new SqlConnection(_connectionString);

            return await connection.QueryAsync<ProductData>("sp_GetAllProducts", commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<ProductData>> GetByCategoryId(int categoryId)
        {
            await using var connection = new SqlConnection(_connectionString);

            var values = new { categoryId }; 

            return await connection.QueryAsync<ProductData>("sp_GetProductsByCategoryId", values, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> GetCountByCategoryId(int categoryId)
        {
            await using var connection = new SqlConnection(_connectionString);

            var values = new { categoryId };

            return await connection.ExecuteScalarAsync<int>("sp_CountProductsWithCategory", values, commandType: CommandType.StoredProcedure);
        }
    }
}
//sp_CountProductsWithCategory