using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models;
using Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;
using Exizent.CaseManagement.Client.Tests.JsonBuilders.Expenses;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders;

public static class CaseJsonBuilder
{
    public static JsonObject Build(CaseResourceRepresentation resourceRepresentation)
    {
        var jsonObject = new JsonObject();

        jsonObject.Add("id", resourceRepresentation.Id);
        jsonObject.Add("deceased", DeceasedJsonBuilder.Build(resourceRepresentation.Deceased));

        var people = new JsonArray(resourceRepresentation.People.Select(
            PersonJsonBuilder.Build).ToArray<JsonNode?>());
        jsonObject.Add("people", people);

        var estateItems = new JsonArray(resourceRepresentation.EstateItems.Select(x =>
            EstateItemJsonBuilderFactory.Create(x).Build()).ToArray<JsonNode?>());

        jsonObject.Add("estateItems", estateItems);

        var expenses = new JsonArray(resourceRepresentation.Expenses.Select(
            ExpenseJsonBuilder.Build).ToArray<JsonNode?>());

        jsonObject.Add("expenses", expenses);

        return jsonObject;
    }
}