using Notino.Domain.Commands.DocumentCommands;
using Notino.Domain.Models.Abstraction;

namespace Notino.Data.InMemoryEF.Entities;

public sealed class DocumentEntity : IModel
{
    public DocumentEntity()
    { 
    }

    public DocumentEntity(Guid? id)
    {
        Id = id ?? Guid.NewGuid();
    }

    public DocumentEntity(CreateDocumentCommand command) : this(command.Id)
    {
        Data = command.Data;
        Tags = command.Tags;
    }

    public Guid Id { get; set; }

    public List<string> Tags { get; set; }

    public object Data { get; set; }
}