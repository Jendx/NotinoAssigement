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
        //Would rename to document. I did not do it becous I wanted to stick to the assigement
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
            "/documents/{documentId}",
            async (
                [FromRoute] [Required] Guid? documentId, 
                [FromServices] IHandler<Document, GetDocumentCommand> handler,
                [FromHeader] string accept) =>
            {
                accept = accept is EmptyAcceptHeader or null ? DefaultHeaderValue : accept;
                var documentType = Enum.Parse<DocumentType>(accept.Split('/')[1], true);

                var result = await handler.HandleAsync(new GetDocumentCommand()
                {
                    Id = documentId!.Value,
                });

                var serializedResult = SerializerFactory
                    .CreateSerializer<Document>(documentType)
                    .Serialize(result);

                return Results.Ok(serializedResult);
            })
        .WithName("GetDocument")
        .WithOpenApi();
    }
}
