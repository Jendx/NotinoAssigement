
using Microsoft.Extensions.DependencyInjection;
using Notino.Domain.Abstraction;
using Notino.Domain.Models;
using Notino.Domain.Models.Abstraction;

namespace Notino.Data.InMemoryEF.Extensions;
internal static class RegistrationExtension
{
    public static IServiceCollection RegisterData(this IServiceCollection services)
    {
        services
            .AddSingleton<IDBOperations<IModel>, DBOperations<IModel>>();

        return services;
    }
}
