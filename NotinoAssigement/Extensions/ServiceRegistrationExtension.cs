namespace Notino.Api.Extensions;

using Notino.Api.Handlers;
using Notino.Api.Handlers.Abstraction;
using Notino.Data.SQLite.Extensions;
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
            .AddSingleton<IHandler<Document>, CreateDocumentHandler>();

        return services;
    }
}