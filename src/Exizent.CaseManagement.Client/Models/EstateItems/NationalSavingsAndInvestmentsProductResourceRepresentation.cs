using Dahomey.Json.Attributes;

namespace Exizent.CaseManagement.Client.Models.EstateItems;

[JsonDiscriminator(nameof(EstateItemType.NationalSavingsAndInvestmentsProduct))]
public class NationalSavingsAndInvestmentsProductResourceRepresentation : EstateItemResourceRepresentation
{
    public string AccountNumber { get; init; } = null!;
    public NSIProductType? Product { get; init; }
    public decimal? ConfirmedBalance { get; init; }
    public decimal? EstimatedBalance { get; init; }
    public decimal? InterestUpToDateOfDeath { get; init; }
    public decimal? IndexLinkedIncrease { get; init; }
    public decimal ProportionOwned { get; init; }
    public bool? IsPassedToSurvivingJointOwner { get; init; }
    public string? NotPassedDetails { get; init; }
    public EstateItemRealisationResourceRepresentation Realisation { get; init; } = null!;
    public IReadOnlyList<Guid> JointOwnerIds { get; init; } = Array.Empty<Guid>();
}