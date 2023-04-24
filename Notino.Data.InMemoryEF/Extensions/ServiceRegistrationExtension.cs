namespace Notino.Data.InMemoryEF.Extensions;

using Microsoft.Extensions.DependencyInjection;
using Notino.Data.InMemoryEF.Database;
using Notino.Data.InMemoryEF.Entities;
using Notino.Domain.Abstraction;
using Microsoft.EntityFrameworkCore;

public static class ServiceRegistrationExtension
{
    public static IServiceCollection UseInMemoryEFDB(this IServiceCollection services)
    {
        services
            .AddDbContext<NotinoDBContext>(config =>
            {
                config.UseInMemoryDatabase("NotinoInMemoryDatabase");
            })
            .AddScoped<IDBOperations<DocumentEntity>, DBOperations<DocumentEntity>>();

        return services;
    }


}
