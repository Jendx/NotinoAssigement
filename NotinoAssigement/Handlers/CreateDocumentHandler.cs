using Notino.Domain.Models;
using NotinoAssigement.Handlers.Abstraction;

namespace NotinoAssigement.Handlers;

internal sealed class CreateDocumentHandler : IHandler<Document>
{
    public Task<Document> HandleAsync(Document model)
    {
        throw new NotImplementedException();
    }
}
