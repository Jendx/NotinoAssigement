namespace Notino.Data.SQLite.SQL;

internal static class Queries
{
    public const string GetDocuments = """
        SELECT Documents.Id, Documents.Data, Tags.Tag
        FROM Documents
        	JOIN Tags ON Documents.id = Tags.DocumentId
        """;

    public const string GetDocument = """
        SELECT *
        FROM Documents
        WHERE Documents.Id = @Id
        """;
    public const string InsertDocument = """
        INSERT INTO Documents (Id, Data) 
        VALUES (@Id, @Data)
        """;

    public const string InsertTag = """
        INSERT INTO Tags (Id, Tag, DocumentId) 
        VALUES (@Id, @Tag, @DocumentId)
        """;

    public const string GetTag = """
        SELECT *
        FROM Tags
        WHERE Tags.Id = @Id
        """;

}