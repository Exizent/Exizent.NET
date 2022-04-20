using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.Expenses;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.Expenses;

public class ExpenseJsonBuilder
{
    public static JsonObject Build(ExpenseResourceRepresentation resourceRepresentation)
    {
        var jsonObject = new JsonObject();

        jsonObject.Add("id", resourceRepresentation.Id);
        jsonObject.Add("description", resourceRepresentation.Description);
        jsonObject.Add("caseItemId", resourceRepresentation.CaseItemId);
        jsonObject.Add("value", resourceRepresentation.Value);
        jsonObject.Add("from", resourceRepresentation.From.ToString("O"));
        jsonObject.Add("to", resourceRepresentation.To.ToString("O"));
        jsonObject.Add("supplier", resourceRepresentation.Supplier);
        jsonObject.Add("paidByThirdParty", resourceRepresentation.PaidByThirdParty);
        jsonObject.Add("reimbursement",
            resourceRepresentation.Reimbursement is null
                ? null
                : ExpenseReimbursementJsonBuilder.Build(resourceRepresentation.Reimbursement));
        jsonObject.Add("notes", resourceRepresentation.Notes);


        return jsonObject;
    }
}