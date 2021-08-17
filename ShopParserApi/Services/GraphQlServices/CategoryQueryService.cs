using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DotLiquid.Exceptions;
using HotChocolate;
using HotChocolate.Types;
using ShopParserApi.Services.GraphQlServices.GeneratedService;

namespace ShopParserApi.Services.GraphQlServices
{
    public class CategoryQueryService
    {
        public async Task<ICollection<GeneratedService.ResponseCategory>> GetCategoriesAsync(
            [Service] CategoryClient service, CancellationToken cancellationToken, int? id, int? companyId)
        {
            if (id!=null&& companyId!=null)
            {
                return await service.GetNestedByParentIdAndCompanyIdAsync(id, companyId, cancellationToken);
            }
            if (companyId != null)
            {
                return await service.GetNestedByParentIdAsync(companyId, cancellationToken);
            }
            if (id != null)
            {
                throw new ArgumentException("There no such method which supports only id");
            }
            return await service.GetAllAsync(cancellationToken);

        }
    }
    public class CategoryQueryType : ObjectTypeExtension<CategoryQueryService>
    {
        protected override void Configure(IObjectTypeDescriptor<CategoryQueryService> descriptor) =>
            descriptor.Name(nameof(QueryService));
    }
}