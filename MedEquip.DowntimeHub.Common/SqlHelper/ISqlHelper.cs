using Dapper;

namespace MedEquip.DowntimeHub.Common.SqlHelper
{
    public interface ISqlHelper
    {
        Task<T?> GetSingleAsync<T>(string storedProcedureName,DynamicParameters? parameters = null) where T : class;
        Task<T?> ExecuteScalarAsync<T>(string storedProcedureName,DynamicParameters parameters);
        Task ExecuteAsync(string storedProcedureName, DynamicParameters parameters);
        Task<IEnumerable<T>> QueryAsync<T>(string storedProcedureName, DynamicParameters? parameters = null);
        Task<T?> QueryFirstOrDefaultAsync<T>(string storedProcedureName,DynamicParameters? parameters = null); 
        Task<int> ExecuteRawSqlAsync(string sql, object? parameters = null);
        Task<SqlMapper.GridReader> QueryMultipleAsync(string storedProcedureName, DynamicParameters? parameters = null);
        Task<TResult> QueryMultipleAsync<TResult>(string storedProcedureName, Func<SqlMapper.GridReader, Task<TResult>> readFunc, DynamicParameters? parameters = null);
    }
}
