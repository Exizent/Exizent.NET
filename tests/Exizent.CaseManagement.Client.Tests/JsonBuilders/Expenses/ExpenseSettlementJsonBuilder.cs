using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.Expenses;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.Expenses;

public class ExpenseSettlementJsonBuilder
{
    public static JsonObject Build(ExpenseSettlementResourceRepresentation resourceRepresentation)
    {
        return new JsonObject
        {
            { "id", resourceRepresentation.Id },
            { "value", resourceRepresentation.Value },
            { "sourceOfFunds", resourceRepresentation.SourceOfFunds.ToString("G") },
            { "at", resourceRepresentation.At.ToString("O") },
            { "estateItemId", resourceRepresentation.EstateItemId },
            { "personId", resourceRepresentation.PersonId },
            { "generateIht423", resourceRepresentation.GenerateIht423 },
            { "shareSettlement", BuildShareSettlement(resourceRepresentation.ShareSettlement) },
        };
    }

    public static JsonArray Build(List<ExpenseSettlementResourceRepresentation> settlements)
    {
        return new JsonArray(settlements.Select(Build).ToArray<JsonNode>());
    }

    private static JsonNode? BuildShareSettlement(ShareSettlementDetailsResourceRepresentation? shareSettlement)
    {
        if (shareSettlement is null) return null;

        return new JsonObject
        {
            { "quantity", shareSettlement.Quantity },
            { "price", shareSettlement.Price },
            { "paySurplus", BuildPaySurplus(shareSettlement.PaySurplus) },
        };
    }

    private static JsonNode? BuildPaySurplus(PaySurplusDestinationResourceRepresentation? paySurplus)
    {
        if (paySurplus is null) return null;

        return new JsonObject
        {
            { "type", paySurplus.Type?.ToString("G") },
            { "caseItemId", paySurplus.CaseItemId },
            { "other", paySurplus.Other },
        };
    }
}