using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.Organisations;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders;

public static class OrganisationJsonBuilder
{
    public static JsonObject Build(OrganisationResourceRepresentation resourceRepresentation)
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
            { "name", resourceRepresentation.Name },
            { "type", resourceRepresentation.Type },
            { "companiesHouseNumber", resourceRepresentation.CompaniesHouseNumber },
            { "registeredCharityNumber", resourceRepresentation.RegisteredCharityNumber },
            { "contactNumber", resourceRepresentation.ContactNumber },
            { "emailAddress", resourceRepresentation.EmailAddress },
            { "address", resourceRepresentation.Address is null ? null : AddressJsonBuilder.Build(resourceRepresentation.Address) },
            { "notes", resourceRepresentation.Notes },
            { "bankDetails", resourceRepresentation.BankDetails is null
                ? null
                : BankDetailsJsonBuilder.Build(resourceRepresentation.BankDetails) },
            { "individualSignatoryDetails", resourceRepresentation.IndividualSignatoryDetails is null
                ? null
                :  SignatoryJsonBuilder.Build(resourceRepresentation.IndividualSignatoryDetails) },
            { "isSignatory", resourceRepresentation.IsSignatory }
        };

        return jsonObject;
    }
}