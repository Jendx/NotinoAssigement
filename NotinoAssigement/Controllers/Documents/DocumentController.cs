using Microsoft.AspNetCore.Mvc;
using Notino.Api.Handlers.Abstraction;
using Notino.Domain.Commands.DocumentCommands;
using Notino.Domain.Enums;
using Notino.Domain.Models;
using Notino.Domain.Serializers;

namespace Notino.Api.Controllers.Documents;

public static class DocumentController
{
    private const string EmptyAcceptHeader = "*/*";
    private const string DefaultHeaderValue = "application/json";

    public static void AddDocumentEndpoints(this WebApplication? app)
    {
        app.MapPost("/documents",
            async (CreateDocumentCommand document,
            [FromServices] IHandler<Document, CreateDocumentCommand> handler) =>
        {
            if (document is null)
            {
                return Results.BadRequest("No documents are send");
            }

            await handler.HandleAsync(document);

            return Results.Ok();
        })
        .WithName("CreateDocument")
        .WithOpenApi();

        app.MapPut(
            "/documents",
            async (
                UpdateDocumentCommand document,
                [FromServices] IHandler<Document, UpdateDocumentCommand> handler) =>
        {
            if (document is null)
            {
                return Results.BadRequest("No documents are send");
            }

            await handler.HandleAsync(document);

            return Results.Ok();
        })
        .WithName("UpdateDocument")
        .WithOpenApi();

        app.MapGet(
            "/GetDocuments/{documentId}",
            async (
                [FromRoute] Guid documentId, 
                [FromServices] IHandler<Document, GetDocumentCommand> handler,
                HttpContext httpContext) =>
            {
                if (documentId == Guid.Empty)
                {
                    return Results.NotFound("Invalid Id");
                }

                var acceptHeader = httpContext.Request.Headers["Accept"].FirstOrDefault(DefaultHeaderValue);
                acceptHeader = acceptHeader is EmptyAcceptHeader ? DefaultHeaderValue : acceptHeader;

                var result = await handler.HandleAsync(new GetDocumentCommand()
                {
                    Id = documentId,
                });

                var serializedResult = SerializerFactory
                    .CreateSerializer<Document>(Enum.Parse<DocumentType>(acceptHeader, true))
                    .Serialize(result);

                return Results.Text(serializedResult, acceptHeader);
            })
        .WithName("GetDocument")
        .WithOpenApi();
    }
}
