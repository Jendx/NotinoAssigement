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
            .AddTransient<IDBOperations<DocumentSchema>, DBOperations<DocumentSchema>>()
            .AddTransient<IDBOperations<TagSchema>, DBOperations<TagSchema>>();

        SqlMapper.AddTypeHandler(new GuidTypeHandler());

        return services;
    }
}
