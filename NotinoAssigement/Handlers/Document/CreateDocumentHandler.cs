namespace Notino.Api.Handlers.Document;

using Notino.Api.Handlers.Abstraction;
using Notino.Data.InMemoryEF;
using Notino.Data.InMemoryEF.Entities;
using Notino.Domain.Abstraction;
using Notino.Domain.Commands.DocumentCommands;
using Notino.Domain.Helpers;
using Notino.Domain.Models;
using Notino.Domain.Models.Abstraction;

internal sealed class CreateDocumentHandler : IHandler<Document, CreateDocumentCommand>
{
    private readonly IDBOperations<DocumentSchema> _documents;
    private readonly IDBOperations<TagSchema> _tags;

    private readonly IDBOperations<DocumentEntity> _documentsEF;
    private readonly IDBOperations<TagEntity> _tagsEF;

    public CreateDocumentHandler(
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
        
        //Just showcase that this project supports multiple DB's 
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
        var documentResult = await _documentsEF.InsertAsync(new DocumentEntity(command));
        
        var result = new Document()
        {
            Id = command.Id,
            Data = documentResult.Data.FromByteArray<object>(),
            Tags = new List<string>()
        };

        await InsertTagsEFAsync(command, result);

        return result;
    }

    private async Task InsertTagsEFAsync(CreateDocumentCommand command, Document result)
    {
        foreach (var tag in command.Tags)
        {
            result.Tags.Add(
                (await _tagsEF.InsertAsync(new TagEntity(tag, command.Id))).Tag);
        }
    }
}