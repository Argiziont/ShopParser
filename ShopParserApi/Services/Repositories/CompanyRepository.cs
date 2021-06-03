using Dapper;
using Microsoft.Data.SqlClient;
using ShopParserApi.Models;
using ShopParserApi.Services.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

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

        public async Task Add(DateTime syncDate= new DateTime(), int? sourceId = null, string externalId = null, string name = null, string url = null, string jsonData = null,
            string jsonDateSchema=null, int companyState=0)
        {
            await using var connection = new SqlConnection(_connectionString);

            var values = new
            {
                @SourceId= sourceId,
                @ExternalId= externalId,
                @Name=name,
                @Url =url,
                @SyncDate=syncDate,
                @JsonData=jsonData,
                @JsonDataSchema=jsonDateSchema,
                @CompanyState=companyState
                
            };
            await connection.ExecuteAsync("sp_AddCompany", param: values, commandType: CommandType.StoredProcedure);

        }

        public async Task Add(CompanyData companyData) => await Add(companyData.SyncDate, companyData.SourceId,
            companyData.ExternalId, companyData.Name, companyData.Url, companyData.JsonData, companyData.JsonDataSchema,
            (int) companyData.CompanyState);
    }
}