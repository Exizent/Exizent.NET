using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders;

public class FinancialGiftsDetailsJsonBuilder 
{
    public FinancialGiftsDetailsJsonBuilder(FinancialGiftsDetailsResourceRepresentation resourceRepresentation)
    {
    }
              
    public static JsonObject? Build(FinancialGiftsDetailsResourceRepresentation? resourceRepresentation)
    {
        if (resourceRepresentation is null)
        {
            return null;
        }
        return new JsonObject
        {
            {"type", nameof(EstateItemType.FinancialGift)},
            {"electionForInheritanceTax", resourceRepresentation.ElectionForInheritanceTax},
            {"didDeceasedBenefitFromAnyAssets", resourceRepresentation.DidDeceasedBenefitFromAnyAssets},
            {"didDeceasedMakeAnyGiftsOrTransfers", resourceRepresentation.DidDeceasedMakeAnyGiftsOrTransfers},
            {"didDeceasedMakeUseOfTheAsset", resourceRepresentation.DidDeceasedMakeUseOfTheAsset},
            {"didPersonGiftDidNotTakeFullPossession", resourceRepresentation.DidPersonGiftDidNotTakeFullPossession},
        };
    }
}