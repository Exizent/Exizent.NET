using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.Incomes;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.Incomes;

public class ShareholdingIncomeJsonBuilder : IncomeBaseJsonBuilder<ShareholdingIncomeResourceRepresentation>
{
    public ShareholdingIncomeJsonBuilder(ShareholdingIncomeResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }

    protected override JsonObject InnerBuild(JsonObject jsonObject,
        ShareholdingIncomeResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(IncomeType.ShareholdingIncome));
        jsonObject.Add("estateItemId", resourceRepresentation.EstateItemId);
        jsonObject.Add("source", resourceRepresentation.Source.ToString());
        jsonObject.Add("otherSource", resourceRepresentation.OtherSource);
        jsonObject.Add("from", resourceRepresentation.From);
        jsonObject.Add("to", resourceRepresentation.To);
        jsonObject.Add("isTaxable", resourceRepresentation.IsTaxable);
        jsonObject.Add("amountType", resourceRepresentation.AmountType.ToString());
        jsonObject.Add("destination", resourceRepresentation.Destination.ToString());
        jsonObject.Add("destinationCaseItemId", resourceRepresentation.DestinationCaseItemId);
        jsonObject.Add("otherDestination", resourceRepresentation.OtherDestination);
        jsonObject.Add("includesValueUpToDateOfDeath", resourceRepresentation.IncludesValueUpToDateOfDeath);
        jsonObject.Add("valueIncludingUpToDateOfDeath", resourceRepresentation.ValueIncludingUpToDateOfDeath);
        jsonObject.Add("quantityOfShares", resourceRepresentation.QuantityOfShares);
        jsonObject.Add("sharePrice", resourceRepresentation.SharePrice);
        jsonObject.Add("cash", resourceRepresentation.Cash);
        jsonObject.Add("shareholderReferenceNumber", resourceRepresentation.ShareholderReferenceNumber);

        return jsonObject;
    }
}
