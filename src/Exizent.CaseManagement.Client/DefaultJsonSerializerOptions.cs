using System.Text.Json;
using System.Text.Json.Serialization;

namespace Exizent.CaseManagement.Client;

internal static class DefaultJsonSerializerOptions
{
    public static JsonSerializerOptions Instance { get; } = CreateJsonSerializerOptions();

    private static JsonSerializerOptions CreateJsonSerializerOptions()
    {
        var options = new JsonSerializerOptions();
        options.Converters.Add(new JsonStringEnumConverter());
        options.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;


        return options;
    }
}