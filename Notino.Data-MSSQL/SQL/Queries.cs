using Notino.Domain.Models;

namespace Notino.Data.SQLite.SQL;

public static class Queries
{
    public const string GetDocuments = $"""
        SELECT Documents.{nameof(DocumentSchema.Id)}, Documents.{nameof(DocumentSchema.Data)}, Tags.{nameof(TagSchema.Tag)}
        FROM Documents
        	JOIN Tags ON Documents.{nameof(DocumentSchema.Id)} = Tags.{nameof(TagSchema.DocumentId)}
        """;

    public const string GetDocument = $"""
        SELECT *
        FROM Documents
        WHERE Documents.{nameof(DocumentSchema.Id)} = @{nameof(DocumentSchema.Id)}
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
        WHERE Tags.{nameof(TagSchema.Id)} = @{nameof(TagSchema.Id)}
        """;

    public const string GetTagsOfDocument = $"""
        SELECT *
        FROM Tags
        WHERE Tags.{nameof(TagSchema.DocumentId)} = @{nameof(DocumentSchema.Id)}
        """;

}