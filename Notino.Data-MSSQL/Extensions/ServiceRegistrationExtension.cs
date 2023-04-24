namespace Notino.Data.SQLite.Extensions;

using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Notino.Data.SQLite.TypeHandlers;
using Notino.Domain.Abstraction;
using Notino.Domain.Models;
using Notino.Domain.Models.Abstraction;

public static class RegistrationExtension
{
    public static IServiceCollection UseSQLiteDB(this IServiceCollection services)
    {
        services
            .AddScoped<IDBOperations<DocumentSchema>, DBOperations<DocumentSchema>>()
            .AddScoped<IDBOperations<TagSchema>, DBOperations<TagSchema>>();

        RegisterTypeHandlers();

        return services;
    }

    private static void RegisterTypeHandlers()
    {
        SqlMapper.AddTypeHandler(new GuidTypeHandler());
    }
}
