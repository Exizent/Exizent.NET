using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.Expenses;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.Expenses;

public class ExpenseJsonBuilder
{
    public static JsonObject Build(ExpenseResourceRepresentation resourceRepresentation)
    {
        var jsonObject = new JsonObject
        {
            { "id", resourceRepresentation.Id },
            { "description", resourceRepresentation.Description },
            { "caseItemId", resourceRepresentation.CaseItemId },
            { "value", resourceRepresentation.Value },
            { "from", resourceRepresentation.From.ToString("O") },
            { "to", resourceRepresentation.To.ToString("O") },
            { "supplier", resourceRepresentation.Supplier },
            { "paidByThirdParty", resourceRepresentation.PaidByThirdParty },
            { "reimbursement", resourceRepresentation.Reimbursement is null
                ? null
                : ExpenseReimbursementJsonBuilder.Build(resourceRepresentation.Reimbursement) },
            { "notes", resourceRepresentation.Notes },
            { "purpose", resourceRepresentation.Purpose.ToString() }
        };


        return jsonObject;
    }
}