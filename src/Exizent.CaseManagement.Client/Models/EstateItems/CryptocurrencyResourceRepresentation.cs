using Dahomey.Json.Attributes;

namespace Exizent.CaseManagement.Client.Models.EstateItems;

[JsonDiscriminator(nameof(EstateItemType.Cryptocurrency))]
public class CryptocurrencyResourceRepresentation : EstateItemResourceRepresentation
{
    public AddressResourceRepresentation Address { get; init; } = null!;
    public string? TypeOfWallet { get; init; }
    public string? LocationOfWallet { get; init; }
    public string? WalletLoginInformation { get; init; }
    public string? WalletAccessGuide { get; init; }
    public decimal ProportionOwned { get; init; }
    public bool? IsPassedToSurvivingJointOwner { get; init; }
    public string? NotPassedDetails { get; init; }
    public decimal? InvestmentValue { get; init; }
    public decimal? GrossSaleProceeds { get; init; }
}