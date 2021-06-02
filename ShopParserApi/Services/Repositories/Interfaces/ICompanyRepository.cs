using System.Collections.Generic;
using System.Threading.Tasks;
using ShopParserApi.Models;

namespace ShopParserApi.Services.Repositories.Interfaces
{
    public interface ICompanyRepository
    {
        public Task<IEnumerable<CompanyData>> GetAll();
        public Task<CompanyData> GetByName(string name);
    }
}