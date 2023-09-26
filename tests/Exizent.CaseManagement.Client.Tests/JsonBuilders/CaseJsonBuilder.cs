using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models;
using Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;
using Exizent.CaseManagement.Client.Tests.JsonBuilders.Expenses;
using Exizent.CaseManagement.Client.Tests.JsonBuilders.Incomes;

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

        if (resourceRepresentation.FinancialGiftsDetails is not null)
        {
            jsonObject.Add("financialGiftsDetails",
                FinancialGiftsDetailsJsonBuilder.Build(resourceRepresentation.FinancialGiftsDetails));
        }

        var company = CompanyJsonBuilder.Build(resourceRepresentation.Company);
        if(company is not null)
        {
            jsonObject.Add("company", company);
        }        
        var people = new JsonArray(resourceRepresentation.People.Select(
            PersonJsonBuilder.Build).ToArray<JsonNode?>());
        jsonObject.Add("people", people);

        var organisations = new JsonArray(resourceRepresentation.Organisations.Select(
            OrganisationJsonBuilder.Build).ToArray<JsonNode?>());
        jsonObject.Add("organisations", organisations);

        var estateItems = new JsonArray(resourceRepresentation.EstateItems.Select(x =>
            EstateItemJsonBuilderFactory.Create(x).Build()).ToArray<JsonNode?>());

        jsonObject.Add("estateItems", estateItems);

        var incomes = new JsonArray(resourceRepresentation.Incomes.Select(x =>
            IncomeJsonBuilderFactory.Create(x).Build()).ToArray<JsonNode?>());

        jsonObject.Add("incomes", incomes);

        var expenses = new JsonArray(resourceRepresentation.Expenses.Select(
            ExpenseJsonBuilder.Build).ToArray<JsonNode?>());
        
        jsonObject.Add("expenses", expenses);

        var documents = new JsonArray(resourceRepresentation.Documents
            .Select(CaseDocumentJsonBuilder.Build).ToArray<JsonNode?>());

        jsonObject.Add("documents", documents);
     
        return jsonObject;
    }
}