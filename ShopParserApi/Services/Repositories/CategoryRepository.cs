using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ShopParserApi.Models;
using ShopParserApi.Services.GeneratedClientFile;
using ShopParserApi.Services.Interfaces;
using ShopParserApi.Services.Repositories.Interfaces;

namespace ShopParserApi.Services.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IDapperExecutorFactory _dapperExecutorFactory;
        private readonly IMapper _mapper;

        public CategoryRepository(IDapperExecutorFactory dapperExecutorFactory, IMapper mapper)
        {
            _dapperExecutorFactory = dapperExecutorFactory;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryData>> GetAll()
        {
            var executor=_dapperExecutorFactory.CreateDapperExecutor<EmptyInputParams,sp_GetAllCategoriesOutput>();
            var spService = new sp_GetAllCategories(executor);

            var executeResult = await spService.Execute();
            try
            {
                return _mapper.Map<IEnumerable<sp_GetAllCategoriesOutput>, IEnumerable<CategoryData>>(executeResult);
            }
            catch (AutoMapperMappingException autoMapperException)
            {
                if (autoMapperException.InnerException != null) throw autoMapperException.InnerException;
                // this will break your call stack
                // you may not know where the error is called and rather
                // want to clone the InnerException or throw a brand new Exception

                return null;
            }
          
        }

        public async Task<IEnumerable<CategoryData>> GetPaged(int page, int rowsPerPage)
        {
            var executor = _dapperExecutorFactory.CreateDapperExecutor<sp_GetPagedCategoriesInput, sp_GetPagedCategoriesOutput>();
            var spService = new sp_GetPagedCategories(executor);

            var executeResult = await spService.Execute(new sp_GetPagedCategoriesInput{Page = page, RowsPerPage = rowsPerPage});
            try
            {
                return _mapper.Map<IEnumerable<sp_GetPagedCategoriesOutput>, IEnumerable<CategoryData>>(executeResult);
            }
            catch (AutoMapperMappingException autoMapperException)
            {
                if (autoMapperException.InnerException != null) throw autoMapperException.InnerException;
                return null;
            }
        }

        public async Task<IEnumerable<CategoryData>> GetNestedByParentId(int categoryId)
        {
            var executor = _dapperExecutorFactory.CreateDapperExecutor<sp_GetNestedCategoryByParentIdInput, sp_GetNestedCategoryByParentIdOutput>();
            var spService = new sp_GetNestedCategoryByParentId(executor);

            var executeResult = await spService.Execute(new sp_GetNestedCategoryByParentIdInput { CategoryId = categoryId});
            try
            {
                return _mapper.Map<IEnumerable<sp_GetNestedCategoryByParentIdOutput>, IEnumerable<CategoryData>>(executeResult);
            }
            catch (AutoMapperMappingException autoMapperException)
            {
                if (autoMapperException.InnerException != null) throw autoMapperException.InnerException;
                return null;
            }
        }

        public async Task<IEnumerable<CategoryData>> GetByProductId(int productId)
        {
            var executor = _dapperExecutorFactory.CreateDapperExecutor<sp_GetCategoriesByProductIdInput, sp_GetCategoriesByProductIdOutput>();
            var spService = new sp_GetCategoriesByProductId(executor);

            var executeResult = await spService.Execute(new sp_GetCategoriesByProductIdInput { ProductId = productId});
            try
            {
                return _mapper.Map<IEnumerable<sp_GetCategoriesByProductIdOutput>, IEnumerable<CategoryData>>(executeResult);
            }
            catch (AutoMapperMappingException autoMapperException)
            {
                if (autoMapperException.InnerException != null) throw autoMapperException.InnerException;
                return null;
            }
        }

        public async Task<IEnumerable<CategoryData>> GetNestedByParentIdAndCompanyId(int categoryId, int companyId)
        {
            var executor = _dapperExecutorFactory.CreateDapperExecutor<sp_GetNestedCategoryByParentIdAndCompanyIdInput, sp_GetNestedCategoryByParentIdAndCompanyIdOutput>();
            var spService = new sp_GetNestedCategoryByParentIdAndCompanyId(executor);

            var executeResult = await spService.Execute(new sp_GetNestedCategoryByParentIdAndCompanyIdInput {CategoryId = categoryId,CompanyId = companyId});
            try
            {
                return _mapper.Map<IEnumerable<sp_GetNestedCategoryByParentIdAndCompanyIdOutput>, IEnumerable<CategoryData>>(executeResult);
            }
            catch (AutoMapperMappingException autoMapperException)
            {
                if (autoMapperException.InnerException != null) throw autoMapperException.InnerException;
                return null;
            }
        }

        public async Task<CategoryData> GetById(int categoryId)
        {
            var executor = _dapperExecutorFactory.CreateDapperExecutor<sp_GetCategoryByIdInput, sp_GetCategoryByIdOutput>();
            var spService = new sp_GetCategoryById(executor);

            var executeResult = await spService.Execute(new sp_GetCategoryByIdInput { CategoryId = categoryId });
            try
            {
                return _mapper.Map<sp_GetCategoryByIdOutput, CategoryData>(executeResult.FirstOrDefault());
            }
            catch (AutoMapperMappingException autoMapperException)
            {
                if (autoMapperException.InnerException != null) throw autoMapperException.InnerException;
                return null;
            }
        }
    }
}