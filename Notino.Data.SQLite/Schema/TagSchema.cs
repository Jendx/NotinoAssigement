namespace Notino.Domain.Models;

using Notino.Data.SQLite.SQL;
using Notino.Domain.Attributes;
using Notino.Domain.Models.Abstraction;

[Query(InsertQuery = Queries.InsertTag, UpdateQuery = Queries.UpdateTag, GetQuery = Queries.GetTag)]
public sealed class TagSchema : IModel
{
    public TagSchema() 
    { 
    }

    public TagSchema(Guid? id)
    {
        Id = id ?? Guid.NewGuid();
    }

    public TagSchema(string tag, Guid documentId, Guid? id = null) : this(id)
    {
        DocumentId = documentId;
        Tag = tag; 
    }

    public Guid Id { get; set; }

    public Guid DocumentId { get; set; }

    public string Tag { get; set; }
}