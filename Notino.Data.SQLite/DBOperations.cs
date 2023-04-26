namespace Notino.Data.SQLite;

using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Notino.Domain.Abstraction;
using Notino.Domain.Attributes;
using Notino.Domain.Models.Abstraction;
using System.Reflection;
using System.Threading.Tasks;

internal sealed class DBOperations<TModel> : IDBOperations<TModel>
    where TModel : IModel, new()
{
    private readonly string connectionString;

    public DBOperations(IConfiguration configuration)
    {
        connectionString = configuration.GetRequiredSection("ConnectionString").Value;
    }


    public async Task<IEnumerable<TModel>> GetAsync(TModel parameters, string query = null)
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