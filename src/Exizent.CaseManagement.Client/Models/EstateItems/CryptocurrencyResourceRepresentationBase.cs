namespace Exizent.CaseManagement.Client.Models.EstateItems;

public class CryptocurrencyResourceRepresentationBase : EstateItemResourceRepresentationBase, IHasAddress, IStandardJointOwnership, IRealisable
{
    public AddressResourceRepresentation Address { get; set; } = null!;
    public string? TypeOfWallet { get; set; }
    public string? LocationOfWallet { get; set; }
    public string? WalletLoginInformation { get; set; }
    public string? WalletAccessGuide { get; set; }
    public decimal ProportionOwned { get; set; }
    public bool? IsPassedToSurvivingJointOwner { get; set; }
    public string? NotPassedDetails { get; set; }
    public decimal? InvestmentValue { get; set; }
    public decimal? GrossSaleProceeds { get; set; }
    public EstateItemRealisationResourceRepresentation Realisation { get; set; } = null!;
    public IReadOnlyList<Guid> JointOwnerIds { get; set; } = Array.Empty<Guid>();
}