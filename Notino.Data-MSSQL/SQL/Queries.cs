using Notino.Domain.Models;

namespace Notino.Data.SQLite.SQL;

internal static class Queries
{
    public const string GetDocuments = $"""
        SELECT Documents.{nameof(DocumentSchema.Id)}, Documents.{nameof(DocumentSchema.Data)}, Tags.{nameof(TagSchema.Tag)}
        FROM Documents
        	JOIN Tags ON Documents.id = Tags.DocumentId
        """;

    public const string GetDocument = $"""
        SELECT *
        FROM Documents
        WHERE Documents.Id = @{nameof(DocumentSchema.Id)}
        """;
    public const string InsertDocument = $"""
        INSERT INTO Documents ({nameof(DocumentSchema.Id)}, {nameof(DocumentSchema.Data)}) 
        VALUES (@{nameof(DocumentSchema.Id)}, @{nameof(DocumentSchema.Data)})
        """;

    public const string InsertTag = $"""
        INSERT INTO Tags ({nameof(TagSchema.Id)}, {nameof(TagSchema.Tag)}, {nameof(TagSchema.DocumentId)}) 
        VALUES (@{nameof(TagSchema.Id)}, @{nameof(TagSchema.Tag)}, @{nameof(TagSchema.DocumentId)})
        """;

    public const string GetTag = $"""
        SELECT *
        FROM Tags
        WHERE Tags.Id = @{nameof(TagSchema.Id)}
        """;

}