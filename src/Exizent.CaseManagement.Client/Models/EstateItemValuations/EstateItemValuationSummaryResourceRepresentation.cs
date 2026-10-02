namespace Exizent.CaseManagement.Client.Models.EstateItemValuations;

/// <summary>
/// A finished automated valuation, as recorded on the estate item. Informational only: nothing calculates with it.
/// </summary>
public class EstateItemValuationSummaryResourceRepresentation
{
    public Guid Id { get; init; }
    public EstateItemValuationProvider Provider { get; init; }
    public EstateItemValuationStatus Status { get; init; }
    public DateTime ValuedAt { get; init; }

    /// <summary>Set when completed, null when failed.</summary>
    public decimal? Value { get; init; }

    public EstateItemValuationConfidence? Confidence { get; init; }

    /// <summary>Why a failed valuation failed.</summary>
    public string? FailureReason { get; init; }
}
