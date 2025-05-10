using Newtonsoft.Json;

namespace RestSharp_SwaggerUI_ExecuteAutomation.Models;

public class Tag
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }
}