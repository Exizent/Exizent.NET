using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

public class InvestmentBondEstateItemJsonBuilder : EstateItemJsonBuilder<InvestmentBondResourceRepresentation>
{
    public InvestmentBondEstateItemJsonBuilder(InvestmentBondResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }
   
    protected override JsonObject InnerBuild(JsonObject jsonObject,
        InvestmentBondResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(EstateItemType.InvestmentBond));
        jsonObject.Add("address", AddressJsonBuilder.Build(resourceRepresentation.Address));
        jsonObject.Add("provider", resourceRepresentation.Provider);
        jsonObject.Add("accountNumber", resourceRepresentation.AccountNumber);
        jsonObject.Add("hasChargeableEventOccurred", resourceRepresentation.HasChargeableEventOccurred);
        jsonObject.Add("proportionOwned", resourceRepresentation.ProportionOwned);
        jsonObject.Add("isPassedToSurvivingJointOwner", resourceRepresentation.IsPassedToSurvivingJointOwner);
        jsonObject.Add("notPassedDetails", resourceRepresentation.NotPassedDetails);
        jsonObject.Add("dividendDue", resourceRepresentation.DividendDue);
        jsonObject.Add("investmentValue", resourceRepresentation.InvestmentValue);
        jsonObject.Add("isValidForInheritanceTax", resourceRepresentation.IsValidForInheritanceTax);
        jsonObject.Add("realisation", resourceRepresentation.Realisation is null ? null : EstateItemRealisationJsonBuilder.Build(resourceRepresentation.Realisation));

        return jsonObject;
    }
}