using Dahomey.Json.Attributes;

namespace Exizent.CaseManagement.Client.Models.EstateItems;

[JsonDiscriminator(nameof(EstateItemType.MoneyOwed))]
public class MoneyOwedResourceRepresentation : EstateItemResourceRepresentation
{
    public AddressResourceRepresentation Address { get; init; } = null!;
    public decimal ProportionOwned { get; init; }
    public bool? IsPassedToSurvivingJointOwner { get; init; }
    public string? NotPassedDetails { get; init; }
    public MoneyOwedClassification Classification { get; init; }
    public string Institution { get; init; } = null!;
    public string Description { get; init; } = null!;
    public decimal? ExpectedValue { get; init; }
    public bool IsValidForInheritanceTax { get; init; }
    public bool GenerateIht409 { get; init; }
    public EstateItemRealisationResourceRepresentation Realisation { get; init; } = null!;
    public IReadOnlyList<Guid> JointOwnerIds { get; init; } = Array.Empty<Guid>();
}