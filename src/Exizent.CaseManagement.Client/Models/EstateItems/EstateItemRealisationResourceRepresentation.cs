namespace Exizent.CaseManagement.Client.Models.EstateItems;

public class EstateItemRealisationResourceRepresentation
{
    public DateTime? ReceivedAt { get; init; }
    public decimal? Value { get; init; }
    public RealisationDestination? Destination { get; init; }
    public string? OtherDestination { get; init; }
    public Guid? BankAccountId { get; init; }
}