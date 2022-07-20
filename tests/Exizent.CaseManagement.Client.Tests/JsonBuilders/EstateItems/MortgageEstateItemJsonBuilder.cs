using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

public class MortgageEstateItemJsonBuilder : EstateItemJsonBuilder<MortgageResourceRepresentation>
{
    public MortgageEstateItemJsonBuilder(MortgageResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }
       
    protected override JsonObject InnerBuild(JsonObject jsonObject,
        MortgageResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(EstateItemType.Mortgage));
        jsonObject.Add("proportionOwned", resourceRepresentation.ProportionOwned);
        jsonObject.Add("isPassedToSurvivingJointOwner", resourceRepresentation.IsPassedToSurvivingJointOwner);
        jsonObject.Add("notPassedDetails", resourceRepresentation.NotPassedDetails);
        jsonObject.Add("provider", resourceRepresentation.Provider);
        jsonObject.Add("mortgageType", resourceRepresentation.MortgageType);
        jsonObject.Add("linkedEstateItemId", resourceRepresentation.LinkedEstateItemId);
        jsonObject.Add("debtValue", resourceRepresentation.DebtValue);
        jsonObject.Add("settlement", EstateItemSettlementJsonBuilder.Build(resourceRepresentation.Settlement));
        jsonObject.Add("jointOwnerIds", new JsonArray(resourceRepresentation.JointOwnerIds.Select(x => (JsonNode)JsonValue.Create(x)!).ToArray()));

        return jsonObject;
    }
}