namespace Exizent.CaseManagement.Client.Models.EstateItemValuations;

/// <summary>An automated valuation of an estate item, from request to result.</summary>
public class EstateItemValuationResourceRepresentation
{
    public Guid Id { get; init; }
    public Guid EstateItemId { get; init; }
    public EstateItemValuationProvider Provider { get; init; }

    /// <summary>Pending until the provider answers or the valuation times out.</summary>
    public EstateItemValuationStatus Status { get; init; }

    public DateTime RequestedAt { get; init; }

    /// <summary>Not set while pending.</summary>
    public DateTime? CompletedAt { get; init; }

    /// <summary>What was sent for valuation, as the estate item held it when requested.</summary>
    public EstateItemValuationSubjectResourceRepresentation Subject { get; init; } = null!;

    /// <summary>Set when completed.</summary>
    public decimal? Value { get; init; }

    public EstateItemValuationConfidence? Confidence { get; init; }

    /// <summary>Set when failed.</summary>
    public string? FailureReason { get; init; }

    /// <summary>What the provider said about the subject beyond its value, when it said anything.</summary>
    public EstateItemValuationDetailsResourceRepresentation? Details { get; init; }

    public bool IsPending => Status == EstateItemValuationStatus.Pending;
}
