using System.Threading.Tasks;
using ShopParserApi.Models;

namespace ShopParserApi.Services.Interfaces
{
    public interface IProductService
    {
        public Task<ProductData> InsertProductPageIntoDb(string productUrl);

        public Task<ProductData> InsertProductPageIntoDb(ProductData product);
    }
}