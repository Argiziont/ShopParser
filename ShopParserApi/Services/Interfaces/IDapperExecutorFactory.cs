﻿using ShopParserApi.Services.Repositories.Interfaces;

namespace ShopParserApi.Services.Interfaces
{
    public interface IDapperExecutorFactory
    {
        public IDapperExecutor<TIn, TOut> CreateDapperExecutor<TIn, TOut>() where TOut : class;

        public IDapperExecutor<TIn> CreateDapperExecutor<TIn>();

    }
}