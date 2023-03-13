using Dahomey.Json.Attributes;

namespace Exizent.CaseManagement.Client.Models.EstateItems;

[JsonDiscriminator(nameof(EstateItemType.FinancialGift))]
public class FinancialGiftResourceRepresentation : EstateItemResourceRepresentation
{
    public string? RecipientFirstName { get; init; }
    public string? RecipientSurname { get; init; }
    public string? Description { get; init; }
    public decimal? GrossValue { get; init; }
    public decimal ExemptionValue { get; init; }
    public DateTime? DateOfGift { get; init; }
    public FinancialGiftExemptionCategory? ExemptionCategory { get; init; }
}