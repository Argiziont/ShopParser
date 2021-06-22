using AutoMapper;
using ShopParserApi.Models;
using ShopParserApi.Services.GeneratedClientFile;

namespace ShopParserApi.Services.MapperService
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Sp_GetAllCategoriesOutput, CategoryData>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Url,
                    opt => opt.MapFrom(src => src.Url))
                .ForMember(dest => dest.Products,
                    opt => opt.Ignore())
                .ForMember(dest => dest.SupCategoryData,
                    opt => opt.Ignore());
            CreateMap<Sp_GetPagedCategoriesOutput, CategoryData>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Url,
                    opt => opt.MapFrom(src => src.Url))
                .ForMember(dest => dest.Products,
                    opt => opt.Ignore())
                .ForMember(dest => dest.SupCategoryData,
                    opt => opt.Ignore());
            CreateMap<Sp_GetNestedCategoryByParentIdOutput, CategoryData>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Url,
                    opt => opt.MapFrom(src => src.Url))
                .ForMember(dest => dest.Products,
                    opt => opt.Ignore())
                .ForMember(dest => dest.SupCategoryData,
                    opt => opt.Ignore());
            CreateMap<Sp_GetCategoriesByProductIdOutput, CategoryData>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Url,
                    opt => opt.MapFrom(src => src.Url))
                .ForMember(dest => dest.Products,
                    opt => opt.Ignore())
                .ForMember(dest => dest.SupCategoryData,
                    opt => opt.Ignore());
            CreateMap<Sp_GetNestedCategoryByParentIdAndCompanyIdOutput, CategoryData>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Url,
                    opt => opt.MapFrom(src => src.Url))
                .ForMember(dest => dest.Products,
                    opt => opt.Ignore())
                .ForMember(dest => dest.SupCategoryData,
                    opt => opt.Ignore());
            CreateMap<Sp_GetCategoryByIdOutput, CategoryData>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Url,
                    opt => opt.MapFrom(src => src.Url))
                .ForMember(dest => dest.Products,
                    opt => opt.Ignore())
                .ForMember(dest => dest.SupCategoryData,
                    opt => opt.Ignore());
        }
    }
}