using System;
using ShopParserApi.Models;
using ShopParserApi.Services.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ShopParserApi.Services.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly string _connectionString;

        public CompanyRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<CompanyData>> GetAll()
        {
            await using var connection = new SqlConnection(_connectionString);

            return await connection.QueryAsync<CompanyData>("sp_GetAllCompanies", commandType: CommandType.StoredProcedure);

        }

        public async Task<CompanyData> GetByName(string name)
        {
            await using var connection = new SqlConnection(_connectionString);

            var values = new { @companyName = name };

            return await connection.QuerySingleOrDefaultAsync<CompanyData>("sp_GetCompanyByName", param: values, commandType: CommandType.StoredProcedure);
        }

        public async Task<CompanyData> GetById(int id)
        {

                await using var connection = new SqlConnection(_connectionString);

                var values = new { @companyId = id };

                return await connection.QuerySingleOrDefaultAsync<CompanyData>("sp_GetCompanyById", param: values, commandType: CommandType.StoredProcedure);


        }
    }
}
//sp_GetCompanyById