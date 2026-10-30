using Dahomey.Json.Attributes;

namespace Exizent.CaseManagement.Client.Models.EstateItems;

[JsonDiscriminator(nameof(EstateItemType.Cash))]
public class CashResourceRepresentation : EstateItemResourceRepresentation
{
    public AddressResourceRepresentation Address { get; init; } = null!;
    public decimal ProportionOwned { get; init; }
    public bool? IsPassedToSurvivingJointOwner { get; init; }
    public string? NotPassedDetails { get; init; }
    public string Description { get; init; } = null!;
    public decimal Value { get; init; }
    public bool IsValidForInheritanceTax { get; init; }
    public bool? IsCharityDonation { get; init; }
    public EstateItemRealisationResourceRepresentation Realisation { get; init; } = null!;
    public IReadOnlyList<Guid> JointOwnerIds { get; init; } = Array.Empty<Guid>();
}
