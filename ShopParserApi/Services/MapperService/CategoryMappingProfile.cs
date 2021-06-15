using AutoMapper;
using ShopParserApi.Models;
using ShopParserApi.Services.GeneratedClientFile;

namespace ShopParserApi.Services.MapperService
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<sp_GetAllCategoriesOutput, CategoryData>();
            CreateMap<sp_GetPagedCategoriesOutput, CategoryData>();
            CreateMap<sp_GetNestedCategoryByParentIdOutput, CategoryData>();
            CreateMap<sp_GetCategoriesByProductIdOutput, CategoryData>();
            CreateMap<sp_GetNestedCategoryByParentIdAndCompanyIdOutput, CategoryData>();
            CreateMap<sp_GetCategoryByIdOutput, CategoryData>();
        }
    }
}