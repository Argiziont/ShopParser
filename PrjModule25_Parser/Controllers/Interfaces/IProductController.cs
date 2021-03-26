using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PrjModule25_Parser.Controllers.Interfaces
{
    public interface IProductController
    {
        public Task<IActionResult> ParseDataInsideProductPageAsync(string productUrl);
        public Task<IActionResult> ParseAllProductUrlsInsideSellerPageAsync(string shopName);
        public Task<IActionResult> ParseSingleProductInsideSellerPageAsync(string productId);
    }
}