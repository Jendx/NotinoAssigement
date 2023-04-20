namespace Notino.Api.Handlers;

using Notino.Api.Handlers.Abstraction;
using Notino.Domain.Abstraction;
using Notino.Domain.Models;

internal sealed class CreateDocumentHandler : IHandler<Document>
{
    private readonly IDBOperations<Document> _dbOperations; 

    public CreateDocumentHandler(
        IDBOperations<Document> dbOperations)
    {
        _dbOperations = dbOperations is not null ? dbOperations : throw new ArgumentNullException(nameof(dbOperations));
    }


    public async Task<Document> HandleAsync(Document model)
    {
        var data = await _dbOperations.InsertAsync(model);

        return model;
    }
}
