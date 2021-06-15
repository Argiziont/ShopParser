using ShopParserApi.Services.Repositories.Interfaces;

namespace ShopParserApi.Services.Interfaces
{
    public interface IDapperExecutorFactory
    {
        public IDapperExecutor<TIn, TOut> CreateDapperExecutor<TIn, TOut>();
        public IDapperExecutor<TIn> CreateDapperExecutor<TIn>();

    }
}