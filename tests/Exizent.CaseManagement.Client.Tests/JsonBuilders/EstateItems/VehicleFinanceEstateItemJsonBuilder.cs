using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

public class VehicleFinanceEstateItemJsonBuilder : EstateItemJsonBuilder<VehicleFinanceResourceRepresentation>
{
    public VehicleFinanceEstateItemJsonBuilder(VehicleFinanceResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }
             
    protected override JsonObject InnerBuild(JsonObject jsonObject,
        VehicleFinanceResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(EstateItemType.VehicleFinance));
        jsonObject.Add("proportionOwned", resourceRepresentation.ProportionOwned);
        jsonObject.Add("isPassedToSurvivingJointOwner", resourceRepresentation.IsPassedToSurvivingJointOwner);
        jsonObject.Add("notPassedDetails", resourceRepresentation.NotPassedDetails);
        jsonObject.Add("provider", resourceRepresentation.Provider);
        jsonObject.Add("hasProviderBeenAdvised", resourceRepresentation.HasProviderBeenAdvised);
        jsonObject.Add("linkedEstateItemId", resourceRepresentation.LinkedEstateItemId);
        jsonObject.Add("debtValue", resourceRepresentation.DebtValue);
        jsonObject.Add("settlement", EstateItemSettlementJsonBuilder.Build(resourceRepresentation.Settlement));
        jsonObject.Add("jointOwnerIds", new JsonArray(resourceRepresentation.JointOwnerIds.Select(x => (JsonNode)JsonValue.Create(x)!).ToArray()));

        return jsonObject;
    }
}