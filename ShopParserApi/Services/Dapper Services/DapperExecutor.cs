using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using ShopParserApi.Services.Dapper_Services.Extensions;
using ShopParserApi.Services.Dapper_Services.Interfaces;
using DapperSqlExtensions = ShopParserApi.Services.Extensions.DapperSqlExtensions;

namespace ShopParserApi.Services.Dapper_Services
{
    public class DapperExecutor<TInParams> : IDapperExecutor<TInParams>
        where TInParams : class
    {
        private readonly string _connectionString;

        public DapperExecutor(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task ExecuteAsync(string spName, TInParams inputParams)
        {
            if (JsonWrapperAttributeExtensions.ContainsAttribute<TInParams>())
            {
                await ExecuteWithJsonInputAsync(spName, inputParams);
            }
            else
            {
                await using SqlConnection connection = new SqlConnection(_connectionString);
                await connection.ExecuteAsync(spName, inputParams, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task ExecuteWithJsonInputAsync(string spName, TInParams inputParams)
        {
            /*
             *  If input is json so we must to know how to deserialize this
             *  Dapper requires input names to be set
             *  We pass this param name through  JsonWrapperAttribute
             *  And create dynamic dictionary wrapper for our object
             */
            await using SqlConnection connection = new SqlConnection(_connectionString);

            DynamicParameters parameters = new DynamicParameters(new Dictionary<string, object>
            {
                {
                    JsonWrapperAttributeExtensions.GetAttributeCustom<TInParams>().StoreProcedureJsonInputName,
                    JsonConvert.SerializeObject(inputParams)
                }
            });

            await connection.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public class DapperExecutor<TInParams, TOutParams> : IDapperExecutor<TInParams, TOutParams>
        where TOutParams : class
        where TInParams : class
    {
        private readonly string _connectionString;

        public DapperExecutor(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<TOutParams>> ExecuteAsync(string spName)
        {
            await using SqlConnection connection = new SqlConnection(_connectionString);

            return await connection.QueryAsync<TOutParams>(spName, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<TOutParams>> ExecuteJsonAsync(string spName)
        {
            await using SqlConnection connection = new SqlConnection(_connectionString);

            return await Task.FromResult(DapperSqlExtensions.QueryJson<TOutParams>(connection, spName,
                commandType: CommandType.StoredProcedure,
                buffered: false));
        }

        async Task<IEnumerable<TOutParams>> IDapperExecutor<TInParams, TOutParams>.ExecuteAsync(string spName,
            TInParams inputParams)
        {
            await using SqlConnection connection = new SqlConnection(_connectionString);

            if (JsonWrapperAttributeExtensions.ContainsAttribute<TInParams>())
                return await ExecuteWithJsonInputAsync(spName, inputParams);

            if (typeof(TInParams) == typeof(EmptyInputParams) || inputParams.Equals(default))
                return await ExecuteAsync(spName);


            return await connection.QueryAsync<TOutParams>(spName, inputParams,
                commandType: CommandType.StoredProcedure);
        }

        async Task<IEnumerable<TOutParams>> IDapperExecutor<TInParams, TOutParams>.ExecuteJsonAsync(string spName,
            TInParams inputParams)
        {
            await using SqlConnection connection = new SqlConnection(_connectionString);

            if (JsonWrapperAttributeExtensions.ContainsAttribute<TInParams>())
                return await ExecuteWithJsonInputAndOutputAsync(spName, inputParams);

            if (typeof(TInParams) == typeof(EmptyInputParams) || inputParams.Equals(default))
                return await ExecuteJsonAsync(spName);

            return await Task.FromResult(DapperSqlExtensions.QueryJson<TOutParams>(connection, spName, inputParams,
                commandType: CommandType.StoredProcedure,
                buffered: false));
        }

        public async Task<IEnumerable<TOutParams>> ExecuteWithJsonInputAsync(string spName, TInParams inputParams)
        {
            /*
             *  If input is json so we must to know how to deserialize this
             *  Dapper requires input names to be set
             *  We pass this param name through  JsonWrapperAttribute
             *  And create dynamic dictionary wrapper for our object
             */
            await using SqlConnection connection = new SqlConnection(_connectionString);

            DynamicParameters parameters = new DynamicParameters(new Dictionary<string, object>
            {
                {
                    JsonWrapperAttributeExtensions.GetAttributeCustom<TInParams>().StoreProcedureJsonInputName,
                    JsonConvert.SerializeObject(inputParams)
                }
            });

            return await connection.QueryAsync<TOutParams>(spName, parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<TOutParams>> ExecuteWithJsonInputAndOutputAsync(string spName,
            TInParams inputParams)
        {
            /*
             *  If input is json so we must to know how to deserialize this
             *  Dapper requires input names to be set
             *  We pass this param name through  JsonWrapperAttribute
             *  And create dynamic dictionary wrapper for our object
             */
            await using SqlConnection connection = new SqlConnection(_connectionString);

            DynamicParameters parameters = new DynamicParameters(new Dictionary<string, object>
            {
                {
                    JsonWrapperAttributeExtensions.GetAttributeCustom<TInParams>().StoreProcedureJsonInputName,
                    JsonConvert.SerializeObject(inputParams)
                }
            });

            return await Task.FromResult(DapperSqlExtensions.QueryJson<TOutParams>(connection, spName, parameters,
                commandType: CommandType.StoredProcedure,
                buffered: false));
        }
    }
}