namespace Exizent.CaseManagement.Client.Models;

public class EstateItemRealisationResourceRepresentation
{
    public DateTime? ReceivedAt { get; init; }
    public decimal? Value { get; init; }
    public RealisationDestination? Destination { get; init; }
    public string? OtherDestination { get; init; }
}