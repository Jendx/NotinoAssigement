namespace UnitTests.Database;

using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Notino.Data.InMemoryEF.Database;
using Notino.Data.InMemoryEF.Entities;
using Notino.Domain.Commands.DocumentCommands;
using Notino.Domain.Models;
using UnitTests.Helpers;

/// <summary>
/// If there was more storages, I would make this class partial and have file for each storage
/// </summary>
[TestClass]
public class DatabaseSeeder
{
    private const string InsertDocumentQuery = $"""
        INSERT INTO Documents ({nameof(DocumentSchema.Id)}, {nameof(DocumentSchema.Data)})
        VALUES (@{nameof(DocumentSchema.Id)}, @{nameof(DocumentSchema.Data)})
        """;

    private const string InsertTagQuery = $"""
        INSERT INTO Tags ({nameof(TagSchema.Id)}, {nameof(TagSchema.Tag)}, {nameof(TagSchema.DocumentId)})
        VALUES (@{nameof(TagSchema.Id)}, @{nameof(TagSchema.Tag)}, @{nameof(TagSchema.DocumentId)})
        """;

    private readonly NotinoDBContext _dbContext;

    private static ServiceProvider _serviceProvider;

    public DatabaseSeeder()
    {
        var services = new ServiceCollection();
        services
            .AddDbContext<NotinoDBContext>(config =>
            {
                config.UseInMemoryDatabase("NotinoInMemoryDatabase");
            });

        _serviceProvider = services.BuildServiceProvider();

        _dbContext = _serviceProvider.GetRequiredService<NotinoDBContext>();
    }

    [TestInitialize]
    public async Task SeedDatabaseAsync()
    {
        var commands = JsonConvert.DeserializeObject<CreateDocumentCommand[]>(File.ReadAllText(DatabaseHelpers.SeedData));

        //Make sure, that DB is clear on Init if the test crashed
        await DatabaseCleaner.CleanDatabase();

        await SeedSQLightAsync(commands);
        await SeedInMemoryEFAsync(commands);
    }

    private async Task SeedSQLightAsync(CreateDocumentCommand[] commands)
    {
        using var connection = new SqliteConnection(DatabaseHelpers.ConnectionString);
        await connection.OpenAsync();

        foreach (var command in commands)
        {
            await connection.ExecuteAsync(InsertDocumentQuery, new DocumentSchema(command));

            foreach(var tag in command.Tags)
            {
                await connection.ExecuteAsync(InsertTagQuery, new TagSchema(tag, command.Id));
            }    
        }

        await connection.CloseAsync();
    }

    private async Task SeedInMemoryEFAsync(CreateDocumentCommand[] commands)
    {
        foreach (var command in commands)
        {
            _dbContext.AddAsync(new DocumentEntity(command));
            
            foreach(var tag in command.Tags)
            {
                _dbContext.AddAsync(new TagEntity(tag, command.Id));
            }
        }
    }
}
