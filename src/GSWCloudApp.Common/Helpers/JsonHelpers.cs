using System.Text.Json;

namespace GSWCloudApp.Common.Helpers;

/// <summary>
/// Provides helper methods for JSON serialization.
/// </summary>
public static class JsonHelpers
{
    /// <summary>
    /// Creates and configures a new instance of <see cref="JsonSerializerOptions"/>.
    /// </summary>
    /// <returns>A configured <see cref="JsonSerializerOptions"/> instance.</returns>
    public static JsonSerializerOptions JsonSerializer()
    {
        return new JsonSerializerOptions
        {
            IndentSize = 2,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }
}
