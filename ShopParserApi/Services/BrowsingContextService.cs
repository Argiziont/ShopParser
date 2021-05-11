using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using ShopParserApi.Services.Extensions;
using ShopParserApi.Services.Interfaces;

namespace ShopParserApi.Services
{
    public class BrowsingContextService : IBrowsingContextService
    {
        private readonly IBrowsingContext _browsingContext;

        public BrowsingContextService()
        {
            var config = Configuration.Default.WithDefaultLoader().WithJs().WithCss();
            _browsingContext = BrowsingContext.New(config);
        }

        public async Task<IDocument> OpenPageAsync(string path)
        {
            return await _browsingContext.OpenPageAsync(path);
        }
    }
}