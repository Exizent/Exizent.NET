namespace Exizent.CaseManagement.Client.Models.EstateItems;

public class GetShareRealisationResourceRepresentation
{
    public Guid Id { get; init; }
    public DateOnly? ReceivedAt { get; init; }
    public RealisationDestination? Destination { get; init; }
    public string? OtherDestination { get; init; }
    public decimal Quantity { get; init; }
    public decimal Price { get; init; }
    public decimal Value { get; init; }
    public bool IncludeDateOfDeathShares { get; init; }
    public IReadOnlyList<Guid> IncomeIds { get; init; } = Array.Empty<Guid>();
    public Guid? CaseItemId { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}
