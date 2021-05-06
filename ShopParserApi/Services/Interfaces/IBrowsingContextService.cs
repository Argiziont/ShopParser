using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;

namespace ShopParserApi.Services.Interfaces
{
    public interface IBrowsingContextService
    {
        public Task<IDocument> OpenPageAsync(string path);
    }
}