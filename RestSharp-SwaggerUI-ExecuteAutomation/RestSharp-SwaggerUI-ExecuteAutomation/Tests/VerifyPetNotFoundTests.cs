using System.Net;
using RestSharp_SwaggerUI_ExecuteAutomation.Models;
using RestSharp_SwaggerUI_ExecuteAutomation.Pages;
using RestSharp;

namespace RestSharp_SwaggerUI_ExecuteAutomation.Tests;

public class VerifyPetNotFoundTests : BaseTest
{
    private PetTestsPages petPages;
    private readonly int invalidId = 99999999;

    [SetUp]
    public void TestSetup()
    {
        petPages = new PetTestsPages(client);
    }

    [Test]
    public async Task GetNonExistentPet()
    {
        var response = await petPages.GetPetByIdAsync(invalidId);

        TestContext.WriteLine($"GetNonExistentPet Response Code: {response.StatusCode}");
        TestContext.WriteLine($"Response Content: {response.Content}");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound), "Expected 404 Not Found");
    }

    [Test]
    public async Task UpdateNonExistentPetWithFormData()
    {
        var response = await petPages.UpdatePetWithFormDataAsync(invalidId, "GhostPet", "available");

        TestContext.WriteLine($"UpdateNonExistentPetWithFormData Response Code: {response.StatusCode}");
        TestContext.WriteLine($"Response Content: {response.Content}");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound), "Expected 404 Not Found");
    }
}