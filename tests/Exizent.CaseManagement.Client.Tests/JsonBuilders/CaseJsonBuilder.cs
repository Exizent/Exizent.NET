using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models;
using Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;
using Exizent.CaseManagement.Client.Tests.JsonBuilders.Expenses;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders;

public static class CaseJsonBuilder
{
    public static JsonObject Build(CaseResourceRepresentation resourceRepresentation)
    {
        var jsonObject = new JsonObject
        {
            { "id", resourceRepresentation.Id },
            { "deceased", DeceasedJsonBuilder.Build(resourceRepresentation.Deceased) }
        };
        
        var company = CompanyJsonBuilder.Build(resourceRepresentation.Company);
        jsonObject.Add("company", company);
        
        var people = new JsonArray(resourceRepresentation.People.Select(
            PersonJsonBuilder.Build).ToArray<JsonNode?>());
        jsonObject.Add("people", people);

        var organisations = new JsonArray(resourceRepresentation.Organisations.Select(
            OrganisationJsonBuilder.Build).ToArray<JsonNode?>());
        jsonObject.Add("organisations", organisations);

        var estateItems = new JsonArray(resourceRepresentation.EstateItems.Select(x =>
            EstateItemJsonBuilderFactory.Create(x).Build()).ToArray<JsonNode?>());

        jsonObject.Add("estateItems", estateItems);

        var expenses = new JsonArray(resourceRepresentation.Expenses.Select(
            ExpenseJsonBuilder.Build).ToArray<JsonNode?>());

        jsonObject.Add("expenses", expenses);

        return jsonObject;
    }
}