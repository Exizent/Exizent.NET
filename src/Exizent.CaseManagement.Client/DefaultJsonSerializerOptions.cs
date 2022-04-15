using System.Text.Json;
using System.Text.Json.Serialization;
using Dahomey.Json;
using Dahomey.Json.Serialization.Conventions;
using Exizent.CaseManagement.Client.Models.EstateItems;

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
        
        var jsonSerializerOptions = options.SetupExtensions();
        var registry = jsonSerializerOptions.GetDiscriminatorConventionRegistry();
        registry.ClearConventions();
        registry.RegisterConvention(new DefaultDiscriminatorConvention<string>(jsonSerializerOptions, "type"));
        registry.RegisterType<BankAccountResourceRepresentation>();

        return options;
    }
}