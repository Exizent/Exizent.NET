using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.Deceased;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders;

public static class DeceasedJsonBuilder
{
    public static JsonObject Build(DeceasedResourceRepresentation deceased)
    {
        var deceasedJson = new JsonObject
        {
            { "title", deceased.Title },
            { "firstName", deceased.FirstName },
            { "lastName", deceased.LastName },
            { "middleName", deceased.MiddleName },
            { "otherNames", new JsonArray(deceased.OtherNames.Select(x => (JsonNode)JsonValue.Create(x)!).ToArray()) },
            { "maidenName", deceased.MaidenName },
            { "dateOfBirth", deceased.DateOfBirth },
            { "niNumber", deceased.NiNumber },
            { "occupation", deceased.Occupation },
            { "address", deceased.Address is null ? null : AddressJsonBuilder.Build(deceased.Address) },
            { "willDetails", deceased.WillDetails is null ? null : WillDetailsJsonBuilder.Build(deceased.WillDetails) },
            { "dateOfDeath", deceased.DateOfDeath },
            { "placeOfDeath", deceased.PlaceOfDeath },
            { "domicile", deceased.Domicile },
            { "otherDomicile", deceased.OtherDomicile },
            { "sheriffdom", deceased.Sheriffdom?.ToString() },
            { "otherSheriffdom", deceased.OtherSheriffdom },
            { "hasDeathCertificate", deceased.HasDeathCertificate },
            { "ihtReferenceNumber", deceased.IhtReferenceNumber },
            { "uniqueTaxpayerReference", deceased.UniqueTaxpayerReference },
            { "hasSurvivingSpouseOrCivilPartner", deceased.HasSurvivingSpouseOrCivilPartner },
            { "maritalStatus", deceased.MaritalStatus?.ToString() },
            { "maritalStatusDate", deceased.MaritalStatusDate },
            { "notes", deceased.Notes }
        };

        return deceasedJson;
    }
}