using Dahomey.Json.Attributes;

namespace Exizent.CaseManagement.Client.Models.EstateItems;

[JsonDiscriminator(nameof(EstateItemType.BusinessInterest))]
public class BusinessInterestResourceRepresentation : EstateItemResourceRepresentation
{
    public string BusinessName { get; init; } = null!;
    public decimal? ExecutorEstimatedValue { get; init; }
    public decimal? SurveyorFormalValue { get; init; }
    public string? FormalValuationBy { get; init; }
    public string Description { get; init; } = null!;
    public bool IsJointlyOwned { get; init; }
    public bool? IsPassedToSurvivingJointOwner { get; init; }
    public AddressResourceRepresentation Address { get; init; } = null!;
    public bool IsHeritable { get; init; }
    public bool IsValidForInheritanceTax { get; init; }
    public decimal? GrossSaleProceeds { get; init; }
    public EstateItemRealisationResourceRepresentation Realisation { get; init; } = null!;
}