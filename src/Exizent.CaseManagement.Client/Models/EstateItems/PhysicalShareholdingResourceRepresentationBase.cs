namespace Exizent.CaseManagement.Client.Models.EstateItems;

public abstract class PhysicalShareholdingResourceRepresentationBase : EstateItemResourceRepresentationBase, IStandardJointOwnership, IHasAddress, IRealisable
{
    protected PhysicalShareholdingResourceRepresentationBase(): base(EstateItemType.PhysicalShareholding){}

    public AddressResourceRepresentation? Address { get; set; }
    public string? ShareholdingName { get; set; }
    public string? LookupId { get; set; }
    public string? ShareholderReferenceNumber { get; set; }
    public decimal? Quantity { get; set; }
    public string? Description { get; set; }
    public string? ShareRegistrar { get; set; }
    public PhysicalShareholdingValuationResourceRepresentation? Valuation { get; set; }
    public decimal ProportionOwned { get; set; }
    public bool? IsPassedToSurvivingJointOwner { get; set; }
    public string? NotPassedDetails { get; set; }
    public decimal? DividendDue { get; set; }
    public string? ValuationBy { get; set; }
    public bool? IsListedOnRecognisedStockExchange { get; set; }
    public bool? IsTradedElsewhere { get; set; }
    public bool? IsListedOnUkExchanges { get; set; }
    public EstateItemRealisationResourceRepresentation? Realisation { get; set; }
    public bool? OwnedForTwoYears { get; set; }
    public decimal? BusinessRelief { get; set; }
    public CurrencyCode TradingCurrencyCode { get; set; }
    public decimal? ExchangeRate { get; set; }
    public bool IsValidForInheritanceTax { get; set; }
    public bool IsIncludedInEstateAccounts { get; set; }
    public bool HadControlOfTheCompany { get; set; }
    public bool? ShouldGroupByRegistrar { get; set; }
    public IReadOnlyList<Guid> JointOwnerIds { get; set; } = Array.Empty<Guid>();
    public IReadOnlyList<ShareRealisationResourceRepresentation>? Realisations { get; set; }

    AddressResourceRepresentation IHasAddress.Address
    {
        get => Address!;
        set => Address = value;
    }

    EstateItemRealisationResourceRepresentation IRealisable.Realisation
    {
        get => Realisation!;
        set => Realisation = value;
    }
}
