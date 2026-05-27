namespace Exizent.CaseManagement.Client.Models.EstateItems;

public abstract class LifePolicyResourceRepresentationBase : EstateItemResourceRepresentationBase, IHasAddress, IHasJointOwners, IRealisable, ICanBeValidForIht
{
    protected LifePolicyResourceRepresentationBase(): base(EstateItemType.LifePolicy){}

    public AddressResourceRepresentation Address { get; set; } = null!;
    public string? Provider { get; set; }
    public string? PolicyNumber { get; set; }
    public bool IsValidForInheritanceTax { get; set; }
    public decimal? SumAssured { get; set; }
    public bool PaysOnDeathOfDeceased { get; set; }
    public string? BeneficiaryDetails { get; set; }
    public string? Comments { get; set; }
    public decimal ProportionOwned { get; set; }
    /// <remarks>This property is always null when <see cref="IsPassedToSurvivingJointOwner"/> is false.</remarks>
    public IReadOnlyList<Guid>? JointOwnerIds { get; set; }
    public EstateItemRealisationResourceRepresentation Realisation { get; set; } = null!;
}