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
            {"continuedToBenefitFromGift", resourceRepresentation.ContinuedToBenefitFromGift},
            {"hasMadeGiftsOverAnnualLimit", resourceRepresentation.HasMadeGiftsOverAnnualLimit},
            {"hasGivenUpTrustAssetsWithinTimeLimit", resourceRepresentation.HasGivenUpTrustAssetsWithinTimeLimit},
            {"madeGiftWhereRecipientDidNotTakeFullPossession", resourceRepresentation.MadeGiftWhereRecipientDidNotTakeFullPossession},
            {"electedThatIhtShouldNotApplyToAssetsPreviouslyOwned", resourceRepresentation.ElectedThatIhtShouldNotApplyToAssetsPreviouslyOwned},
        };
    }
}