using System.Net;
using RestSharp_SwaggerUI_ExecuteAutomation.Models;
using RestSharp_SwaggerUI_ExecuteAutomation.Pages;

namespace RestSharp_SwaggerUI_ExecuteAutomation.Tests;

public class PetTests : BaseTest
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
        var pet = new Pet(
            (int)testData.petId,
            (string)testData.petName,
            (string)testData.petStatus);
        
        var response = await petPages.CreatePetAsync(pet);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Unable to create pet");
        TestContext.WriteLine("Pet created successfully.");

        // Log the created pet's ID for debugging
        TestContext.WriteLine($"Created Pet ID: {pet.Id}");
    }

    [Test, Order(2)]
    public async Task GetPetById()
    {
        var petId = (int)testData.petId;

        var response = await petPages.GetPetByIdAsync(petId);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Unable to get the Pet because Pet by Id was not found");

        var pet = response.Data;
        Assert.That(pet.Name, Is.EqualTo((string)testData.petName), "Pet name mismatch");
    }

    [Test, Order(3)]
    public async Task UpdatePet()
    {
        var updatedPet = new Pet(
            (int)testData.petId,
            (string)testData.newPetName,
            (string)testData.petStatus);
        
        var response = await petPages.UpdatePetAsync(updatedPet);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Unable to update the pet.");
        
        TestContext.WriteLine("Pet updated successfully.");
    }

    [Test, Order(4)]
    public async Task DeletePet()
    {
        var petId = (int)testData.petId;
        TestContext.WriteLine($"Fetching Pet with ID: {petId}");

        var getPet = await petPages.GetPetByIdAsync(petId);
        Assert.That(getPet.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Pet by Id was not found");
        
        var response = await petPages.DeletePetAsync(petId);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Unable to delete Pet");
        TestContext.WriteLine("Pet deleted successfully.");
    }
}