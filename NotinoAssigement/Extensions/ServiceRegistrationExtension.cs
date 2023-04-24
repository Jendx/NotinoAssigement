namespace Notino.Api.Extensions;

using Notino.Api.Handlers.Abstraction;
using Notino.Api.Handlers.Document;
using Notino.Data.SQLite.Extensions;
using Notino.Domain.Commands.DocumentCommands;
using Notino.Domain.Models;

internal static class ServiceRegistrationExtension
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .UseSQLiteDB();


        services.RegisterHandlers();

        return services;
    }

    private static IServiceCollection RegisterHandlers(this IServiceCollection services)
    {
        services
            .AddSingleton<IHandler<Document, CreateDocumentCommand>, CreateDocumentHandler>()
            .AddSingleton<IHandler<Document, GetDocumentCommand>, GetDocumentHandler>()
            .AddSingleton<IHandler<Document, UpdateDocumentCommand>, UpdateDocumentHandler>();

        return services;
    }
}