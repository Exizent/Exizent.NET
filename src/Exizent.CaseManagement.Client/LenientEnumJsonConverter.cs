using System.Text.Json;
using System.Text.Json.Serialization;

namespace Exizent.CaseManagement.Client;

/// <summary>
/// Reads an enum from its name, reading a name it does not know as null rather than failing the whole read.
/// Writes the name.
/// </summary>
internal sealed class LenientEnumJsonConverter<TEnum> : JsonConverter<TEnum?> where TEnum : struct, Enum
{
    private static readonly Dictionary<string, TEnum> ValuesByName = Enum.GetNames<TEnum>()
        .ToDictionary(name => name, Enum.Parse<TEnum>, StringComparer.OrdinalIgnoreCase);

    public override TEnum? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String &&
            ValuesByName.TryGetValue(reader.GetString()!, out var value))
        {
            return value;
        }

        reader.Skip();
        return null;
    }

    public override void Write(Utf8JsonWriter writer, TEnum? value, JsonSerializerOptions options)
    {
        if (value is null)
        {
            writer.WriteNullValue();
            return;
        }

        writer.WriteStringValue(value.Value.ToString());
    }
}
