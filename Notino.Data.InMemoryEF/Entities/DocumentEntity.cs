namespace Notino.Data.InMemoryEF.Entities;

using Notino.Domain.Commands.DocumentCommands;
using Notino.Domain.Helpers;
using Notino.Domain.Models.Abstraction;

public sealed class DocumentEntity : IModel
{
    public DocumentEntity()
    { 
    }

    public DocumentEntity(Guid? id)
    {
        Id = id ?? Guid.NewGuid();
    }

    public DocumentEntity(Guid id, object data = null)
    {
        Id = id;
        if (data is not null)
        {
            Data = data.ToByteArray();
        }
    }

    public DocumentEntity(CreateDocumentCommand command) : this(command.Id, command.Data)
    {
    }    
    
    public DocumentEntity(UpdateDocumentCommand command) : this(command.Id, command.Data)
    {
    }

    public Guid Id { get; set; }

    public List<TagEntity> Tags { get; set; }

    public byte[] Data { get; set; }
}