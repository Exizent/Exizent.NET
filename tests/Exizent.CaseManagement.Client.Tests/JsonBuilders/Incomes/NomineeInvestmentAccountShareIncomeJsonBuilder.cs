using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.Incomes;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.Incomes;

public class NomineeInvestmentAccountShareIncomeJsonBuilder
    : IncomeBaseJsonBuilder<NomineeInvestmentAccountShareIncomeResourceRepresentation>
{
    public NomineeInvestmentAccountShareIncomeJsonBuilder(
        NomineeInvestmentAccountShareIncomeResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }

    protected override JsonObject InnerBuild(JsonObject jsonObject,
        NomineeInvestmentAccountShareIncomeResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(IncomeType.NomineeInvestmentAccountShareIncome));
        jsonObject.Add("estateItemId", resourceRepresentation.EstateItemId);
        jsonObject.Add("source", resourceRepresentation.Source.ToString());
        jsonObject.Add("otherSource", resourceRepresentation.OtherSource);
        jsonObject.Add("from", resourceRepresentation.From);
        jsonObject.Add("to", resourceRepresentation.To);
        jsonObject.Add("isTaxable", resourceRepresentation.IsTaxable);
        jsonObject.Add("amountType", resourceRepresentation.AmountType?.ToString());
        jsonObject.Add("destinationCaseItemId", resourceRepresentation.DestinationCaseItemId);
        jsonObject.Add("otherDestination", resourceRepresentation.OtherDestination);
        jsonObject.Add("includesValueUpToDateOfDeath", resourceRepresentation.IncludesValueUpToDateOfDeath);
        jsonObject.Add("valueIncludingUpToDateOfDeath", resourceRepresentation.ValueIncludingUpToDateOfDeath);
        jsonObject.Add("listedInvestmentId", resourceRepresentation.ListedInvestmentId);
        jsonObject.Add("quantityOfShares", resourceRepresentation.QuantityOfShares);
        jsonObject.Add("sharePrice", resourceRepresentation.SharePrice);

        return jsonObject;
    }
}
