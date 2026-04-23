using Dahomey.Json.Attributes;

namespace Exizent.CaseManagement.Client.Models.Incomes;

[JsonDiscriminator(nameof(IncomeType.ShareholdingIncome))]
public class ShareholdingIncomeResourceRepresentation : IncomeBaseResourceRepresentation
{
    public Guid EstateItemId { get; init; }

    public IncomeSource Source { get; init; }

    public string? OtherSource { get; init; }

    public DateTime From { get; init; }

    public DateTime To { get; init; }

    public bool? IsTaxable { get; init; }

    public AmountType? AmountType { get; init; }

    public IncomeDestination Destination { get; init; }

    public string? OtherDestination { get; init; }

    public bool IncludesValueUpToDateOfDeath { get; init; }

    public decimal? ValueIncludingUpToDateOfDeath { get; init; }

    public decimal QuantityOfShares { get; init; }

    public decimal SharePrice { get; init; }

    public decimal? Cash { get; init; }

    public string? ShareholderReferenceNumber { get; init; }
}
