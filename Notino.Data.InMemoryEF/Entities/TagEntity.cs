using Notino.Domain.Models.Abstraction;

namespace Notino.Data.InMemoryEF.Entities;

public sealed class TagEntity : IModel
{
    public TagEntity()
    { 
    }

    public TagEntity(Guid? id)
    {
        Id = id ?? Guid.NewGuid();
    }

    public TagEntity(string tag, Guid documentId, Guid? id = null) : this(id)
    {
        DocumentId = documentId;
        Tag = tag;
    }

    public Guid Id { get; set; }

    public Guid DocumentId { get; set; }

    public string Tag { get; set; }
}