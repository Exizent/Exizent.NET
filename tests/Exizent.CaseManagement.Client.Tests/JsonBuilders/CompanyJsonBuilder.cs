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
            { "officePhoneNumber", company.OfficePhoneNumber },
            { "address", CompanyAddressJsonBuilder.Build(company.Address) },
        };

        return deceasedJson;
    }
}