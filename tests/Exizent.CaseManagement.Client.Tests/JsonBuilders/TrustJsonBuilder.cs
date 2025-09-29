using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.Trusts;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders;

public static class TrustJsonBuilder
{
    public static JsonObject Build(TrustResourceRepresentation resourceRepresentation)
    {
        var notes = new JsonArray(resourceRepresentation.Notes.Select(
        NoteJsonBuilder.Build).ToArray<JsonNode?>());
        var jsonObject = new JsonObject
        {
            { "id", resourceRepresentation.Id },
            { "name", resourceRepresentation.Name },
            { "description", resourceRepresentation.Description },
            { "entitlement", resourceRepresentation.Entitlement is null
                ? null
                : EntitlementJsonBuilder.Build(resourceRepresentation.Entitlement) },
            { "showBeneficiariesOnEstateAccounts", resourceRepresentation.ShowBeneficiariesOnEstateAccounts },
            { "showTrusteesOnEstateAccounts", resourceRepresentation.ShowTrusteesOnEstateAccounts },
            { "createdAt", resourceRepresentation.CreatedAt },
            { "updatedAt", resourceRepresentation.UpdatedAt },
            {
                "beneficiaries",
                new JsonArray(resourceRepresentation.Beneficiaries.Select(x => (JsonNode)JsonValue.Create(x)!).ToArray())
            },
            {
                "trustees",
                new JsonArray(resourceRepresentation.Trustees.Select(x => (JsonNode)JsonValue.Create(x)!).ToArray())
            },

            { "notes", notes },
        };

        return jsonObject;
    }
}