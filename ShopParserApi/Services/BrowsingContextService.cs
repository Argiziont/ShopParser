using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using ShopParserApi.Services.Extensions;
using ShopParserApi.Services.Interfaces;

namespace ShopParserApi.Services
{
    public class BrowsingContextService: IBrowsingContextService
    {
        public BrowsingContextService()
        {
            var config = Configuration.Default.WithDefaultLoader().WithJs().WithCss();
            _browsingContext= BrowsingContext.New(config);
        }

        private readonly IBrowsingContext _browsingContext;

        public async Task<IDocument> OpenPageAsync(string path) => await _browsingContext.OpenPageAsync(path);
    }
}