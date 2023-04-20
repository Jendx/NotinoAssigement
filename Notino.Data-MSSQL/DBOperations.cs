namespace Notino.Data.SQLite;

using Dapper;
using Microsoft.Data.Sqlite;
using Notino.Data.SQLite.SQL;
using Notino.Domain.Abstraction;
using Notino.Domain.Models.Abstraction;
using System.Threading.Tasks;

internal sealed class DBOperations<TModel> : IDBOperations<TModel>
    where TModel : IModel
{
    private const string connectionString = "Data Source=NotinoAssignment.db;Version=3;";

    public DBOperations()
    {

    }

    public TModel Get()
    {
        throw new NotImplementedException();
    }

    public async Task<TModel> UpdateAsync(TModel data)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> InsertAsync(TModel data)
    {
        using var connection = new SqliteConnection(connectionString);
        await connection.OpenAsync();

        var result = await connection.ExecuteAsync(Queries.InsertDocuments, data);

        await connection.CloseAsync();

        return result > 0;
    }
}
