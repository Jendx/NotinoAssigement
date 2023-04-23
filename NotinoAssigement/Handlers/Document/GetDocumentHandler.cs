namespace Notino.Api.Handlers.Document;

using Notino.Api.Handlers.Abstraction;
using Notino.Data.SQLite.SQL;
using Notino.Domain.Abstraction;
using Notino.Domain.Commands.DocumentCommands;
using Notino.Domain.Models;

internal sealed class GetDocumentHandler : IHandler<Document, GetDocumentCommand>
{
    private readonly IDBOperations<DocumentSchema> _documents;
    private readonly IDBOperations<TagSchema> _tags;

    public GetDocumentHandler(
        IDBOperations<DocumentSchema> documents,
        IDBOperations<TagSchema> tags)
    {
        _documents = documents is not null ? documents : throw new ArgumentNullException(nameof(documents));
        _tags = tags is not null ? tags : throw new ArgumentNullException(nameof(tags));
    }

    public async Task<Document> HandleAsync(GetDocumentCommand model)
    {
        var documentResult = (await _documents.GetAsync(Queries.GetDocument, model)).FirstOrDefault();
        if (documentResult is null)
        {
            return null;
        }

        var tagsResult = await _tags.GetAsync(Queries.GetTagsOfDocument, model);

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

        return result;
    }
}