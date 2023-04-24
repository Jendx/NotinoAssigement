namespace Notino.Api.Controllers.Documents;

using Microsoft.AspNetCore.Mvc;
using Notino.Api.Handlers.Abstraction;
using Notino.Domain.Commands.DocumentCommands;
using Notino.Domain.Enums;
using Notino.Domain.Models;
using Notino.Domain.Serializers;
using System.ComponentModel.DataAnnotations;

public static class DocumentEndpoints
{
    private const string EmptyAcceptHeader = "*/*";
    private const string DefaultHeaderValue = "application/json";

    public static void AddDocumentEndpoints(this WebApplication? app)
    {
        app.MapPost("/documents",
            async (
                [FromBody] [Required] CreateDocumentCommand document,
                [FromServices] IHandler<Document, CreateDocumentCommand> handler) =>
        {
            var result = await handler.HandleAsync(document);

            return Results.Ok(result);
        })
        .WithName("CreateDocument")
        .WithOpenApi();

        app.MapPut(
            "/documents",
            async (
                [FromBody] [Required] UpdateDocumentCommand document,
                [FromServices] IHandler<Document, UpdateDocumentCommand> handler) =>
        {
            var result = await handler.HandleAsync(document);

            return Results.Ok();
        })
        .WithName("UpdateDocument")
        .WithOpenApi();

        app.MapGet(
            "/GetDocuments/{documentId}",
            async (
                [FromRoute] [Required] Guid? documentId, 
                [FromServices] IHandler<Document, GetDocumentCommand> handler,
                 HttpContext httpContext) =>
            {
                var acceptHeader = httpContext.Request.Headers["Accept"].FirstOrDefault(DefaultHeaderValue);
                acceptHeader = acceptHeader is EmptyAcceptHeader or null ? DefaultHeaderValue : acceptHeader;

                var result = await handler.HandleAsync(new GetDocumentCommand()
                {
                    Id = documentId!.Value,
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
