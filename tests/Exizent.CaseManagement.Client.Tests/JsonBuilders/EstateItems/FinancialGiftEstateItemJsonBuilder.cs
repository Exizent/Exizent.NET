using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

public class FinancialGiftEstateItemJsonBuilder : EstateItemJsonBuilder<FinancialGiftResourceRepresentation>
{
    public FinancialGiftEstateItemJsonBuilder(FinancialGiftResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }

    private static JsonObject BuildGiftExemptionResourceRepresentation(
        GiftExemptionResourceRepresentation resourceRepresentation)
    {
        var jsonObject = new JsonObject();

        jsonObject.Add("category", (int)resourceRepresentation.Category);
        jsonObject.Add("details", resourceRepresentation.Details);
        jsonObject.Add("value", resourceRepresentation.Value);
        return jsonObject;
    }

    protected override JsonObject InnerBuild(JsonObject jsonObject,
        FinancialGiftResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(EstateItemType.FinancialGift));
        jsonObject.Add("preOwnedAssetNumber", resourceRepresentation.PreOwnedAssetNumber);
        jsonObject.Add("dateOfElection", resourceRepresentation.DateOfElection);
        jsonObject.Add("recipientFirstName", resourceRepresentation.RecipientFirstName);
        jsonObject.Add("recipientSurname", resourceRepresentation.RecipientSurname);
        jsonObject.Add("recipientOrganisationName", resourceRepresentation.RecipientOrganisationName);
        jsonObject.Add("description", resourceRepresentation.Description);
        jsonObject.Add("grossValue", resourceRepresentation.GrossValue);
        jsonObject.Add("giftType", resourceRepresentation.GiftType.ToString());
        jsonObject.Add("dateOfGift", resourceRepresentation.DateOfGift);
        jsonObject.Add("relationshipToDeceased", resourceRepresentation.RelationshipToDeceased.ToString());
        jsonObject.Add("otherRelationshipToDeceasedDetails", resourceRepresentation.OtherRelationshipToDeceasedDetails);
        jsonObject.Add("exemptions",new JsonArray(resourceRepresentation.Exemptions.Select(BuildGiftExemptionResourceRepresentation).ToArray<JsonNode>()));
        return jsonObject;
    }
}