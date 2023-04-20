namespace Notino.Data.SQLite.Extensions;

using Microsoft.Extensions.DependencyInjection;
using Notino.Domain.Abstraction;
using Notino.Domain.Models;

internal static class RegistrationExtension
{
    public static IServiceCollection RegisterData(this IServiceCollection services)
    {
        services
            .AddSingleton<IDBOperations<Document>, DBOperations<Document>>();

        return services;
    }
}
