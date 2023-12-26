using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

public class DeceasedAssetsTrustEstateItemJsonBuilder : EstateItemJsonBuilder<DeceasedAssetsTrustResourceRepresentation>
{
    public DeceasedAssetsTrustEstateItemJsonBuilder(DeceasedAssetsTrustResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }

    private static JsonObject BuildExemption(
       ExemptionResourceRepresentation exemption)
    {
        var jsonObject = new JsonObject();

        jsonObject.Add("type", (int)exemption.Type);
        jsonObject.Add("value", exemption.Value);
        jsonObject.Add("details", exemption.Details);
        return jsonObject;
    }

    private static JsonObject BuildTrusteeOrSolicitor(
       TrusteeOrSolicitorResourceRepresentation trusteeOrSolicitor)
    {
        var jsonObject = new JsonObject();

        jsonObject.Add("firstName", trusteeOrSolicitor.FirstName);
        jsonObject.Add("lastName", trusteeOrSolicitor.LastName);
        jsonObject.Add("address",  AddressJsonBuilder.Build(trusteeOrSolicitor.Address!));
        return jsonObject;
    }

    private static JsonObject BuildAsset(
      AssetResourceRepresentation asset)
    {
        var jsonObject = new JsonObject();

        jsonObject.Add("description", asset.Description);
        jsonObject.Add("value", asset.Value);
        return jsonObject;
    }

    private static JsonObject BuildLiability(
     LiabilityResourceRepresentation liability)
    {
        var jsonObject = new JsonObject();

        jsonObject.Add("description", liability.Description);
        jsonObject.Add("value", liability.Value);
        return jsonObject;
    }

    private static JsonObject BuildAssetDetails(
     AssetDetailsResourceRepresentation assetDetails)
    {
        var jsonObject = new JsonObject();

        jsonObject.Add("trustAssets", new JsonArray(assetDetails.TrustAssets.Select(BuildAsset).ToArray<JsonNode>()));
        jsonObject.Add("trustLiabilities", new JsonArray(assetDetails.TrustLiabilities.Select(BuildLiability).ToArray<JsonNode>()));
        jsonObject.Add("exemptions", new JsonArray(assetDetails.Exemptions.Select(BuildExemption).ToArray<JsonNode>()));
        jsonObject.Add("isTaxToBePaidNow", assetDetails.IsTaxToBePaidNow);
        return jsonObject;
    }

    protected override JsonObject InnerBuild(JsonObject jsonObject,
        DeceasedAssetsTrustResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(EstateItemType.DeceasedAssetsTrust));
        jsonObject.Add("trustType", (int)resourceRepresentation.TrustType);
        jsonObject.Add("nameOfSettlor", resourceRepresentation.NameOfSettlor);
        jsonObject.Add("settlorDateOfDeathOrSettlementDate", resourceRepresentation.SettlorDateOfDeathOrSettlementDate);
        jsonObject.Add("name", resourceRepresentation.Name);
        jsonObject.Add("nameAndAgeOfPersonReceivingBenefit", resourceRepresentation.NameAndAgeOfPersonReceivingBenefit);
        jsonObject.Add("uniqueTaxReferenceNumber", resourceRepresentation.UniqueTaxReferenceNumber);
        jsonObject.Add("trustCreationDate", resourceRepresentation.TrustCreationDate);
        jsonObject.Add("hasDetailsOfAssets", resourceRepresentation.HasDetailsOfAssets);
        jsonObject.Add("trusteesAndSolicitors", new JsonArray(resourceRepresentation.TrusteesAndSolicitors.Select(BuildTrusteeOrSolicitor).ToArray<JsonNode>()));
        jsonObject.Add("propertyBusinessSharesAssets", BuildAssetDetails(resourceRepresentation.PropertyBusinessSharesAssets));
        jsonObject.Add("otherAssets", BuildAssetDetails(resourceRepresentation.OtherAssets));
        jsonObject.Add("exemptions", new JsonArray(resourceRepresentation.Exemptions.Select(BuildExemption).ToArray<JsonNode>()));
        jsonObject.Add("totalValue", resourceRepresentation.TotalValue);


        return jsonObject;
    }
}