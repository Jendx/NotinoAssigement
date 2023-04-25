namespace UnitTests.Extensions;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Notino.Api;

internal static class TestContextExtensions
{
    public static WebApplicationFactory<Startup> CreateWebApplicationFactory(this TestContext context)
    {
        return new WebApplicationFactory<Startup>()
            .WithWebHostBuilder(options =>
            {
                options
                    .UseTestServer();
            });
    }
}
