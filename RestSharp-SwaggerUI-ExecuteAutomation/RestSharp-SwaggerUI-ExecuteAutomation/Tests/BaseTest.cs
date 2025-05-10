using RestSharp_SwaggerUI_ExecuteAutomation.Utils;
using RestSharp;

namespace RestSharp_SwaggerUI_ExecuteAutomation.Tests;

public class BaseTest
{
    protected RestClient client;
    protected dynamic? testData;
    private string baseUrl;

    [SetUp]
    public async Task Setup()
    {
        // Load API base URL from config.json
        var config = JsonHelper.LoadJson<Dictionary<string, string>>("config.json");
        baseUrl = config["BaseUrl"];

        // Initialize RestClient
        client = new RestClient(new RestClientOptions
        {
            BaseUrl = new Uri(baseUrl)
        });

        // Load pet test data from testdata.json
        testData = JsonHelper.LoadJson<dynamic>("testdata.json");

        await Task.CompletedTask;
    }

    [TearDown]
    public void Cleanup()
    {
        client.Dispose();
    }
}