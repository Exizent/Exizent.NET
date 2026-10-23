using Dahomey.Json.Attributes;

namespace Exizent.CaseManagement.Client.Models.Incomes;

[JsonDiscriminator((nameof(IncomeType.Income)))]
public class IncomeResourceRepresentation : IncomeBaseResourceRepresentation
{
    
    public Guid? EstateItemId { get; init; }

    public IncomeSource? Source { get; init; }

    public string? OtherSource { get; init; }

    public DateTime? From { get; init; }

    public DateTime? To { get; init; }

    public AmountType? AmountType { get; init; }

    [Obsolete("Use DestinationCaseItemId, or OtherDestination when the income is not paid to a case item, instead.")]
    public IncomeDestination? Destination { get; init; }

    public Guid? DestinationCaseItemId { get; init; }

    public string? OtherDestination { get; init; }

    public bool IncludesValueUpToDateOfDeath { get; init; }

    public decimal? ValueIncludingUpToDateOfDeath { get; init; }
}