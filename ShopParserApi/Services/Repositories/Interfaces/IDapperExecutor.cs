namespace ShopParserApi.Services.Repositories.Interfaces
{
    public interface IDapperExecutor<in TInParams>
    {
        System.Threading.Tasks.Task ExecuteAsync(string spName, TInParams inputParams);
    }

    public interface IDapperExecutor<in TInParams, TOutParams>
    {
        System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<TOutParams>> ExecuteAsync(string spName, TInParams inputParams);
        System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<TOutParams>> ExecuteAsync(string spName);
    }


    public class EmptyInputParams { }
}