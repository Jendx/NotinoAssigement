namespace Notino.Data.SQLite.SQL;

internal static class Queries
{
    public const string GetDocuments = "SELECT * FROM Documents";
    public const string InsertDocuments = """
        INSERT INTO Documents (Id, Tags, Data) 
        VALUES (@Id, @Tags, @Data)
        """;

}