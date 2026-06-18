namespace Exizent.CaseManagement.Client.Models.EstateItems;

public class ShareRealisationResourceRepresentation
{
    public DateOnly? ReceivedAt { get; init; }
    public RealisationDestination? Destination { get; init; }
    public string? OtherDestination { get; init; }
    public decimal Quantity { get; init; }
    public decimal Price { get; init; }
    public bool IncludeDateOfDeathShares { get; init; }
    public IReadOnlyList<Guid>? IncomeIds { get; init; }
    public Guid? CaseItemId { get; init; }
}
