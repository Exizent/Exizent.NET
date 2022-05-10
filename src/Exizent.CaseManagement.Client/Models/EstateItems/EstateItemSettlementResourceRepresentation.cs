namespace Exizent.CaseManagement.Client.Models.EstateItems;

public class EstateItemSettlementResourceRepresentation
{
    public Guid? ThirdPartyCreditorId { get; init; }
    public decimal? Value { get; init; }
    public Guid? CaseItemId { get; init; }
    public DateTime? ReceivedAt { get; init; }
}