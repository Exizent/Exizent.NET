namespace Exizent.CaseManagement.Client.Models;

public class PutFinancialGiftsDetailsResourceRepresentation
{
    public bool HasMadeGiftsOverAnnualLimit { get; init; }
    public bool HasGivenUpTrustAssetsWithinTimeLimit { get; init; }
    public bool ContinuedToBenefitFromGift { get; init; }
    public bool MadeGiftWhereRecipientDidNotTakeFullPossession { get; init; }
    public bool ElectedThatIhtShouldNotApplyToAssetsPreviouslyOwned { get; init; }
}
