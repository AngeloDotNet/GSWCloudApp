using System.Text.Json;

namespace GSWCloudApp.Common.Helpers;

public static class JsonHelpers
{
    public static JsonSerializerOptions JsonSerializer()
    {
        return new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }
}