using System.Text.Json;
using System.Text.Json.Serialization;

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
        var jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
            IndentSize = 2,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

        return jsonSerializerOptions;
    }
}
