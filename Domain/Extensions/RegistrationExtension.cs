namespace Notino.Domain.Extensions;

using Microsoft.Extensions.DependencyInjection;
using Notino.Domain.Abstraction;
using Notino.Domain.Models;

internal static class RegistrationExtension
{
    public static IServiceCollection RegisterHandlers(this IServiceCollection services)
    {

        return services;
    }
}
