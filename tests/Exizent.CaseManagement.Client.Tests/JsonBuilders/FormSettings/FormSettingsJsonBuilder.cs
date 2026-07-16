using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.FormSettings;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.FormSettings;

public static class FormSettingsJsonBuilder
{
    public static JsonObject Build(FormSettingsResourceRepresentation resourceRepresentation)
    {
        return new JsonObject
        {
            { "c1", BuildC1(resourceRepresentation.C1) },
        };
    }

    private static JsonObject BuildC1(C1FormSettingsResourceRepresentation c1)
    {
        return new JsonObject
        {
            {
                "registrarsToGroup",
                new JsonArray(c1.RegistrarsToGroup.Select(x => JsonValue.Create(x)).ToArray<JsonNode?>())
            },
        };
    }
}
