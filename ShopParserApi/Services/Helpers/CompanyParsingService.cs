using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using ShopParserApi.Models;
using ShopParserApi.Models.Helpers;

namespace ShopParserApi.Services.Helpers
{
    public static class CompanyParsingService
    {
        public static IEnumerable<ProductData> ParseCompanyProducts(CompanyData company,IDocument companyPage)
        {


            var linksSublistPage = companyPage.QuerySelectorAll("*[data-qaid='product_link']").ToList()
                .Cast<IHtmlAnchorElement>()
                .Select(u => u.Href).ToList();

            return linksSublistPage.Select(url => new ProductData
            {
                Url = url,
                Company = company,
                ProductState = ProductState.Idle
            }).ToList();
        }
    }
}