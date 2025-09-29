using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.People;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders;

public static class PersonJsonBuilder
{
    public static JsonObject Build(PersonResourceRepresentation resourceRepresentation)
    {
        var jsonObject = new JsonObject
        {
            { "id", resourceRepresentation.Id },
            {
                "roles",
                new JsonArray(resourceRepresentation.Roles.Select(x => JsonValue.Create(x.ToString("G")))
                    .ToArray<JsonNode?>())
            },
            { "executorStatus", resourceRepresentation.ExecutorStatus?.ToString() },
            { "title", resourceRepresentation.Title },
            { "firstName", resourceRepresentation.FirstName },
            { "lastName", resourceRepresentation.LastName },
            { "middleName", resourceRepresentation.MiddleName },
            { "otherNames", new JsonArray(resourceRepresentation.OtherNames?.Select(x => JsonValue.Create(x)).ToArray<JsonNode?>() ??
                                          Array.Empty<JsonNode>()) },
            { "relationshipToDeceased", resourceRepresentation.RelationshipToDeceased?.ToString() },
            { "otherRelationshipToDeceased", resourceRepresentation.OtherRelationshipToDeceased },
            { "dateOfBirth", resourceRepresentation.DateOfBirth },
            { "dateOfDeath", resourceRepresentation.DateOfDeath },
            { "contactNumber", resourceRepresentation.ContactNumber },
            { "emailAddress", resourceRepresentation.EmailAddress },
            { "occupation", resourceRepresentation.Occupation },
            { "niNumber", resourceRepresentation.NiNumber },
            { "address", resourceRepresentation.Address is null ? null : AddressJsonBuilder.Build(resourceRepresentation.Address) },
            { "notes", resourceRepresentation.Notes },
            { "bankDetails", resourceRepresentation.BankDetails is null
                ? null
                : BankDetailsJsonBuilder.Build(resourceRepresentation.BankDetails) },
            { "entitlement", resourceRepresentation.Entitlement is null
                ? null
                : EntitlementJsonBuilder.Build(resourceRepresentation.Entitlement) },
            { "isSignatory", resourceRepresentation.IsSignatory },
            { "placeOfMarriage", resourceRepresentation.PlaceOfMarriage },
            { "dateOfMarriageOrCivilPartnership", resourceRepresentation.DateOfMarriageOrCivilPartnership },
            { "createdAt", resourceRepresentation.CreatedAt }
        };

        return jsonObject;
    }
}