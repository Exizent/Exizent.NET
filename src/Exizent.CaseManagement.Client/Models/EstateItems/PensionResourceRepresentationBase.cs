namespace Exizent.CaseManagement.Client.Models.EstateItems;

public abstract class PensionResourceRepresentationBase : EstateItemResourceRepresentationBase, IHasAddress, IRealisable, ICanBeValidForIht
{
    
    protected PensionResourceRepresentationBase(): base(EstateItemType.Pension){}

    public AddressResourceRepresentation Address { get; set; } = null!;
    public string? PensionType { get; set; }
    public string? Provider { get; set; }
    public string? PlanReference { get; set; }
    public bool? HasValidNominationForm { get; set; }
    public decimal? DeathBenefitValuePayable { get; set; }
    public string? BeneficiaryDetails { get; set; }
    public bool IsValidForInheritanceTax { get; set; }
    public EstateItemRealisationResourceRepresentation Realisation { get; set; } = null!;
}
