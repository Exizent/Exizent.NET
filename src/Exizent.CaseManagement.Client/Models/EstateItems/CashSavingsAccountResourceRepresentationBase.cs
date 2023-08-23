namespace Exizent.CaseManagement.Client.Models.EstateItems;

public abstract class CashSavingsAccountResourceRepresentationBase : EstateItemResourceRepresentationBase, IStandardJointOwnership, IHasAddress, IRealisable
{
    protected CashSavingsAccountResourceRepresentationBase(): base(EstateItemType.CashSavingsAccount){}

    public string? Institution { get; set; }
    public string? AccountNumber { get; set; }
    public string? SortCode { get; set; }
    public bool IsFixedTerm { get; set; }
    public bool IsNationalSavingsAndInvestments { get; set; }
    public NationalSavingsAndInvestmentsSavingsProduct? NationalSavingsAndInvestmentsProduct { get; set; }
    public decimal? EstimatedBalance { get; set; }
    public decimal? ConfirmedBalance { get; set; }
    public decimal? InterestUpToDateOfDeath { get; set; }
    public AddressResourceRepresentation Address { get; set; } = null!;
    public bool? IsPassedToSurvivingJointOwner { get; set; }
    public string? NotPassedDetails { get; set; }
    public decimal ProportionOwned { get; set; }
    public EstateItemRealisationResourceRepresentation Realisation { get; set; } = null!;
    public IReadOnlyList<Guid> JointOwnerIds { get; set; } = Array.Empty<Guid>();
}