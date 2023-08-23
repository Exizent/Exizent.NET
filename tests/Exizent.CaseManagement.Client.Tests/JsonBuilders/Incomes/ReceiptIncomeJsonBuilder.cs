using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;
using Exizent.CaseManagement.Client.Models.Incomes;
using Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.Incomes;

public class ReceiptIncomeJsonBuilder : IncomeBaseJsonBuilder<ReceiptGetIncomeResourceRepresentation>
{
    public ReceiptIncomeJsonBuilder(ReceiptGetIncomeResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }

    protected override JsonObject InnerBuild(JsonObject jsonObject,
        ReceiptGetIncomeResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(IncomeType.Receipt));
        jsonObject.Add("companyName", resourceRepresentation.CompanyName);
        jsonObject.Add("description", resourceRepresentation.Description);
        jsonObject.Add("at", resourceRepresentation.At);

        return jsonObject;
    }
}