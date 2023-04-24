namespace Notino.Api;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Notino.Api.Extensions;
using Notino.Api.Handlers.Abstraction;
using Notino.Domain.Commands.DocumentCommands;
using Notino.Domain.Enums;
using Notino.Domain.Models;
using Notino.Domain.Serializers;
using System.Net.Mime;

public class API
{
    public static void Main()
    {
        var builder = WebApplication.CreateBuilder(Array.Empty<string>());

        // Add services to the container.
        builder.Services.RegisterServices();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }


        app.MapPost("/documents", async (CreateDocumentCommand document, [FromServices] IHandler<Document, CreateDocumentCommand> handler) =>
        {
            if (document is null)
            {
                return Results.BadRequest("No documents are send");
            }

            await handler.HandleAsync(document);

            return Results.Ok();
        })
        .WithName("CreateDocuments")
        .WithOpenApi();

        app.MapGet(
            "/GetDocuments/{documentId}",
            async ([FromRoute] Guid documentId, [FromServices] IHandler<Document, GetDocumentCommand> handler, HttpContext httpContext) =>
        {
            if (documentId == Guid.Empty)
            {
                return Results.NotFound("Invalid Id");
            }

            var acceptHeader = httpContext.Request.Headers["Accept"].FirstOrDefault("application/json");

            var result = await handler.HandleAsync(new GetDocumentCommand()
            {
                Id = documentId,
            });

            var serializedResult = SerializerFactory
                .CreateSerializer<Document>(Enum.Parse<DocumentType>(acceptHeader.Split('/')[1], true))
                .Serialize(result);

            return Results.Text(serializedResult, acceptHeader);
        })
        .WithName("GetDocuments")
        .WithOpenApi();

        app.Run("http://localhost:5000");
    }
}