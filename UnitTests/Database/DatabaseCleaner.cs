namespace UnitTests.Database;

using Dapper;
using Microsoft.Data.Sqlite;
using UnitTests.Helpers;

[TestClass]
public static class DatabaseCleaner
{
    private const string DeleteDocumentsQuery = "DELETE FROM Documents";

    private const string DeleteTagsQuery = "DELETE FROM Tags";

    [TestCleanup]
    public static async Task CleanDatabase()
    {
        var connection = new SqliteConnection(DatabaseHelpers.ConnectionString);
        await connection.OpenAsync();

        await connection.ExecuteAsync(DeleteTagsQuery);
        await connection.ExecuteAsync(DeleteDocumentsQuery);

        await connection.CloseAsync();
    }
}
