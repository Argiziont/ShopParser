using Microsoft.Extensions.Configuration;
using ShopParserApi.Services.Dapper_Services;
using ShopParserApi.Services.Dapper_Services.Interfaces;
using ShopParserApi.Services.Interfaces;
using ShopParserApi.Services.Repositories;
using ShopParserApi.Services.Repositories.Interfaces;

namespace ShopParserApi.Services
{
    public class DapperExecutorFactory: IDapperExecutorFactory
    {
        private readonly string _connectionString;

        public DapperExecutorFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("UserDb");
        }
        public IDapperExecutor<TIn, TOut> CreateDapperExecutor<TIn, TOut>() 
            where TOut : class
            where TIn : class
        {
            return new DapperExecutor<TIn, TOut>(_connectionString);

        }
        public IDapperExecutor<TIn> CreateDapperExecutor<TIn>()
            where TIn : class
        {
            return new DapperExecutor<TIn>(_connectionString);
        }
    }
}