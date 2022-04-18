using Dahomey.Json.Attributes;

namespace Exizent.CaseManagement.Client.Models.EstateItems;

[JsonDiscriminator(nameof(EstateItemType.PremiumBond))]
public class PremiumBondResourceRepresentation : EstateItemResourceRepresentation
{
    public AddressResourceRepresentation Address { get; init; } = null!;
    public string? BondHolderNumber { get; init; }
    public string? BondNumber { get; init; }
    public decimal? ValueAtDateOfDeath { get; init; }
    public decimal? ValueOfUnclaimedPrizes { get; init; }
}