namespace Notino.Api.Handlers;

using Notino.Api.Handlers.Abstraction;
using Notino.Domain.Abstraction;
using Notino.Domain.Helpers;
using Notino.Domain.Models;
using Notino.Domain.Models.Abstraction;
using System.Diagnostics;

internal sealed class CreateDocumentHandler : IHandler<Document>
{
    private readonly IDBOperations<DocumentSchema> _documents; 
    private readonly IDBOperations<TagSchema> _tags; 

    public CreateDocumentHandler(
        IDBOperations<DocumentSchema> documents,
        IDBOperations<TagSchema> tags)
    {
        _documents = documents is not null ? documents : throw new ArgumentNullException(nameof(documents));
        _tags = tags is not null ? tags : throw new ArgumentNullException(nameof(tags));
    }

    public async Task<Document> HandleAsync(Document model)
    {
        var documentResult = await _documents.InsertAsync(new DocumentSchema(model));
        var result = new Document()
        {
            Id = model.Id,
            Data = (documentResult as DocumentSchema).Data.FromByteArray<object>(),
            Tags = new List<string>()
        };

        await InsertTagsAsync(model, result);

        return result;
    }

    private async Task InsertTagsAsync(Document model, Document result)
    {
        foreach (var tag in model.Tags)
        {
            result.Tags.Add(
                (await _tags.InsertAsync(new TagSchema(tag, model.Id))).Tag);
        }
    }
}