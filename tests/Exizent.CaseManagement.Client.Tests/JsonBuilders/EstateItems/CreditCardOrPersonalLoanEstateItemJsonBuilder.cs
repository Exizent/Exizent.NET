using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

public class
    CreditCardOrPersonalLoanEstateItemJsonBuilder : EstateItemJsonBuilder<
        CreditCardOrPersonalLoanResourceRepresentation>
{
    public CreditCardOrPersonalLoanEstateItemJsonBuilder(
        CreditCardOrPersonalLoanResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }

    protected override JsonObject InnerBuild(JsonObject jsonObject,
        CreditCardOrPersonalLoanResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(EstateItemType.CreditCardOrPersonalLoan));
        jsonObject.Add("proportionOwned", resourceRepresentation.ProportionOwned);
        jsonObject.Add("isPassedToSurvivingJointOwner", resourceRepresentation.IsPassedToSurvivingJointOwner);
        jsonObject.Add("notPassedDetails", resourceRepresentation.NotPassedDetails);
        jsonObject.Add("provider", resourceRepresentation.Provider);
        jsonObject.Add("accountNumber", resourceRepresentation.AccountNumber);
        jsonObject.Add("hasProviderBeenAdvised", resourceRepresentation.HasProviderBeenAdvised);
        jsonObject.Add("debtValue", resourceRepresentation.DebtValue);
        jsonObject.Add("settlement", EstateItemSettlementJsonBuilder.Build(resourceRepresentation.Settlement));

        return jsonObject;
    }
}