using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

public class StoreCardOrCatalogueAccountEstateItemJsonBuilder : EstateItemJsonBuilder<StoreCardOrCatalogueAccountResourceRepresentation>
{
    public StoreCardOrCatalogueAccountEstateItemJsonBuilder(StoreCardOrCatalogueAccountResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }
          
    protected override JsonObject InnerBuild(JsonObject jsonObject,
        StoreCardOrCatalogueAccountResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(EstateItemType.StoreCardOrCatalogueAccount));
        jsonObject.Add("proportionOwned", resourceRepresentation.ProportionOwned);
        jsonObject.Add("isPassedToSurvivingJointOwner", resourceRepresentation.IsPassedToSurvivingJointOwner);
        jsonObject.Add("notPassedDetails", resourceRepresentation.NotPassedDetails);
        jsonObject.Add("provider", resourceRepresentation.Provider);
        jsonObject.Add("hasProviderBeenAdvised", resourceRepresentation.HasProviderBeenAdvised);
        jsonObject.Add("debtValue", resourceRepresentation.DebtValue);
        jsonObject.Add("settlement", EstateItemSettlementJsonBuilder.Build(resourceRepresentation.Settlement));

        return jsonObject;
    }
}