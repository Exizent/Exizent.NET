namespace Exizent.CaseManagement.Client.Models.EstateItemValuations;

/// <summary>
/// An automated valuation of an estate item, from request to result. Informational only: nothing calculates with it.
/// </summary>
public class EstateItemValuationResourceRepresentation
{
    public Guid Id { get; init; }
    public EstateItemValuationProvider Provider { get; init; }

    /// <summary>Pending until the provider answers or the valuation times out.</summary>
    public EstateItemValuationStatus Status { get; init; }

    public DateTime RequestedAt { get; init; }

    /// <summary>When the valuation completed or failed. Null while pending.</summary>
    public DateTime? FinishedAt { get; init; }

    /// <summary>Set when completed.</summary>
    public decimal? Value { get; init; }

    public EstateItemValuationConfidence? Confidence { get; init; }

    /// <summary>Set when failed.</summary>
    public EstateItemValuationFailure? Failure { get; init; }

    public bool IsPending => Status == EstateItemValuationStatus.Pending;
}
