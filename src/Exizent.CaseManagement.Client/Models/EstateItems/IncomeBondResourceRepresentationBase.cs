namespace Exizent.CaseManagement.Client.Models.EstateItems;

public abstract class IncomeBondResourceRepresentationBase : EstateItemResourceRepresentationBase, IHasAddress, IStandardJointOwnership, IRealisable
{
    public AddressResourceRepresentation Address { get; set; } = null!;
    public string? AccountNumber { get; set; }
    public decimal? ConfirmedBalance { get; set; }
    public decimal? EstimatedBalance { get; set; }
    public decimal? InterestUpToDateOfDeath { get; set; }
    public bool IsNationalSavingsAndInvestments { get; set; }
    public decimal ProportionOwned { get; set; }
    public bool? IsPassedToSurvivingJointOwner { get; set; }
    public string? NotPassedDetails { get; set; }
    public EstateItemRealisationResourceRepresentation Realisation { get; set; } = null!;
    public IReadOnlyList<Guid> JointOwnerIds { get; set; } = Array.Empty<Guid>();
}