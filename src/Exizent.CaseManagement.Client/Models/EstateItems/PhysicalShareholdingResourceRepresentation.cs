using Dahomey.Json.Attributes;

namespace Exizent.CaseManagement.Client.Models.EstateItems;

[JsonDiscriminator(nameof(EstateItemType.PhysicalShareholding))]
public class PhysicalShareholdingResourceRepresentation : EstateItemResourceRepresentation
{
    public AddressResourceRepresentation? Address { get; init; }
    public string? ShareholdingName { get; init; }
    public string? LookupId { get; init; }
    public string? ShareholderReferenceNumber { get; init; }
    public decimal? Quantity { get; init; }
    public string? Description { get; init; }
    public string? ShareRegistrar { get; init; }
    public PhysicalShareholdingValuationResourceRepresentation? Valuation { get; init; }
    public decimal? Price { get; init; }
    public decimal ProportionOwned { get; init; }
    public bool? IsPassedToSurvivingJointOwner { get; init; }
    public string? NotPassedDetails { get; init; }
    public decimal? DividendDue { get; init; }
    public string? ValuationBy { get; init; }
    [Obsolete("Use IsListedOnRecognisedStockExchange instead.")]
    public bool IsQuotedOnLondonStockExchange { get; init; }
    public bool? IsListedOnRecognisedStockExchange { get; init; }
    public bool? IsTradedElsewhere { get; init; }
    public bool? IsListedOnUkExchanges { get; init; }
    public EstateItemRealisationResourceRepresentation? Realisation { get; init; }
    public IReadOnlyList<Guid> JointOwnerIds { get; init; } = Array.Empty<Guid>();
    public bool? OwnedForTwoYears { get; init; }
    public decimal? BusinessRelief { get; init; }
    public CurrencyCode TradingCurrencyCode { get; init; }
    public decimal? ExchangeRate { get; init; }
    public bool IsValidForInheritanceTax { get; init; }
    public bool IsIncludedInEstateAccounts { get; init; }
    public bool HadControlOfTheCompany { get; init; }
    public bool? ShouldGroupByRegistrar { get; init; }
    public IReadOnlyList<GetShareRealisationResourceRepresentation> Realisations { get; init; } = Array.Empty<GetShareRealisationResourceRepresentation>();
}
