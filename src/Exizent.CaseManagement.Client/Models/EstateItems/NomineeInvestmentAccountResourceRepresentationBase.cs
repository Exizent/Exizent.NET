namespace Exizent.CaseManagement.Client.Models.EstateItems;

public class NomineeInvestmentAccountResourceRepresentationBase : EstateItemResourceRepresentationBase, IStandardJointOwnership, IHasAddress, ICanBeValidForIht, IRealisable
{
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
    public IReadOnlyList<Guid> JointOwnerIds { get; set; } = Array.Empty<Guid>();
    public bool HadControlOfTheCompany { get; set; }
}
