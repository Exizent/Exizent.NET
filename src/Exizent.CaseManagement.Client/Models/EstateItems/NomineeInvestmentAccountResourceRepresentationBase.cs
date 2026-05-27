namespace Exizent.CaseManagement.Client.Models.EstateItems;

public abstract class NomineeInvestmentAccountResourceRepresentationBase : EstateItemResourceRepresentationBase, IStandardJointOwnership, IHasAddress, ICanBeValidForIht, IRealisable
{
    protected NomineeInvestmentAccountResourceRepresentationBase(): base(EstateItemType.NomineeInvestmentAccount){}

    public AddressResourceRepresentation Address { get; set; } = null!;
    public string? NomineeManager { get; set; }
    public string? AccountNumber { get; set; }
    public string? AccountType { get; set; }
    public IReadOnlyList<ListedInvestmentResourceRepresentation> ListedInvestments { get; set; } = null!;
    public decimal ProportionOwned { get; set; }
    public bool? IsPassedToSurvivingJointOwner { get; set; }
    public string? NotPassedDetails { get; set; }
    public decimal? DividendDue { get; set; }
    public decimal? InvestmentValue { get; set; }
    public decimal? Cash { get; set; }

    public string? ValuationBy { get; set; }

    public bool IsValidForInheritanceTax { get; set; }
    public EstateItemRealisationResourceRepresentation Realisation { get; set; } = null!;
    /// <remarks>This property is always null when <see cref="IsPassedToSurvivingJointOwner"/> is false.</remarks>
    public IReadOnlyList<Guid>? JointOwnerIds { get; set; }
    public bool HadControlOfTheCompany { get; set; }
}
