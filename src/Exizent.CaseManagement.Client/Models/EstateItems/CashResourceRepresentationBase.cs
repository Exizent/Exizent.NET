namespace Exizent.CaseManagement.Client.Models.EstateItems;

public abstract class CashResourceRepresentationBase : EstateItemResourceRepresentationBase, IHasAddress, IStandardJointOwnership, IRealisable, ICanBeValidForIht
{
    protected CashResourceRepresentationBase() : base(EstateItemType.Cash) { }

    public AddressResourceRepresentation Address { get; set; } = null!;
    public decimal ProportionOwned { get; set; }
    public bool? IsPassedToSurvivingJointOwner { get; set; }
    public string? NotPassedDetails { get; set; }
    public string Description { get; set; } = null!;
    public decimal Value { get; set; }
    public bool IsValidForInheritanceTax { get; set; }
    public bool? IsCharityDonation { get; set; }
    public EstateItemRealisationResourceRepresentation Realisation { get; set; } = null!;
    public IReadOnlyList<Guid> JointOwnerIds { get; set; } = Array.Empty<Guid>();
}
