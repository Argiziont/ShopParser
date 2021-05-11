using System.IO;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;

namespace ShopParserApi.Services.Extensions
{
    public static class BrowsingContextExtensions
    {
        public static async Task<IDocument> OpenPageAsync(this IBrowsingContext browsingContext, string path)
        {
            var z = IsLocalPath(path);

            if (IsLocalPath(path))
                return await browsingContext.OpenAsync(req => req.Content(File.ReadAllText(path)));

            return await browsingContext.OpenAsync(path);
        }

        private static bool IsLocalPath(string p)
        {
            return File.Exists(p);
        }
    }
}