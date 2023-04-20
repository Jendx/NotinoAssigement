namespace Notino.Api;

using Notino.Domain.Models;

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

        app.UseHttpsRedirection();

        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        app.MapGet("/weatherforecast", () =>
        {
            var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
                .ToArray();
            return forecast;
        })
        .WithName("GetWeatherForecast")
        .WithOpenApi();

        app.MapPost("/documents", async (Document document, CreateDocumentHandler handler) =>
        {
            if (document is null) 
            {
                return Results.BadRequest("No documents are send");
            }

            await handler.HandleAsync(document);

            return Results.Ok();
        })
        .WithName("CreateDocuments")
        .WithOpenApi();

        app.Run();
    }


    internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }

}

