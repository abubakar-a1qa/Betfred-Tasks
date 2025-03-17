using System.Net;
using RestSharp_SwaggerUI_ExecuteAutomation.Models;
using RestSharp_SwaggerUI_ExecuteAutomation.Pages;
using RestSharp;

namespace RestSharp_SwaggerUI_ExecuteAutomation.Tests;

public class RetrievePetsByTagsTests : BaseTest
{
    private PetTestsPages petPages;

    [SetUp]
    public void TestSetup()
    {
        petPages = new PetTestsPages(client);
    }

    [Test, Order(1)]
    public async Task CreatePetsWithTags()
    {
        var pet1 = new Pet((int)testData.petId, "FastDog", "available") 
        { 
            Tags = new[] { new Tag { Id = 1, Name = "fast" } } 
        };
        var pet2 = new Pet((int)testData.petId + 1, "CuteCat", "available") 
        { 
            Tags = new[] { new Tag { Id = 2, Name = "cute" } } 
        };

        var response1 = await petPages.CreatePetAsync(pet1);
        var response2 = await petPages.CreatePetAsync(pet2);

        Assert.That(response1.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Failed to create pet1");
        Assert.That(response2.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Failed to create pet2");
    }

    [Test, Order(2)]
    public async Task RetrievePetsByTag()
    {
        var response = await petPages.FindPetsByTagsAsync(new[] { "cute" });

        TestContext.WriteLine($"RetrievePetsByTag Response Code: {response.StatusCode}");
        TestContext.WriteLine($"Response Content: {response.Content}");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Expected 200 OK status code");
        
        var pets = response.Data;
        Assert.That(pets, Is.Not.Empty, "No pets found with tag 'cute'");
        Assert.That(pets.Any(p => p.Name == "CuteCat"), "Pet 'CuteCat' not found in response");
    }

    [Test, Order(3)]
    public async Task DeletePets()
    {
        var response1 = await petPages.DeletePetAsync((int)testData.petId);
        var response2 = await petPages.DeletePetAsync((int)testData.petId + 1);

        Assert.That(response1.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Failed to delete pet1");
        Assert.That(response2.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Failed to delete pet2");
    }
}