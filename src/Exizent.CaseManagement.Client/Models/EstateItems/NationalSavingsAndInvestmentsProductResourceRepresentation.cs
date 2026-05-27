using Dahomey.Json.Attributes;

namespace Exizent.CaseManagement.Client.Models.EstateItems;

[JsonDiscriminator(nameof(EstateItemType.NationalSavingsAndInvestmentsProduct))]
public class NationalSavingsAndInvestmentsProductResourceRepresentation : EstateItemResourceRepresentation
{
    public string AccountNumber { get; init; } = null!;
    public NSIProductType ProductType { get; init; }
    public decimal? ConfirmedBalance { get; init; }
    public decimal? EstimatedBalance { get; init; }
    public decimal? InterestUpToDateOfDeath { get; init; }
    public decimal? IndexLinkedIncrease { get; init; }
    public decimal ProportionOwned { get; init; }
    public bool? IsPassedToSurvivingJointOwner { get; init; }
    public string? NotPassedDetails { get; init; }
    public EstateItemRealisationResourceRepresentation Realisation { get; init; } = null!;
    /// <remarks>This property is always null when <see cref="IsPassedToSurvivingJointOwner"/> is false.</remarks>
    public IReadOnlyList<Guid>? JointOwnerIds { get; init; }
    public AddressResourceRepresentation? Address { get; init; }
}