using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;
using Exizent.CaseManagement.Client.Models.Incomes;
using Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.Incomes;

public class ReceiptIncomeJsonBuilder : IncomeBaseJsonBuilder<ReceiptIncomeResourceRepresentation>
{
    public ReceiptIncomeJsonBuilder(ReceiptIncomeResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }

    protected override JsonObject InnerBuild(JsonObject jsonObject,
        ReceiptIncomeResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(IncomeType.Receipt));
        jsonObject.Add("companyName", resourceRepresentation.CompanyName);
        jsonObject.Add("description", resourceRepresentation.Description);
        jsonObject.Add("at", resourceRepresentation.At);

        return jsonObject;
    }
}