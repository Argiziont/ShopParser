using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using ShopParserApi.Services.GraphQlServices.GeneratedService;

namespace ShopParserApi.Services.GraphQlServices
{
    public class CompanyQueryService
    {
        public async Task<ICollection<GeneratedService.ResponseCompany>> GetCompaniesAsync(
            [Service] CompanyClient service, CancellationToken cancellationToken)
        {
            return await service.GetAllAsync(cancellationToken);
        }
    }
    public class CompanyQueryType : ObjectTypeExtension<CompanyQueryService>
    {
        protected override void Configure(IObjectTypeDescriptor<CompanyQueryService> descriptor) =>
            descriptor.Name(nameof(QueryService));
    }
}