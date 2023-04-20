namespace Notino.Data.SQLite;

using Microsoft.Data.Sqlite;

internal sealed class DatabaseConnection
{
    private const string connectionString = "Data Source=NotinoAssignment.db;Version=3;";
    /// <summary>
    /// Initializes connection to DataBase. If Database does not exist, it will create new one
    /// </summary>
    public async Task InitDatabaseAsync()
    {
        using var connection = new SqliteConnection(connectionString);
        await connection.OpenAsync();

        await connection.CloseAsync();
    }

}
