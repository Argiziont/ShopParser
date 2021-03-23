using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PrjModule25_Parser.Controllers.Interfaces
{
    public interface IShopController
    {
        public Task<IActionResult> AddProductsListFromSellerAsync(string sellerName);
        public Task<IActionResult> FindDataInsideSellerPageAsync(string sellerUrl);
    }
}