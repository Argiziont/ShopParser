using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using ShopParserApi.Models;
using ShopParserApi.Services.Dapper_Services.Interfaces;
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
            var executor=_dapperExecutorFactory.CreateDapperExecutor<EmptyInputParams,Sp_GetAllCategoriesOutput>();
            var spService = new Sp_GetAllCategories(executor);


            var executeResult = await spService.Execute();
            try
            {
                return _mapper.Map<IEnumerable<Sp_GetAllCategoriesOutput>, IEnumerable<CategoryData>>(executeResult);
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
            var executor = _dapperExecutorFactory.CreateDapperExecutor<Sp_GetPagedCategoriesInput, Sp_GetPagedCategoriesOutput>();
            var spService = new Sp_GetPagedCategories(executor);

            var executeResult = await spService.Execute(new Sp_GetPagedCategoriesInput{Page = page, RowsPerPage = rowsPerPage});
            try
            {
                return _mapper.Map<IEnumerable<Sp_GetPagedCategoriesOutput>, IEnumerable<CategoryData>>(executeResult);
            }
            catch (AutoMapperMappingException autoMapperException)
            {
                if (autoMapperException.InnerException != null) throw autoMapperException.InnerException;
                return null;
            }
        }

        public async Task<IEnumerable<CategoryData>> GetNestedByParentId(int categoryId)
        {
            var executor = _dapperExecutorFactory.CreateDapperExecutor<Sp_GetNestedCategoryByParentIdInput, Sp_GetNestedCategoryByParentIdOutput>();
            var spService = new Sp_GetNestedCategoryByParentId(executor);

            var executeResult = await spService.Execute(new Sp_GetNestedCategoryByParentIdInput { CategoryId = categoryId});
            try
            {
                return _mapper.Map<IEnumerable<Sp_GetNestedCategoryByParentIdOutput>, IEnumerable<CategoryData>>(executeResult);
            }
            catch (AutoMapperMappingException autoMapperException)
            {
                if (autoMapperException.InnerException != null) throw autoMapperException.InnerException;
                return null;
            }
        }

        public async Task<IEnumerable<CategoryData>> GetByProductId(int productId)
        {
            var executor = _dapperExecutorFactory.CreateDapperExecutor<Sp_GetCategoriesByProductIdInput, Sp_GetCategoriesByProductIdOutput>();
            var spService = new Sp_GetCategoriesByProductId(executor);

            var executeResult = await spService.Execute(new Sp_GetCategoriesByProductIdInput { ProductId = productId});
            try
            {
                return _mapper.Map<IEnumerable<Sp_GetCategoriesByProductIdOutput>, IEnumerable<CategoryData>>(executeResult);
            }
            catch (AutoMapperMappingException autoMapperException)
            {
                if (autoMapperException.InnerException != null) throw autoMapperException.InnerException;
                return null;
            }
        }

        public async Task<IEnumerable<CategoryData>> GetNestedByParentIdAndCompanyId(int categoryId, int companyId)
        {
            var executor = _dapperExecutorFactory.CreateDapperExecutor<Sp_GetNestedCategoryByParentIdAndCompanyIdInput, Sp_GetNestedCategoryByParentIdAndCompanyIdOutput>();
            var spService = new Sp_GetNestedCategoryByParentIdAndCompanyId(executor);

            var executeResult = await spService.Execute(new Sp_GetNestedCategoryByParentIdAndCompanyIdInput {CategoryId = categoryId,CompanyId = companyId});
            try
            {
                return _mapper.Map<IEnumerable<Sp_GetNestedCategoryByParentIdAndCompanyIdOutput>, IEnumerable<CategoryData>>(executeResult);
            }
            catch (AutoMapperMappingException autoMapperException)
            {
                if (autoMapperException.InnerException != null) throw autoMapperException.InnerException;
                return null;
            }
        }

        public async Task<CategoryData> GetById(int categoryId)
        {
            var executor = _dapperExecutorFactory.CreateDapperExecutor<Sp_GetCategoryByIdInput, Sp_GetCategoryByIdOutput>();
            var spService = new Sp_GetCategoryById(executor);

            var executeResult = await spService.Execute(new Sp_GetCategoryByIdInput { CategoryId = categoryId });
            try
            {
                return _mapper.Map<Sp_GetCategoryByIdOutput, CategoryData>(executeResult.FirstOrDefault());
            }
            catch (AutoMapperMappingException autoMapperException)
            {
                if (autoMapperException.InnerException != null) throw autoMapperException.InnerException;
                return null;
            }
        }
    }
}