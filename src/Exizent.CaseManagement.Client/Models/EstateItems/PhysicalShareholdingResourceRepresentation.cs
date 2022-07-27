using Dahomey.Json.Attributes;

namespace Exizent.CaseManagement.Client.Models.EstateItems;

[JsonDiscriminator(nameof(EstateItemType.PhysicalShareholding))]
public class PhysicalShareholdingResourceRepresentation : EstateItemResourceRepresentation
{
    public AddressResourceRepresentation Address { get; init; } = null!;
    public string? ShareholdingName { get; init; }
    public string? ShareholderReferenceNumber { get; init; }
    public decimal? Quantity { get; init; }
    public string? Description { get; init; }
    public decimal? Price { get; init; }
    public decimal ProportionOwned { get; init; }
    public bool? IsPassedToSurvivingJointOwner { get; init; }
    public string? NotPassedDetails { get; init; }
    public decimal? DividendDue { get; init; }
    public string? ValuationBy { get; init; }
    public bool IsQuotedOnLondonStockExchange { get; init; }
    public EstateItemRealisationResourceRepresentation Realisation { get; init; } = null!;
    public IReadOnlyList<Guid> JointOwnerIds { get; init; } = Array.Empty<Guid>();
    public bool? HadControlOfTheCompany { get; init; }
}
