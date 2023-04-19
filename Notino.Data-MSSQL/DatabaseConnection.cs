namespace Notino.Data.MSSQL;
using Microsoft.Data.SqlClient;

internal class DatabaseConnection
{
    private const string connectionString = "Data Source=(local);Initial Catalog=master;Integrated Security=True;";
    private const string databaseName = "NotinoAssigenement";

    /// <summary>
    /// Initializes connection to DataBase. If Database does not exist, it will create new one
    /// </summary>
    public void InitDatabase()
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        CreateDataBase(connection);
    }

    private void CreateDataBase(SqlConnection connection)
    {
        using var command = new SqlCommand($"CREATE DATABASE {databaseName}", connection);
        command.ExecuteNonQuery();
    }
}
