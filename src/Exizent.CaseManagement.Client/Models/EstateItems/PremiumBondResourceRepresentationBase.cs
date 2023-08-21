namespace Exizent.CaseManagement.Client.Models.EstateItems;

public abstract class PremiumBondResourceRepresentationBase : EstateItemResourceRepresentationBase, IHasAddress, IRealisable
{
    public AddressResourceRepresentation Address { get; set; } = null!;
    public string? BondHolderNumber { get; set; }
    public string? BondNumber { get; set; }
    public decimal? ValueAtDateOfDeath { get; set; }
    public decimal? ValueOfUnclaimedPrizes { get; set; }
    public EstateItemRealisationResourceRepresentation Realisation { get; set; } = null!;
}