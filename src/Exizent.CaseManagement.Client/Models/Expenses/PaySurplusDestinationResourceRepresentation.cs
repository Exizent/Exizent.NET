namespace Exizent.CaseManagement.Client.Models.Expenses;

public class PaySurplusDestinationResourceRepresentation
{
    public PaySurplusToType? Type { get; init; }
    public Guid? CaseItemId { get; init; }
    public string? Other { get; init; }
}
