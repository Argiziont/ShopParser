﻿using HotChocolate;
using ShopParserApi.Services.GraphQlServices.GeneratedService;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate.Types;

namespace ShopParserApi.Services.GraphQlServices
{
    public class ProductQueryService
    {
        [UseOffsetPaging(IncludeTotalCount = true, MaxPageSize = 100)]
        public async Task<ICollection<GeneratedService.ResponseProduct>> GetProductsAsync(
            [Service] ProductClient service, CancellationToken cancellationToken, int? companyId, int? categoryId)
        {
            if (companyId != null && categoryId != null)  
            {
                return await service.GetProductsByCategoryIdAndCompanyIdAsync(categoryId, companyId, cancellationToken);
            }
            if (companyId != null)
            {
                return await service.GetProductsByCompanyIdAsync(companyId,cancellationToken);
            }
            if (categoryId != null)
            {
                return await service.GetProductsByCategoryIdAsync(categoryId, cancellationToken);
            }
            return await service.GetAllProductsAsync(cancellationToken);
        }
        public async Task<GeneratedService.ProductJson> GetProductAsync(
            [Service] ProductClient service, CancellationToken cancellationToken, int id)
        {
           
            return await service.GetFullProductsByIdAsync(id,cancellationToken);
        }
    }
    public class ProductQueryType : ObjectTypeExtension<ProductQueryService>
    {
        protected override void Configure(IObjectTypeDescriptor<ProductQueryService> descriptor) =>
            descriptor.Name(nameof(QueryService));
    }
}