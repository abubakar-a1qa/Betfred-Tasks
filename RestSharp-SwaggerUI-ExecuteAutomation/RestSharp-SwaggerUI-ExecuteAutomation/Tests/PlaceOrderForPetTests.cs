using System.Net;
using Newtonsoft.Json;
using RestSharp_SwaggerUI_ExecuteAutomation.Models;
using RestSharp_SwaggerUI_ExecuteAutomation.Pages;
using RestSharp;

namespace RestSharp_SwaggerUI_ExecuteAutomation.Tests;

public class PlaceOrderForPetTests : BaseTest
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

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Expected 200 OK status code");
    }

    [Test, Order(2)]
    public async Task PlaceOrderForPet()
    {
        var order = new 
        { 
            id = 1, 
            petId = testData.petId, 
            quantity = 1, 
            shipDate = DateTime.UtcNow.ToString("o"),
            status = "placed", 
            complete = true
        };
        
        var request = new RestRequest("store/order", Method.Post);
        request.AddJsonBody(order);
        var response = await client.ExecuteAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Order placement was not successful");
        
        // Validate order details
        var orderResponse = JsonConvert.DeserializeObject<dynamic>(response.Content);
        Assert.That((int)orderResponse.petId, Is.EqualTo((int)testData.petId), "Pet ID mismatch in order");
        Assert.That((string)orderResponse.status, Is.EqualTo("placed"), "Order status mismatch");
    }

    [Test, Order(3)]
    public async Task DeletePet()
    {
        var petId = (int)testData.petId;
        var getPet = await petPages.GetPetByIdAsync(petId);
        Assert.That(getPet.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Pet by Id was not found");
        
        var response = await petPages.DeletePetAsync(petId);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Pet deletion was not successful");
    }
}