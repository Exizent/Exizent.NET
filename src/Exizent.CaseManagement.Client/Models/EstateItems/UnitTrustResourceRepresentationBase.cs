namespace Exizent.CaseManagement.Client.Models.EstateItems;

public class UnitTrustResourceRepresentationBase : EstateItemResourceRepresentationBase, IStandardJointOwnership, IRealisable
{
    public AddressResourceRepresentation Address { get; set; } = null!;
    public string? FundManager { get; set; }
    public decimal? UnitsHeld { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal ProportionOwned { get; set; }
    public bool? IsPassedToSurvivingJointOwner { get; set; }
    public string? NotPassedDetails { get; set; }
    public decimal? DividendDue { get; set; }
    public decimal? InvestmentValue { get; set; }
    public EstateItemRealisationResourceRepresentation Realisation { get; set; } = null!;
    public IReadOnlyList<Guid> JointOwnerIds { get; set; } = Array.Empty<Guid>();
}
