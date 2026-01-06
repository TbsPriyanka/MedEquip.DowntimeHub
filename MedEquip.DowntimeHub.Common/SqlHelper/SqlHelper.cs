using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace MedEquip.DowntimeHub.Common.SqlHelper
{
    public class SqlHelper : ISqlHelper
    {
        private readonly string _connectionString;

        public SqlHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString(ConstantData.MedEquipDowntimeHubConnectionString)!;
        }

        private IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);

        public async Task<T?> GetSingleAsync<T>(string storedProcedureName,DynamicParameters? parameters = null) where T : class
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<T>(
                storedProcedureName,
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<T?> ExecuteScalarAsync<T>(string storedProcedureName,DynamicParameters parameters)
        {
            using var connection = CreateConnection();

            return await connection.ExecuteScalarAsync<T?>(
                storedProcedureName,
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task ExecuteAsync(string storedProcedureName,DynamicParameters parameters)
        {
            using var connection = CreateConnection();
            await connection.ExecuteAsync(
                storedProcedureName,
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string storedProcedureName,DynamicParameters? parameters = null)
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<T>(
                storedProcedureName,
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<T?> QueryFirstOrDefaultAsync<T>(string storedProcedureName,DynamicParameters? parameters = null)
        {
            using var connection = CreateConnection();

            var result = await connection.QueryAsync<T>(
                storedProcedureName,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result.FirstOrDefault();
        }

        public async Task<int> ExecuteRawSqlAsync(string sql,object? parameters = null)
        {
            using var connection = CreateConnection();
            return await connection.ExecuteAsync(
                sql,
                parameters,
                commandType: CommandType.Text
            );
        }

        public async Task<SqlMapper.GridReader> QueryMultipleAsync(string storedProcedureName,DynamicParameters? parameters = null)
        {
            var connection = CreateConnection();
            return await connection.QueryMultipleAsync(
                storedProcedureName,
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<TResult> QueryMultipleAsync<TResult>(string storedProcedureName,Func<SqlMapper.GridReader, Task<TResult>> readFunc,DynamicParameters? parameters = null)
        {
            using var connection = CreateConnection();
            using var multi = await connection.QueryMultipleAsync(
                storedProcedureName,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return await readFunc(multi);
        }
    }
}