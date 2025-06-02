using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.Company;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders;

public static class CompanyJsonBuilder
{
    public static JsonObject? Build(CompanyResourceRepresentation? company)
    {
        if (company is null)
        {
            return null;
        }
        
        var deceasedJson = new JsonObject
        {
            { "name", company.Name },
            { "contactNumber", company.ContactNumber },
            { "address", CompanyAddressJsonBuilder.Build(company.Address) },
            { "primaryAddress", CompanyAddressJsonBuilder.Build(company.PrimaryAddress) },
        };

        var addresses = new JsonArray(company.Addresses.Select(
          CompanyAddressJsonBuilder.Build).ToArray<JsonNode?>());

        deceasedJson.Add("addresses", addresses);

        return deceasedJson;
    }
}