namespace Exizent.CaseManagement.Client.Models.EstateItems;

public class PhysicalShareholdingResourceRepresentationBase : EstateItemResourceRepresentationBase, IStandardJointOwnership, IHasAddress, IRealisable
{
    public AddressResourceRepresentation Address { get; set; } = null!;
    public string? ShareholdingName { get; set; }
    public string? ShareholderReferenceNumber { get; set; }
    public decimal? Quantity { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public decimal ProportionOwned { get; set; }
    public bool? IsPassedToSurvivingJointOwner { get; set; }
    public string? NotPassedDetails { get; set; }
    public decimal? DividendDue { get; set; }
    public string? ValuationBy { get; set; }
    public bool IsQuotedOnLondonStockExchange { get; set; }
    public EstateItemRealisationResourceRepresentation Realisation { get; set; } = null!;
    public IReadOnlyList<Guid> JointOwnerIds { get; set; } = Array.Empty<Guid>();
    public bool HadControlOfTheCompany { get; set; }
}
