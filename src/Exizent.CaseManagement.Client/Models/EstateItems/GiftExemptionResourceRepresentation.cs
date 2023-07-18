namespace Exizent.CaseManagement.Client.Models.EstateItems;

public class GiftExemptionResourceRepresentation
{
    public FinancialGiftExemptionCategory Category { get; init; }
    public string? Details  { get; init; }
    public decimal? Value  { get; init; }
}