using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.Deceased;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders;

public static class DeceasedJsonBuilder
{
    public static JsonObject Build(DeceasedResourceRepresentation deceased)
    {
        var deceasedJson = new JsonObject();
        deceasedJson.Add("title", deceased.Title);
        deceasedJson.Add("firstName", deceased.FirstName);
        deceasedJson.Add("lastName", deceased.LastName);
        deceasedJson.Add("middleName", deceased.MiddleName);
        deceasedJson.Add("otherNames", new JsonArray(deceased.OtherNames.Select(x => (JsonNode)JsonValue.Create(x)!).ToArray()));
        deceasedJson.Add("maidenName", deceased.MaidenName);
        deceasedJson.Add("dateOfBirth", deceased.DateOfBirth);
        deceasedJson.Add("niNumber", deceased.NiNumber);
        deceasedJson.Add("occupation", deceased.Occupation);
        deceasedJson.Add("address", deceased.Address is null ? null : AddressJsonBuilder.Build(deceased.Address));
        deceasedJson.Add("willDetails", deceased.WillDetails is null ? null : WillDetailsJsonBuilder.Build(deceased.WillDetails));
        deceasedJson.Add("dateOfDeath", deceased.DateOfDeath);
        deceasedJson.Add("placeOfDeath", deceased.PlaceOfDeath);
        deceasedJson.Add("domicile", deceased.Domicile);
        deceasedJson.Add("otherDomicile", deceased.OtherDomicile);
        deceasedJson.Add("hasDeathCertificate", deceased.HasDeathCertificate);
        deceasedJson.Add("ihtReferenceNumber", deceased.IhtReferenceNumber);
        deceasedJson.Add("uniqueTaxpayerReference", deceased.UniqueTaxpayerReference);
        deceasedJson.Add("hasSurvivingSpouseOrCivilPartner", deceased.HasSurvivingSpouseOrCivilPartner);
        deceasedJson.Add("maritalStatus", deceased.MaritalStatus?.ToString());
        deceasedJson.Add("maritalStatusDate", deceased.MaritalStatusDate);
        deceasedJson.Add("notes", deceased.Notes);

        return deceasedJson;
    }
}