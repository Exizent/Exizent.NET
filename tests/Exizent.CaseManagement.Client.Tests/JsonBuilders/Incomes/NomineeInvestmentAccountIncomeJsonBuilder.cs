using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.Incomes;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.Incomes;

public class NomineeInvestmentAccountIncomeJsonBuilder
    : IncomeBaseJsonBuilder<NomineeInvestmentAccountIncomeResourceRepresentation>
{
    public NomineeInvestmentAccountIncomeJsonBuilder(
        NomineeInvestmentAccountIncomeResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }

    protected override JsonObject InnerBuild(JsonObject jsonObject,
        NomineeInvestmentAccountIncomeResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(IncomeType.NomineeInvestmentAccountIncome));
        jsonObject.Add("estateItemId", resourceRepresentation.EstateItemId);
        jsonObject.Add("source", resourceRepresentation.Source.ToString());
        jsonObject.Add("otherSource", resourceRepresentation.OtherSource);
        jsonObject.Add("from", resourceRepresentation.From);
        jsonObject.Add("to", resourceRepresentation.To);
        jsonObject.Add("isTaxable", resourceRepresentation.IsTaxable);
        jsonObject.Add("amountType", resourceRepresentation.AmountType?.ToString());
#pragma warning disable CS0618
        jsonObject.Add("destination", resourceRepresentation.Destination.ToString());
#pragma warning restore CS0618
        jsonObject.Add("destinationCaseItemId", resourceRepresentation.DestinationCaseItemId);
        jsonObject.Add("otherDestination", resourceRepresentation.OtherDestination);
        jsonObject.Add("includesValueUpToDateOfDeath", resourceRepresentation.IncludesValueUpToDateOfDeath);
        jsonObject.Add("valueIncludingUpToDateOfDeath", resourceRepresentation.ValueIncludingUpToDateOfDeath);
        jsonObject.Add("origin", resourceRepresentation.Origin.ToString());
        jsonObject.Add("listedInvestmentId", resourceRepresentation.ListedInvestmentId);
        jsonObject.Add("niaDestination", resourceRepresentation.NiaDestination?.ToString());

        return jsonObject;
    }
}
