namespace Exizent.CaseManagement.Client.Models.EstateItems;

public abstract class NationalSavingsAndInvestmentsProductResourceRepresentationBase : EstateItemResourceRepresentationBase, IStandardJointOwnership, IRealisable
{
    protected NationalSavingsAndInvestmentsProductResourceRepresentationBase(): base(EstateItemType.NationalSavingsAndInvestmentsProduct){}

    public string AccountNumber { get; set; } = null!;
    public NSIProductType ProductType { get; init; }
    public decimal? ConfirmedBalance { get; set; }
    public decimal? EstimatedBalance { get; set; }
    public decimal? InterestUpToDateOfDeath { get; set; }
    public decimal? IndexLinkedIncrease { get; init; }
    public decimal ProportionOwned { get; set; }
    public bool? IsPassedToSurvivingJointOwner { get; set; }
    public string? NotPassedDetails { get; set; }
    public EstateItemRealisationResourceRepresentation Realisation { get; set; } = null!;
    public IReadOnlyList<Guid> JointOwnerIds { get; set; } = Array.Empty<Guid>();
}