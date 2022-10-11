using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.Expenses;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.Expenses;

public class ExpenseReimbursementJsonBuilder
{
    public static JsonObject Build(ExpenseReimbursementResourceRepresentation resourceRepresentation)
    {
        var jsonObject = new JsonObject
        {
            { "value", resourceRepresentation.Value },
            { "sourceOfFunds", resourceRepresentation.SourceOfFunds.ToString("G") },
            { "at", resourceRepresentation.At.ToString("O") },
            { "estateItemId", resourceRepresentation.EstateItemId },
            { "personId", resourceRepresentation.PersonId }
        };

        return jsonObject;
    }
}