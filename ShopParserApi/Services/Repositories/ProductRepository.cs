using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using ShopParserApi.Models;
using ShopParserApi.Services.Repositories.Interfaces;

namespace ShopParserApi.Services.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<ProductData>> GetAll()
        {
            await using var connection = new SqlConnection(_connectionString);

            return await connection.QueryAsync<ProductData>("sp_GetAllProducts",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<ProductData>> GetByCategoryId(int categoryId)
        {
            await using var connection = new SqlConnection(_connectionString);

            var values = new {categoryId};

            return await connection.QueryAsync<ProductData>("sp_GetProductsByCategoryId", values,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<ProductData>> GetAllByCompanyId(int companyId)
        {
            await using var connection = new SqlConnection(_connectionString);

            var values = new {companyId};

            return await connection.QueryAsync<ProductData>("sp_GetAllProductsByCompanyId", values,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> GetCountByCategoryId(int categoryId)
        {
            await using var connection = new SqlConnection(_connectionString);

            var values = new {categoryId};

            return await connection.ExecuteScalarAsync<int>("sp_CountProductsWithCategory", values,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> GetCountByCompanyId(int companyId)
        {
            await using var connection = new SqlConnection(_connectionString);

            var values = new {companyId};

            return await connection.ExecuteScalarAsync<int>("sp_CountProductsWithCompany", values,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> GetCountByCategoryIdAndCompanyId(int categoryId, int companyId)
        {
            await using var connection = new SqlConnection(_connectionString);

            var values = new {categoryId, companyId};

            return await connection.ExecuteScalarAsync<int>("sp_CountProductsWithCategoryAndCompany", values,
                commandType: CommandType.StoredProcedure);
        }

        public async Task Update(int productId, int? companyId, string externalId, string title, string url,
            DateTime syncDate, DateTime expirationDate,
            int productState, string description, string price, string keyWords, string jsonData, string jsonDataSchema)
        {
            await using var connection = new SqlConnection(_connectionString);

            var values = new
            {
                productId,
                companyId,
                externalId,
                title,
                url,
                syncDate,
                expirationDate,
                productState,
                description,
                price,
                keyWords,
                jsonData,
                jsonDataSchema
            };

            await connection.ExecuteAsync("sp_UpdateProduct", values, commandType: CommandType.StoredProcedure);
        }

        public async Task Update(int productId, ProductData product)
        {
            await Update(productId, product.CompanyId, product.ExternalId,
                product.Title, product.Url, product.SyncDate, product.ExpirationDate, (int) product.ProductState,
                product.Description, product.Price, product.KeyWords, product.JsonData, product.JsonDataSchema);
        }

        public async Task UpdateProductState(int productId, int productState)
        {
            await using var connection = new SqlConnection(_connectionString);

            var values = new {productId, productState};

            await connection.ExecuteAsync("sp_UpdateProductState", values, commandType: CommandType.StoredProcedure);
        }
    }
}