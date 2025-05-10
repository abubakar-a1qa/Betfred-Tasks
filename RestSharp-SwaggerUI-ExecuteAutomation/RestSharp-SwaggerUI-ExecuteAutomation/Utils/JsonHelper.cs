using Newtonsoft.Json;

namespace RestSharp_SwaggerUI_ExecuteAutomation.Utils;

public static class JsonHelper
{
    public static T? LoadJson<T>(string filePath)
    {
        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", filePath);
        if (!File.Exists(fullPath))
            throw new FileNotFoundException($"JSON file not found: {fullPath}");

        var json = File.ReadAllText(fullPath);
        return JsonConvert.DeserializeObject<T>(json);
    }
}