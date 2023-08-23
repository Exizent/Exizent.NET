namespace Exizent.CaseManagement.Client.Models.EstateItems;

public abstract class BankAccountResourceRepresentationBase : EstateItemResourceRepresentationBase, IStandardJointOwnership, IHasAddress, IRealisable
{
    protected BankAccountResourceRepresentationBase(): base(EstateItemType.BankAccount){}
    public decimal? EstimatedBalance { get; set; }
    public decimal? ConfirmedBalance { get; set; }
    public decimal? InterestUpToDateOfDeath { get; set; }
    public decimal? BankCharges { get; set; }
    public AddressResourceRepresentation Address { get; set; } = null!;
    public bool? IsPassedToSurvivingJointOwner { get; set; }
    public string? SortCode { get; set; }
    public string? AccountNumber { get; set; }
    public string? BuildingSocietyRollNumber { get; set; }
    public string? NameOfBankOrBuildingSociety { get; set; }
    public string? TypeOfAccount { get; set; }
    public string? NotPassedDetails { get; set; }
    public decimal ProportionOwned { get; set; }
    public IReadOnlyList<Guid> JointOwnerIds { get; set; } = Array.Empty<Guid>();
    public EstateItemRealisationResourceRepresentation Realisation { get; set; } = null!;
}