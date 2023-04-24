namespace Notino.Api.Handlers.Document;

using Notino.Api.Handlers.Abstraction;
using Notino.Data.InMemoryEF.Entities;
using Notino.Data.SQLite.SQL;
using Notino.Domain.Abstraction;
using Notino.Domain.Commands.DocumentCommands;
using Notino.Domain.Models;

internal sealed class GetDocumentHandler : IHandler<Document, GetDocumentCommand>
{
    private readonly IDBOperations<DocumentSchema> _documents;
    private readonly IDBOperations<TagSchema> _tags;
    private readonly IDBOperations<DocumentEntity> _documentsEF;

    public GetDocumentHandler(
        IDBOperations<DocumentSchema> documents,
        IDBOperations<TagSchema> tags,
        IDBOperations<DocumentEntity> _documentsEF)
    {
        _documents = documents ?? throw new ArgumentNullException(nameof(documents));
        _tags = tags ?? throw new ArgumentNullException(nameof(tags));

        _documentsEF = _documentsEF ?? throw new ArgumentNullException(nameof(_documentsEF));
    }

    public async Task<Document> HandleAsync(GetDocumentCommand command)
    {
        var documentResult = (await _documents.GetAsync(new DocumentSchema(command.Id), Queries.GetDocument)).FirstOrDefault();
        if (documentResult is null)
        {
            return null;
        }

        var tagsResult = await _tags.GetAsync(new TagSchema(command.Id), Queries.GetTagsOfDocument);

        if (tagsResult is null)
        {
            return null;
        }

        var result = new Document()
        {
            Id = documentResult.Id,
            Data = documentResult.Data,
            Tags = tagsResult.Select(t => t.Tag).ToList(),
        };

        var efResult = GetDataFromInMemoryEFDBAsync(command);

        return result;
    }

    private async Task<Document> GetDataFromInMemoryEFDBAsync(GetDocumentCommand command)
    {
        var result = (await _documentsEF.GetAsync(new DocumentEntity(command.Id))).FirstOrDefault();

        return new Document()
        {
            Id = result.Id,
            Data = result.Data,
            Tags = result.Tags,
        };
    }
}