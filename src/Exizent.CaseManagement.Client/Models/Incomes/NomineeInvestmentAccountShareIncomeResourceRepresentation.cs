using Dahomey.Json.Attributes;

namespace Exizent.CaseManagement.Client.Models.Incomes;

/// <summary>
/// A dividend or interest from a listed investment on a nominee investment account, received as shares.
/// <see cref="IncomeBaseResourceRepresentation.Value"/> is the whole value of the shares, the quantity times the unit
/// price divided by 100, rounded to the nearest penny.
/// </summary>
[JsonDiscriminator(nameof(IncomeType.NomineeInvestmentAccountShareIncome))]
public class NomineeInvestmentAccountShareIncomeResourceRepresentation : IncomeBaseResourceRepresentation
{
    public Guid EstateItemId { get; init; }

    public IncomeSource Source { get; init; }

    public string? OtherSource { get; init; }

    public DateTime From { get; init; }

    public DateTime To { get; init; }

    public bool? IsTaxable { get; init; }

    public AmountType? AmountType { get; init; }

    public Guid? DestinationCaseItemId { get; init; }

    public string? OtherDestination { get; init; }

    public bool IncludesValueUpToDateOfDeath { get; init; }

    public decimal? ValueIncludingUpToDateOfDeath { get; init; }

    /// <summary>The listed investment on the account the dividend or interest was earned on.</summary>
    public Guid? ListedInvestmentId { get; init; }

    /// <summary>The number of shares received.</summary>
    public decimal QuantityOfShares { get; init; }

    /// <summary>The unit price of the shares received, in pence.</summary>
    public decimal SharePrice { get; init; }
}
