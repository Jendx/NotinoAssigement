namespace Notino.Data.SQLite;

using Dapper;
using Microsoft.Data.Sqlite;
using Notino.Domain.Abstraction;
using Notino.Domain.Attributes;
using Notino.Domain.Models.Abstraction;
using System.Reflection;
using System.Threading.Tasks;

internal sealed class DBOperations<TModel> : IDBOperations<TModel>
    where TModel : IModel, new()
{
    private const string connectionString = @"Data Source=C:..\Notino.Data-MSSQL\Database\NotinoAssignment.db";

    public async Task<IEnumerable<TModel>> GetAsync(string query = null, object parameters = null)
    {
        using var connection = new SqliteConnection(connectionString);
        await connection.OpenAsync();

        var result = await connection.QueryAsync<TModel>(
            query ?? typeof(TModel).GetCustomAttribute<QueryAttribute>().GetQuery,
            parameters);

        await connection.CloseAsync();

        return result;
    }

    public async Task<TModel> UpdateAsync(TModel data)
    {
        using var connection = new SqliteConnection(connectionString);
        await connection.OpenAsync();

        await connection.ExecuteAsync(typeof(TModel).GetCustomAttribute<QueryAttribute>().UpdateQuery, data);
        var updatedData = await connection.QueryAsync<TModel>(typeof(TModel).GetCustomAttribute<QueryAttribute>().GetQuery, data);

        await connection.CloseAsync();

        return updatedData.FirstOrDefault();
    }

    public async Task<TModel> InsertAsync(TModel data)
    {
        using var connection = new SqliteConnection(connectionString);
        await connection.OpenAsync();

        await connection.ExecuteAsync(typeof(TModel).GetCustomAttribute<QueryAttribute>().InsertQuery, data);
        var insertedData = await connection.QueryAsync<TModel>(typeof(TModel).GetCustomAttribute<QueryAttribute>().GetQuery, data);

        await connection.CloseAsync();

        return insertedData.FirstOrDefault();
    }
}
