namespace Notino.Domain.Models;

using Notino.Data.SQLite.SQL;
using Notino.Domain.Attributes;
using Notino.Domain.Commands.DocumentCommands;
using Notino.Domain.Helpers;
using Notino.Domain.Models.Abstraction;

[Query(InsertQuery = Queries.InsertDocument, UpdateQuery = Queries.UpdateDocument, GetQuery = Queries.GetDocument)]
public sealed class DocumentSchema : IModel
{
    public DocumentSchema()
    { 
    }

    public DocumentSchema(Guid id, object data)
    {
        Id = id;
        if (data is not null)
        {
            Data = data.ToByteArray();
        }
    }

    public DocumentSchema(CreateDocumentCommand model) : this(model.Id, model.Data)
    {
    }

    public DocumentSchema(UpdateDocumentCommand model) : this(model.Id, model.Data)
    {
    }

    public Guid Id { get; set; }

    public byte[] Data { get; set; }
}