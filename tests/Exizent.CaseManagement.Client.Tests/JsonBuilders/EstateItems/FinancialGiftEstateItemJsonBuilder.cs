using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

public class FinancialGiftEstateItemJsonBuilder : EstateItemJsonBuilder<FinancialGiftResourceRepresentation>
{
    public FinancialGiftEstateItemJsonBuilder(FinancialGiftResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }
              
    protected override JsonObject InnerBuild(JsonObject jsonObject,
        FinancialGiftResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(EstateItemType.FinancialGift));
        jsonObject.Add("recipientFirstName", resourceRepresentation.RecipientFirstName);
        jsonObject.Add("recipientSurname", resourceRepresentation.RecipientSurname);
        jsonObject.Add("description", resourceRepresentation.Description);
        jsonObject.Add("grossValue", resourceRepresentation.GrossValue);
        jsonObject.Add("exemptionCategory", resourceRepresentation.ExemptionCategory.ToString());
        jsonObject.Add("exemptionDetails", resourceRepresentation.ExemptionDetails);
        jsonObject.Add("giftType", resourceRepresentation.GiftType.ToString());
        jsonObject.Add("exemptionValue", resourceRepresentation.ExemptionValue);
        jsonObject.Add("dateOfGift", resourceRepresentation.DateOfGift);

        return jsonObject;
    }
}