namespace Notino.Api.Handlers.Document;

using Notino.Api.Handlers.Abstraction;
using Notino.Data.InMemoryEF.Entities;
using Notino.Domain.Abstraction;
using Notino.Domain.Commands.DocumentCommands;
using Notino.Domain.Commands.TagCommands;
using Notino.Domain.Helpers;
using Notino.Domain.Models;

internal sealed class UpdateDocumentHandler : IHandler<Document, UpdateDocumentCommand>
{
    private readonly IDBOperations<DocumentSchema> _documents;
    private readonly IDBOperations<TagSchema> _tags;

    private readonly IDBOperations<DocumentEntity> _documentsEF;
    private readonly IDBOperations<TagEntity> _tagsEF;

    public UpdateDocumentHandler(
        IDBOperations<DocumentSchema> documents,
        IDBOperations<TagSchema> tags,
        IDBOperations<DocumentEntity> documentsEF,
        IDBOperations<TagEntity> tagsEF)
    {
        _documents = documents is not null ? documents : throw new ArgumentNullException(nameof(documents));
        _tags = tags is not null ? tags : throw new ArgumentNullException(nameof(tags));

        _documentsEF = documentsEF ?? throw new ArgumentNullException(nameof(documentsEF));
        _tagsEF = tagsEF ?? throw new ArgumentNullException(nameof(tagsEF));
    }

    public async Task<Document> HandleAsync(UpdateDocumentCommand command)
    {
        var documentResult = await _documents.UpdateAsync(new DocumentSchema(command));
        var result = new Document()
        {
            Id = command.Id,
            Data = documentResult.Data.FromByteArray<object>(),
            Tags = new List<string>()
        };

        await UpdateTagsAsync(command, result);

        return result;
    }

    private async Task UpdateTagsAsync(UpdateDocumentCommand command, Document result)
    {
        foreach (var tag in command.Tags)
        {
            result.Tags.Add(
                (await _tags.UpdateAsync(new TagSchema(tag.Tag, command.Id, tag.Id))).Tag);
        }
    }

    private async Task<Document> UpdateDataFromInMemoryEFDBAsync(UpdateDocumentCommand command)
    {
        var documentResult = await _documentsEF.InsertAsync(new DocumentEntity(command));

        var result = new Document()
        {
            Id = command.Id,
            Data = documentResult.Data.FromByteArray<object>(),
            Tags = new List<string>()
        };

        await UpdateTagsEFAsync(command, result);

        return result;
    }

    private async Task UpdateTagsEFAsync(UpdateDocumentCommand command, Document result)
    {
        foreach (var tag in command.Tags)
        {
            result.Tags.Add(
                (await _tagsEF.UpdateAsync(new TagEntity(tag.Tag, command.Id, tag.Id))).Tag);
        }
    }
}