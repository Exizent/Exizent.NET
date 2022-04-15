using System.Linq;
using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.People;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders;

public static class PersonJsonBuilder
{
    public static JsonObject Build(PersonResourceRepresentation resourceRepresentation)
    {
        var jsonObject = new JsonObject();

        jsonObject.Add("id", resourceRepresentation.Id);
        jsonObject.Add("roles", new JsonArray(resourceRepresentation.Roles.Select(x => JsonValue.Create(x.ToString("G"))).ToArray()));
        jsonObject.Add("executorStatus", resourceRepresentation.ExecutorStatus?.ToString());
        jsonObject.Add("title", resourceRepresentation.Title);
        jsonObject.Add("firstName", resourceRepresentation.FirstName);
        jsonObject.Add("lastName", resourceRepresentation.LastName);
        jsonObject.Add("middleName", resourceRepresentation.MiddleName);
        jsonObject.Add("otherNames", new JsonArray(resourceRepresentation.OtherNames.Select(x => JsonValue.Create(x)).ToArray()));
        jsonObject.Add("relationshipToDeceased", resourceRepresentation.RelationshipToDeceased?.ToString());
        jsonObject.Add("otherRelationshipToDeceased", resourceRepresentation.OtherRelationshipToDeceased);
        jsonObject.Add("dateOfBirth", resourceRepresentation.DateOfBirth);
        jsonObject.Add("dateOfDeath", resourceRepresentation.DateOfDeath);
        jsonObject.Add("contactNumber", resourceRepresentation.ContactNumber);
        jsonObject.Add("emailAddress", resourceRepresentation.EmailAddress);
        jsonObject.Add("occupation", resourceRepresentation.Occupation);
        jsonObject.Add("niNumber", resourceRepresentation.NiNumber);
        jsonObject.Add("address",  resourceRepresentation.Address is null ? null : AddressJsonBuilder.Build(resourceRepresentation.Address));
        jsonObject.Add("notes", resourceRepresentation.Notes);
        jsonObject.Add("bankDetails", resourceRepresentation.BankDetails is null ? null : BankDetailsJsonBuilder.Build(resourceRepresentation.BankDetails));

        return jsonObject;
    }
}