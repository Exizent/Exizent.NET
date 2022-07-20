using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

public class MiscellaneousAssetEstateItemJsonBuilder : EstateItemJsonBuilder<MiscellaneousAssetResourceRepresentation>
{
    public MiscellaneousAssetEstateItemJsonBuilder(MiscellaneousAssetResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }
     
    protected override JsonObject InnerBuild(JsonObject jsonObject,
        MiscellaneousAssetResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(EstateItemType.MiscellaneousAsset));
        jsonObject.Add("address", AddressJsonBuilder.Build(resourceRepresentation.Address));
        jsonObject.Add("proportionOwned", resourceRepresentation.ProportionOwned);
        jsonObject.Add("isPassedToSurvivingJointOwner", resourceRepresentation.IsPassedToSurvivingJointOwner);
        jsonObject.Add("notPassedDetails", resourceRepresentation.NotPassedDetails);
        jsonObject.Add("itemClassification", resourceRepresentation.ItemClassification.ToString("G"));
        jsonObject.Add("itemDescription", resourceRepresentation.ItemDescription);
        jsonObject.Add("executorEstimatedValue", resourceRepresentation.ExecutorEstimatedValue);
        jsonObject.Add("formalValuation", resourceRepresentation.FormalValuation);
        jsonObject.Add("formalValuationBy", resourceRepresentation.FormalValuationBy);
        jsonObject.Add("isHeritable", resourceRepresentation.IsHeritable);
        jsonObject.Add("isValidForInheritanceTax", resourceRepresentation.IsValidForInheritanceTax);
        jsonObject.Add("isCharityDonation", resourceRepresentation.IsCharityDonation);
        jsonObject.Add("grossSaleProceeds", resourceRepresentation.GrossSaleProceeds);
        jsonObject.Add("realisation", EstateItemRealisationJsonBuilder.Build(resourceRepresentation.Realisation));
        jsonObject.Add("jointOwnerIds", new JsonArray(resourceRepresentation.JointOwnerIds.Select(x => (JsonNode)JsonValue.Create(x)!).ToArray()));

        return jsonObject;
    }
}