using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;
using Exizent.CaseManagement.Client.Models.Incomes;
using Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.Incomes;

public class SumsReceivedIncomeJsonBuilder : IncomeBaseJsonBuilder<SumsReceivedIncomeResourceRepresentation>
{
    public SumsReceivedIncomeJsonBuilder(SumsReceivedIncomeResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }

    protected override JsonObject InnerBuild(JsonObject jsonObject,
        SumsReceivedIncomeResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(IncomeType.SumsReceived));
        jsonObject.Add("estateItemId", resourceRepresentation.EstateItemId);
        jsonObject.Add("description", resourceRepresentation.Description);
        jsonObject.Add("at", resourceRepresentation.At);
        jsonObject.Add("source", resourceRepresentation.Source.ToString());

        return jsonObject;
    }
}