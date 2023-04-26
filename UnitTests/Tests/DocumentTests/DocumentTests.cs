namespace UnitTests.Tests.DocumentTests;

using Microsoft.AspNetCore.Mvc.Testing;
using UnitTests.Database;
using Notino.Api;
using UnitTests.Extensions;
using System.Net.Http.Json;
using Notino.Domain.Models;
using Newtonsoft.Json;
using Notino.Domain.Helpers;

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

    //Tryes to get document from Tests.db
    [TestMethod]
    public async Task GetJsonDocumentAsync()
    {
        //Prepare
        var client = _factory.CreateClient();
        client.DefaultRequestHeaders.Add("Accept", "application/json");

        //Act
        var result = await client.GetFromJsonAsync<object>("documents/f5d9082d-1960-4dc1-95e4-9f3d7d40c530");
        var documentResult = JsonConvert.DeserializeObject<Document>(result.ToString());

        //asserts
        Assert.IsNotNull(documentResult);
        Assert.AreEqual(documentResult.Tags[0], "tag1");
    }

    /// I have created only one basic test. If this was for real app, we should test at least: 
    /// GetXML (Did it return Document? is it valid XML?, are values equal?),
    /// InsertDocument(Is the inserted document == sended, Will it fail if bad Guid is sended? etc...),
    /// UpdateDocument(Are updated values equal to what was sended...)
}