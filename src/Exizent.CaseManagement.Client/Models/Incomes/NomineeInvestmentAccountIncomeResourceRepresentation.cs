using Dahomey.Json.Attributes;

namespace Exizent.CaseManagement.Client.Models.Incomes;

[JsonDiscriminator(nameof(IncomeType.NomineeInvestmentAccountIncome))]
public class NomineeInvestmentAccountIncomeResourceRepresentation : IncomeBaseResourceRepresentation
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

    public NomineeInvestmentAccountIncomeOrigin Origin { get; init; }

    public Guid? ListedInvestmentId { get; init; }

    /// <summary>
    /// The part of the account the income was paid into: <c>CashUninvested</c> for the account's uninvested cash,
    /// or null when it was paid into the account's value as a whole.
    /// </summary>
    public NiaDestination? NiaDestination { get; init; }
}
