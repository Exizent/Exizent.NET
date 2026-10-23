namespace Exizent.CaseManagement.Client.Models.Expenses;

public class PaySurplusDestinationResourceRepresentation
{
    [Obsolete("Use CaseItemId, or Other when the surplus is not paid to a case item, instead.")]
    public PaySurplusToType? Type { get; init; }
    public Guid? CaseItemId { get; init; }
    public string? Other { get; init; }
}
