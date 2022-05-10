using Dahomey.Json.Attributes;

namespace Exizent.CaseManagement.Client.Models.EstateItems;

[JsonDiscriminator(nameof(EstateItemType.CashIsa))]
public class CashIsaResourceRepresentation : EstateItemResourceRepresentation
{
    public string? Institution { get; init; }
    public string? AccountNumber { get; init; }
    public string? SortCode { get; init; }
    public bool IsNationalSavingsAndInvestments { get; init; }
    public NationalSavingsAndInvestmentsIsaProduct? NationalSavingsAndInvestmentsProduct { get; init; }
    public decimal? EstimatedBalance { get; init; }
    public decimal? ConfirmedBalance { get; init; }
    public decimal? InterestUpToDateOfDeath { get; init; }
    public AddressResourceRepresentation Address { get; init; } = null!;
    public EstateItemRealisationResourceRepresentation? Realisation { get; init; }
}