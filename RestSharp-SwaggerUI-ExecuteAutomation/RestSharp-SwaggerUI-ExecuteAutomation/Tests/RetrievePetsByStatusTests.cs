using System.Net;
using Newtonsoft.Json;
using RestSharp_SwaggerUI_ExecuteAutomation.Models;
using RestSharp_SwaggerUI_ExecuteAutomation.Pages;
using RestSharp;

namespace RestSharp_SwaggerUI_ExecuteAutomation.Tests;

public class RetrievePetsByStatusTests : BaseTest
{
    private PetTestsPages petPages;

    [SetUp]
    public void TestSetup()
    {
        petPages = new PetTestsPages(client);
    }

    [Test, Order(1)]
    public async Task CreatePetWithPendingStatus()
    {
        var pet = new Pet((int)testData.petId, (string)testData.petName, "pending");
        var response = await petPages.CreatePetAsync(pet);

        TestContext.WriteLine($"CreatePetWithPendingStatus Response Code: {response.StatusCode}");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Pet with pending status was not successful");
    }

    [Test, Order(2)]
    public async Task RetrievePetsByStatus()
    {
        var response = await petPages.FindPetsByStatusAsync("pending");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Unable to retrieve pets by pending status");
        
        var pets = response.Data;
        Assert.That(pets, Is.Not.Empty, "No pets found with 'pending' status");
        
        var foundPet = pets.Any(p => p.Id == (int)testData.petId && p.Status == "pending");
        Assert.That(foundPet, Is.True, "Created pet not found in the response");
    }

    [Test, Order(3)]
    public async Task DeletePet()
    {
        var response = await petPages.DeletePetAsync((int)testData.petId);
        TestContext.WriteLine($"DeletePet Response Code: {response.StatusCode}");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Unable to delete Pet");
    }
}