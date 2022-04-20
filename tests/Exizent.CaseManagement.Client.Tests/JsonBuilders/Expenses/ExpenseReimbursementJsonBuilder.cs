using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.Expenses;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.Expenses;

public class ExpenseReimbursementJsonBuilder
{
    public static JsonObject Build(ExpenseReimbursementResourceRepresentation resourceRepresentation)
    {
        var jsonObject = new JsonObject();
        
        jsonObject.Add("value", resourceRepresentation.Value);
        jsonObject.Add("sourceOfFunds", resourceRepresentation.SourceOfFunds.ToString("G"));
        jsonObject.Add("at", resourceRepresentation.At.ToString("O"));
        jsonObject.Add("estateItemId", resourceRepresentation.EstateItemId);
        jsonObject.Add("personId", resourceRepresentation.PersonId);

        return jsonObject;
    }
}