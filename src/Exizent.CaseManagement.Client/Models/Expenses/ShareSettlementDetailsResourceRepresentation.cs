namespace Exizent.CaseManagement.Client.Models.Expenses;

public class ShareSettlementDetailsResourceRepresentation
{
    public int? Quantity { get; init; }
    public decimal? Price { get; init; }
    public PaySurplusDestinationResourceRepresentation? PaySurplus { get; init; }

    public decimal Value => (Quantity ?? 0) * (Price ?? 0) / 100m;
}
