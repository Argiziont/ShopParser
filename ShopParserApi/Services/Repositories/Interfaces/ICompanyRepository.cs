using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopParserApi.Models;

namespace ShopParserApi.Services.Repositories.Interfaces
{
    public interface ICompanyRepository
    {
        public Task<IEnumerable<CompanyData>> GetAll();
        public Task<CompanyData> GetByName(string name);
        public Task<CompanyData> GetById(int id);
        public Task Add(DateTime syncDate, int? sourceId, string externalId, string name, string url, string jsonData,
            string jsonDateSchema, int companyState);
        public Task Add(CompanyData companyData);
    }
}