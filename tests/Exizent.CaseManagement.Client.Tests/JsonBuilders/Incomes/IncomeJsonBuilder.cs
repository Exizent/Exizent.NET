using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;
using Exizent.CaseManagement.Client.Models.Incomes;
using Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.Incomes;

public class IncomeJsonBuilder : IncomeBaseJsonBuilder<GetIncomeResourceRepresentation>
{
    public IncomeJsonBuilder(GetIncomeResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }

    protected override JsonObject InnerBuild(JsonObject jsonObject,
        GetIncomeResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(IncomeType.Income));
        jsonObject.Add("estateItemId", resourceRepresentation.EstateItemId);
        jsonObject.Add("from", resourceRepresentation.From);
        jsonObject.Add("to", resourceRepresentation.To);
        jsonObject.Add("otherDestination", resourceRepresentation.OtherDestination);
        jsonObject.Add("destination", resourceRepresentation.Destination.ToString());
        jsonObject.Add("otherSource", resourceRepresentation.OtherSource);
        jsonObject.Add("source", resourceRepresentation.Source.ToString());
        jsonObject.Add("amountType", resourceRepresentation.AmountType.ToString());

        return jsonObject;
    }
}