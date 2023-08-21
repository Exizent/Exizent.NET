namespace Exizent.CaseManagement.Client.Models.EstateItems;

public class LifePolicyResourceRepresentationBase : EstateItemResourceRepresentationBase, IHasAddress, IHasJointOwners, IRealisable, ICanBeValidForIht
{
    public AddressResourceRepresentation Address { get; set; } = null!;
    public string? Provider { get; set; }
    public string? PolicyNumber { get; set; }
    public bool IsValidForInheritanceTax { get; set; }
    public decimal? SumAssured { get; set; }
    public bool PaysOnDeathOfDeceased { get; set; }
    public string? BeneficiaryDetails { get; set; }
    public string? Comments { get; set; }
    public decimal ProportionOwned { get; set; }
    public IReadOnlyList<Guid> JointOwnerIds { get; set; } = Array.Empty<Guid>();
    public EstateItemRealisationResourceRepresentation Realisation { get; set; } = null!;
}