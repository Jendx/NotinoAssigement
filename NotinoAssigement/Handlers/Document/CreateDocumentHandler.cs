namespace Notino.Api.Handlers.Document;

using Notino.Api.Handlers.Abstraction;
using Notino.Data.InMemoryEF.Entities;
using Notino.Domain.Abstraction;
using Notino.Domain.Commands.DocumentCommands;
using Notino.Domain.Helpers;
using Notino.Domain.Models;

internal sealed class CreateDocumentHandler : IHandler<Document, CreateDocumentCommand>
{
    private readonly IDBOperations<DocumentSchema> _documents;
    private readonly IDBOperations<TagSchema> _tags;
    private readonly IDBOperations<DocumentEntity> _documentsEF;

    public CreateDocumentHandler(
        IDBOperations<DocumentSchema> documents,
        IDBOperations<TagSchema> tags,
        IDBOperations<DocumentEntity> documentsEF)
    {
        _documents = documents ?? throw new ArgumentNullException(nameof(documents));
        _tags = tags ?? throw new ArgumentNullException(nameof(tags));

        _documentsEF = documentsEF ?? throw new ArgumentNullException(nameof(documentsEF));
    }

    public async Task<Document> HandleAsync(CreateDocumentCommand command)
    {
        var documentResult = await _documents.InsertAsync(new DocumentSchema(command));
        var result = new Document()
        {
            Id = command.Id,
            Data = documentResult.Data.FromByteArray<object>(),
            Tags = new List<string>()
        };

        await InsertTagsAsync(command, result);
        var efResult = await InsertDataFromInMemoryEFDBAsync(command);

        return result;
    }

    private async Task InsertTagsAsync(CreateDocumentCommand command, Document result)
    {
        foreach (var tag in command.Tags)
        {
            result.Tags.Add(
                (await _tags.InsertAsync(new TagSchema(tag, command.Id))).Tag);
        }
    }

    private async Task<Document> InsertDataFromInMemoryEFDBAsync(CreateDocumentCommand command)
    {
        var result = await _documentsEF.InsertAsync(new DocumentEntity(command));

        return new Document()
        {
            Id = result.Id,
            Data = result.Data,
            Tags = result.Tags,
        };
    }
}