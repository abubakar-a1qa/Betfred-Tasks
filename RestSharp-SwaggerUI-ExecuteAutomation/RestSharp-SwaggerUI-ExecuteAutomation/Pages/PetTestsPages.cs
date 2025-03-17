using Newtonsoft.Json;
using RestSharp_SwaggerUI_ExecuteAutomation.Models;
using RestSharp;
using System.Net;

namespace RestSharp_SwaggerUI_ExecuteAutomation.Pages;

public class PetTestsPages
{
    private readonly RestClient client;

    public PetTestsPages(RestClient restClient)
    {
        this.client = restClient;
    }

    public async Task<RestResponse<Pet>> CreatePetAsync(Pet pet)
    {
        var request = new RestRequest("pet", Method.Post);
        request.AddJsonBody(pet);
        return await client.ExecuteAsync<Pet>(request);
    }

    public async Task<RestResponse<Pet>> GetPetByIdAsync(int petId)
    {
        var request = new RestRequest($"pet/{petId}", Method.Get);
        return await client.ExecuteAsync<Pet>(request);
    }

    public async Task<RestResponse<Pet>> UpdatePetAsync(Pet pet)
    {
        var request = new RestRequest("pet", Method.Put);
        request.AddJsonBody(pet);
        return await client.ExecuteAsync<Pet>(request);
    }

    public async Task<RestResponse> DeletePetAsync(int petId)
    {
        var request = new RestRequest($"pet/{petId}", Method.Delete);
        return await client.ExecuteAsync(request);
    }

    public async Task<RestResponse<List<Pet>>> FindPetsByStatusAsync(string status)
    {
        var request = new RestRequest("pet/findByStatus", Method.Get);
        request.AddQueryParameter("status", status);
        return await client.ExecuteAsync<List<Pet>>(request);
    }

    public async Task<RestResponse<List<Pet>>> FindPetsByTagsAsync(string[] tags)
    {
        var request = new RestRequest("pet/findByTags", Method.Get);
        foreach (var tag in tags)
        {
            request.AddQueryParameter("tags", tag);
        }
        return await client.ExecuteAsync<List<Pet>>(request);
    }

    public async Task<RestResponse> UpdatePetWithFormDataAsync(int petId, string name, string status)
    {
        var request = new RestRequest($"pet/{petId}", Method.Post);
        request.AddParameter("name", name);
        request.AddParameter("status", status);
        return await client.ExecuteAsync(request);
    }
}