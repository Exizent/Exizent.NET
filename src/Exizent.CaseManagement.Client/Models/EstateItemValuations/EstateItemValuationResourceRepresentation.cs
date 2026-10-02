namespace Exizent.CaseManagement.Client.Models.EstateItemValuations;

/// <summary>
/// A requested estate item valuation, as much as a caller needs to wait for it. Once it is no longer pending, its
/// outcome is on the estate item's <c>Valuations</c>.
/// </summary>
public class EstateItemValuationResourceRepresentation
{
    public Guid Id { get; init; }

    /// <summary>Pending until the provider answers or the valuation times out.</summary>
    public EstateItemValuationStatus Status { get; init; }

    public bool IsPending => Status == EstateItemValuationStatus.Pending;
}
