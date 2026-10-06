using Dahomey.Json.Attributes;

namespace Exizent.CaseManagement.Client.Models.EstateItems;

[JsonDiscriminator(nameof(EstateItemType.NomineeInvestmentAccount))]
public class NomineeInvestmentAccountResourceRepresentation : EstateItemResourceRepresentation
{
    public AddressResourceRepresentation Address { get; init; } = null!;
    public string? NomineeManager { get; init; }
    public string? AccountNumber { get; init; }
    public string? AccountType { get; init; }
    public IReadOnlyList<InvestmentCategoryResourceRepresentation> InvestmentCategories { get; init; } = Array.Empty<InvestmentCategoryResourceRepresentation>();
    public decimal ProportionOwned { get; init; }
    public bool? IsPassedToSurvivingJointOwner { get; init; }
    public string? NotPassedDetails { get; init; }
    public decimal? DividendDue { get; init; }
    public decimal? InvestmentValue { get; init; }
    public decimal? Cash { get; init; }
    public decimal? CashUninvestedInterestDue { get; init; }
    public decimal? InterestDue { get; init; }

    public string? ValuationBy { get; init; }

    public bool? IsValidForInheritanceTax { get; init; }
    public EstateItemRealisationResourceRepresentation Realisation { get; init; } = null!;
    public IReadOnlyList<Guid> JointOwnerIds { get; init; } = Array.Empty<Guid>();
    public bool HadControlOfTheCompany { get; init; }
}
