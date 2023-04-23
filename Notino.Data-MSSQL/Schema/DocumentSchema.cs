namespace Notino.Domain.Models;

using Notino.Data.SQLite.SQL;
using Notino.Domain.Attributes;
using Notino.Domain.Commands.DocumentCommands;
using Notino.Domain.Helpers;
using Notino.Domain.Models.Abstraction;
using System.Text.Json;

[Query(InsertQuery = Queries.InsertDocument, GetQuery = Queries.GetDocument)]
public sealed class DocumentSchema : IModel
{
    public DocumentSchema()
    { 
    }

    public DocumentSchema(CreateDocumentCommand model)
    {
        Id = model.Id;
        if (model.Data is not null)
        {
            Data = model.Data.ToByteArray();
        }
    }

    public Guid Id { get; set; }

    public byte[] Data { get; set; }
}