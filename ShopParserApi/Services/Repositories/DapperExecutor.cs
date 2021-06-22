using Dapper;
using Microsoft.Data.SqlClient;
using ShopParserApi.Services.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using ShopParserApi.Services.Extensions;

namespace ShopParserApi.Services.Repositories
{
    public class DapperExecutor<TInParams> : IDapperExecutor<TInParams>
    {
        private readonly string _connectionString;

        public DapperExecutor(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task ExecuteAsync(string spName, TInParams inputParams)
        {
            await using var connection = new SqlConnection(_connectionString);

            await connection.ExecuteAsync(spName, param: inputParams, commandType: CommandType.StoredProcedure);
        }

    }

    public class DapperExecutor<TInParams, TOutParams> : IDapperExecutor<TInParams, TOutParams> where TOutParams : class
    {
        private readonly string _connectionString;

        public DapperExecutor(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<TOutParams>> ExecuteAsync(string spName)
        {
            await using var connection = new SqlConnection(_connectionString);

            return await connection.QueryAsync<TOutParams>(spName, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<TOutParams>> ExecuteJsonAsync(string spName)
        {
            await using var connection = new SqlConnection(_connectionString);

            return await Task.FromResult(connection.QueryJson<TOutParams>(spName, commandType: CommandType.StoredProcedure,
                buffered: false));
        }

        async Task<IEnumerable<TOutParams>> IDapperExecutor<TInParams, TOutParams>.ExecuteAsync(string spName, TInParams inputParams)
        {
            if ((typeof(TInParams) == typeof(EmptyInputParams)) || inputParams.Equals(default))
            {
                return await ExecuteAsync(spName);
            }

            await using var connection = new SqlConnection(_connectionString);

            return await connection.QueryAsync<TOutParams>(spName, param: inputParams, commandType: CommandType.StoredProcedure);
        }

        async Task<IEnumerable<TOutParams>> IDapperExecutor<TInParams, TOutParams>.ExecuteJsonAsync(string spName, TInParams inputParams)
        {
            if ((typeof(TInParams) == typeof(EmptyInputParams)) || inputParams.Equals(default))
            {
                return await ExecuteJsonAsync(spName);
            }

            await using var connection = new SqlConnection(_connectionString);

            return await Task.FromResult(connection.QueryJson<TOutParams>(spName, param: inputParams, commandType: CommandType.StoredProcedure,
                buffered: false));
        }
    }

}