namespace Notino.Api;

using Notino.Api.Controllers.Documents;
using Notino.Api.Extensions;

public class Startup
{
    public static void Main(string[] startupArgs)
    {
        var builder = WebApplication.CreateBuilder(startupArgs);

        // Add services to the container.
        builder.Services
            .RegisterServices();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.AddDocumentEndpoints();

        app.Run();
    }
}