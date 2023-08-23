namespace Exizent.CaseManagement.Client.Models.EstateItems;

public abstract class InvestmentBondResourceRepresentationBase : EstateItemResourceRepresentationBase, IHasAddress, IStandardJointOwnership, IRealisable, ICanBeValidForIht
{
    protected InvestmentBondResourceRepresentationBase(): base(EstateItemType.InvestmentBond){}

    public AddressResourceRepresentation Address { get; set; } = null!;
    public string? Provider { get; set; }
    public string? AccountNumber { get; set; }
    public bool HasChargeableEventOccurred { get; set; }
    public decimal ProportionOwned { get; set; }
    public bool? IsPassedToSurvivingJointOwner { get; set; }
    public string? NotPassedDetails { get; set; }
    public decimal? DividendDue { get; set; }
    public decimal? InvestmentValue { get; set; }
    public bool IsValidForInheritanceTax { get; set; }
    public EstateItemRealisationResourceRepresentation Realisation { get; set; } = null!;
    public IReadOnlyList<Guid> JointOwnerIds { get; set; } = Array.Empty<Guid>();
}