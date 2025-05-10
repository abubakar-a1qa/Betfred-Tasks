using System.Net;
using RestSharp_SwaggerUI_ExecuteAutomation.Models;
using RestSharp_SwaggerUI_ExecuteAutomation.Pages;

namespace RestSharp_SwaggerUI_ExecuteAutomation.Tests;

public class UpdatePetStatusTests : BaseTest
{
    private PetTestsPages petPages;

    [SetUp]
    public void TestSetup()
    {
        petPages = new PetTestsPages(client);
    }

    [Test, Order(1)]
    public async Task CreatePet()
    {
        var pet = new Pet((int)testData.petId, (string)testData.petName, "available");
        var response = await petPages.CreatePetAsync(pet);

        TestContext.WriteLine($"CreatePet Response Code: {response.StatusCode}");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Expected 200 OK status code");
    }

    [Test, Order(2)]
    public async Task UpdatePetStatus()
    {
        var updatedPet = new Pet((int)testData.petId, (string)testData.petName, "sold");
        var response = await petPages.UpdatePetAsync(updatedPet);

        TestContext.WriteLine($"UpdatePetStatus Response Code: {response.StatusCode}");
        TestContext.WriteLine($"Response Content: {response.Content}");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Expected 200 OK status code");
        
        var pet = response.Data;
        Assert.That(pet.Status, Is.EqualTo("sold"), "Pet status was not updated to 'sold'");
    }

    [Test, Order(3)]
    public async Task DeletePet()
    {
        var response = await petPages.DeletePetAsync((int)testData.petId);

        TestContext.WriteLine($"DeletePet Response Code: {response.StatusCode}");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Expected 200 OK status code");
    }
}