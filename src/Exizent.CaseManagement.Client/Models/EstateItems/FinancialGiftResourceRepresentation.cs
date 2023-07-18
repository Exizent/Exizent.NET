using Dahomey.Json.Attributes;
using Exizent.CaseManagement.Client.Models.People;

namespace Exizent.CaseManagement.Client.Models.EstateItems;

[JsonDiscriminator(nameof(EstateItemType.FinancialGift))]
public class FinancialGiftResourceRepresentation : EstateItemResourceRepresentation
{
    public string? RecipientFirstName { get; init; }
    public string? RecipientSurname { get; init; }
    public string? RecipientOrganisationName { get; set; }
    public string? Description { get; init; }
    public decimal? GrossValue { get; init; }
    public decimal? ExemptionValue { get; init; }
    public List<GiftExemptionResourceRepresentation> Exemptions { get; init; } = null!;
    public DateTime? DateOfGift { get; init; }
    public FinancialGiftExemptionCategory? ExemptionCategory { get; init; }
    public string? ExemptionDetails { get; init; }
    public GiftType GiftType { get; init; }
    public RelationshipType? RelationshipToDeceased { get; set; }
    public string? OtherRelationshipToDeceasedDetails { get; set; }
}