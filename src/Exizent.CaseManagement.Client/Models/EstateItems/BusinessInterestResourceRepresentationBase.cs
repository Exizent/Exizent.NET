namespace Exizent.CaseManagement.Client.Models.EstateItems;

public abstract class BusinessInterestResourceRepresentationBase : EstateItemResourceRepresentationBase, IHasAddress, IHasJointOwners, IRealisable
{
    public string? BusinessName { get; set; }
    public decimal? ExecutorEstimatedValue { get; set; }
    public decimal? SurveyorFormalValue { get; set; }
    public string? FormalValuationBy { get; set; }
    public string? Description { get; set; }
    public bool IsJointlyOwned { get; set; }
    public bool? IsPassedToSurvivingJointOwner { get; set; }
    public AddressResourceRepresentation Address { get; set; } = null!;
    public bool IsHeritable { get; set; }
    public bool IsValidForInheritanceTax { get; set; }
    public decimal? GrossSaleProceeds { get; set; }
    public EstateItemRealisationResourceRepresentation Realisation { get; set; } = null!;
    public IReadOnlyList<Guid> JointOwnerIds { get; set; } = Array.Empty<Guid>();
}