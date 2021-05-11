using System.Threading.Tasks;
using ShopParserApi.Models;

namespace ShopParserApi.Services.Interfaces
{
    public interface ICompanyService
    {
        public Task<CompanyData> InsertCompanyIntoDb(CompanyData company);
    }
}