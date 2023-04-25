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
    private readonly IDBOperations<TagEntity> _tagsEF;

    public GetDocumentHandler(
        IDBOperations<DocumentSchema> documents,
        IDBOperations<TagSchema> tags,
        IDBOperations<DocumentEntity> documentsEF,
        IDBOperations<TagEntity> tagsEF)
    {
        _documents = documents ?? throw new ArgumentNullException(nameof(documents));
        _tags = tags ?? throw new ArgumentNullException(nameof(tags));

        _documentsEF = documentsEF ?? throw new ArgumentNullException(nameof(documentsEF));
        _tagsEF = tagsEF ?? throw new ArgumentNullException(nameof(tagsEF));
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
        var documentResult = (await _documentsEF.GetAsync(new DocumentEntity())).Where(de => de.Id == command.Id).FirstOrDefault();
        var tagResult = await _tagsEF.GetAsync(new TagEntity());

        return new Document()
        {
            Id = documentResult.Id,
            Data = documentResult.Data,
            Tags = tagResult.Where(te => te.DocumentId == command.Id).Select(te => te.Tag).ToList(),
        };
    }
}