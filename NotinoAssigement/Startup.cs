namespace Notino.Api;

using Notino.Api.Controllers.Documents;
using Notino.Api.Extensions;

public class API
{
    public static void Main()
    {
        var builder = WebApplication.CreateBuilder(Array.Empty<string>());

        // Add services to the container.
        builder.Services.RegisterServices();

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