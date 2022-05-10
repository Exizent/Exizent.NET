using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

public class MiscellaneousDebtEstateItemJsonBuilder : EstateItemJsonBuilder<MiscellaneousDebtResourceRepresentation>
{
    public MiscellaneousDebtEstateItemJsonBuilder(MiscellaneousDebtResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }
      
    protected override JsonObject InnerBuild(JsonObject jsonObject,
        MiscellaneousDebtResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(EstateItemType.MiscellaneousDebt));
        jsonObject.Add("proportionOwned", resourceRepresentation.ProportionOwned);
        jsonObject.Add("isPassedToSurvivingJointOwner", resourceRepresentation.IsPassedToSurvivingJointOwner);
        jsonObject.Add("notPassedDetails", resourceRepresentation.NotPassedDetails);
        jsonObject.Add("isValidForInheritanceTax", resourceRepresentation.IsValidForInheritanceTax);
        jsonObject.Add("description", resourceRepresentation.Description);
        jsonObject.Add("provider", resourceRepresentation.Provider);
        jsonObject.Add("hasProviderBeenAdvised", resourceRepresentation.HasProviderBeenAdvised);
        jsonObject.Add("debtValue", resourceRepresentation.DebtValue);
        jsonObject.Add("settlement", EstateItemSettlementJsonBuilder.Build(resourceRepresentation.Settlement));
 
        return jsonObject;
    }
}