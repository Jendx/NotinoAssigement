namespace UnitTests.Tests.DocumentTests;

using Microsoft.AspNetCore.Mvc.Testing;
using UnitTests.Database;
using Notino.Api;
using UnitTests.Extensions;

[TestClass]
public sealed class DocumentTests : DatabaseSeeder
{

    private WebApplicationFactory<Startup> _factory = null;

    public DocumentTests()
    {
        _factory = TestContext
            .CreateWebApplicationFactory();
    }

    public TestContext TestContext { get; set; }

    [TestMethod]
    public async Task GetJsonDocumentAsync()
    {
        //Prepare
        var client = _factory.CreateClient();
        client.DefaultRequestHeaders.Add("Accept", "application/json");

        //Act
        var result = await client.GetAsync("documents/3fa85f64-eaea-4562-b3fc-2c963f66abab");
            
        //asserts
        Assert.IsNotNull(result);
    }
}