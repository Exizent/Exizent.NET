using Dahomey.Json.Attributes;

namespace Exizent.CaseManagement.Client.Models.EstateItems;

[JsonDiscriminator(nameof(EstateItemType.NomineeInvestmentAccount))]
public class NomineeInvestmentAccountResourceRepresentation : EstateItemResourceRepresentation
{
    public AddressResourceRepresentation Address { get; init; } = null!;
    public string? NomineeManager { get; init; }
    public string? AccountNumber { get; init; }
    public string? AccountType { get; init; }
    public IReadOnlyList<ListedInvestmentResourceRepresentation> ListedInvestments { get; init; } = null!;
    public decimal ProportionOwned { get; init; }
    public bool? IsPassedToSurvivingJointOwner { get; init; }
    public string? NotPassedDetails { get; init; }
    public decimal? DividendDue { get; init; }
    public decimal? InvestmentValue { get; init; }
    public decimal? Cash { get; init; }

    public string? ValuationBy { get; init; }

    public bool? IsValidForInheritanceTax { get; init; }
    public EstateItemRealisationResourceRepresentation Realisation { get; init; } = null!;
    /// <remarks>This property is always null when <see cref="IsPassedToSurvivingJointOwner"/> is false.</remarks>
    public IReadOnlyList<Guid>? JointOwnerIds { get; init; }
    public bool HadControlOfTheCompany { get; init; }
}
