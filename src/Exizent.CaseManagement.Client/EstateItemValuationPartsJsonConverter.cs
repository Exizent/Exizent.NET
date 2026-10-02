using System.Text.Json;
using System.Text.Json.Serialization;
using Exizent.CaseManagement.Client.Models.EstateItemValuations;

namespace Exizent.CaseManagement.Client;

/// <summary>
/// Reads a valuation's subject or details by their <c>type</c>. They are kept out of the shared discriminator
/// registry because both use the same names ("Property"), and the registry allows each name only once.
/// </summary>
internal sealed class EstateItemValuationPartsJsonConverter<TBase> : JsonConverter<TBase> where TBase : class
{
    private readonly IReadOnlyDictionary<string, Type> _types;

    public EstateItemValuationPartsJsonConverter(IReadOnlyDictionary<string, Type> types) => _types = types;

    public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(TBase);

    public override TBase? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
            return null;

        using var document = JsonDocument.ParseValue(ref reader);
        var root = document.RootElement;

        if (!root.TryGetProperty("type", out var discriminator)
            || discriminator.GetString() is not { } name
            || !_types.TryGetValue(name, out var type))
            throw new JsonException($"{typeof(TBase).Name} has no recognised type.");

        return (TBase?)root.Deserialize(type, options);
    }

    public override void Write(Utf8JsonWriter writer, TBase value, JsonSerializerOptions options) =>
        throw new NotSupportedException($"{typeof(TBase).Name} is only ever read.");
}

internal static class EstateItemValuationPartsJsonConverters
{
    public static readonly JsonConverter Subject =
        new EstateItemValuationPartsJsonConverter<EstateItemValuationSubjectResourceRepresentation>(
            new Dictionary<string, Type>
            {
                [nameof(EstateItemValuationSubjectType.Property)] =
                    typeof(PropertyEstateItemValuationSubjectResourceRepresentation)
            });

    public static readonly JsonConverter Details =
        new EstateItemValuationPartsJsonConverter<EstateItemValuationDetailsResourceRepresentation>(
            new Dictionary<string, Type>
            {
                [nameof(EstateItemValuationSubjectType.Property)] =
                    typeof(PropertyEstateItemValuationDetailsResourceRepresentation)
            });
}
