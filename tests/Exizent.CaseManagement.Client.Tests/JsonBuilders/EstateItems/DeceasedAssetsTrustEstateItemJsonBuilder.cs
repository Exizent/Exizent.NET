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
       TrustExemptionResourceRepresentation exemption)
    {
        var jsonObject = new JsonObject
        {
            { "type", (int)exemption.Type },
            { "value", exemption.Value },
            { "details", exemption.Details }
        };

        return jsonObject;
    }

    private static JsonObject BuildTrusteeOrSolicitor(
       TrusteeOrSolicitorResourceRepresentation trusteeOrSolicitor)
    {
        var jsonObject = new JsonObject
        {
            { "name", trusteeOrSolicitor.Name },
            { "address", AddressJsonBuilder.Build(trusteeOrSolicitor.Address!) }
        };

        return jsonObject;
    }

    private static JsonObject BuildAsset(
      TrustAssetResourceRepresentation asset)
    {
        var jsonObject = new JsonObject
        {
            { "description", asset.Description },
            { "value", asset.Value }
        };

        return jsonObject;
    }

    private static JsonObject BuildLiability(
     TrustLiabilityResourceRepresentation trustLiability)
    {
        var jsonObject = new JsonObject
        {
            { "description", trustLiability.Description },
            { "value", trustLiability.Value }
        };

        return jsonObject;
    }

    private static JsonObject BuildAssetDetails(
     TrustAssetDetailsResourceRepresentation assetDetails)
    {
        var jsonObject = new JsonObject
        {
            { "assets", new JsonArray(assetDetails.Assets.Select(BuildAsset).ToArray<JsonNode>()) },
            { "liabilities", new JsonArray(assetDetails.Liabilities.Select(BuildLiability).ToArray<JsonNode>()) },
            { "exemptions", new JsonArray(assetDetails.Exemptions.Select(BuildExemption).ToArray<JsonNode>()) },
            { "isTaxToBePaidNow", assetDetails.IsTaxToBePaidNow }
        };

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
        jsonObject.Add("nameOfPersonReceivingBenefit", resourceRepresentation.NameOfPersonReceivingBenefit);
        jsonObject.Add("ageOfPersonReceivingBenefit", resourceRepresentation.AgeOfPersonReceivingBenefit);
        jsonObject.Add("uniqueTaxReferenceNumber", resourceRepresentation.UniqueTaxReferenceNumber);
        jsonObject.Add("trustCreationDate", resourceRepresentation.TrustCreationDate);
        jsonObject.Add("hasDetailsOfAssets", resourceRepresentation.HasDetailsOfAssets);
        jsonObject.Add("trusteesAndSolicitors", new JsonArray(resourceRepresentation.TrusteesAndSolicitors.Select(BuildTrusteeOrSolicitor).ToArray<JsonNode>()));
        jsonObject.Add("propertyBusinessSharesAssets", BuildAssetDetails(resourceRepresentation.PropertyBusinessSharesAssets));
        jsonObject.Add("otherAssets", BuildAssetDetails(resourceRepresentation.OtherAssets));
        jsonObject.Add("totalValue", resourceRepresentation.TotalValue);
        jsonObject.Add("isTaxOnTotalValueToBePaidNow", resourceRepresentation.IsTaxOnTotalValueToBePaidNow);
        jsonObject.Add("estimatedValue", resourceRepresentation.EstimatedValue);
        jsonObject.Add("capacity", (int?)resourceRepresentation.Capacity);
        jsonObject.Add("trustAssetType", (int?)resourceRepresentation.TrustAssetType);

        return jsonObject;
    }
}