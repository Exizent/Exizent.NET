namespace Exizent.CaseManagement.Client.Models.EstateItems;

public abstract class FinancialGiftResourceRepresentationBase : EstateItemResourceRepresentationBase
{
    protected FinancialGiftResourceRepresentationBase(): base(EstateItemType.FinancialGift){}

    public string? PreOwnedAssetNumber { get; set; }
    public DateTime? DateOfElection { get; set; }
    public string? RecipientFirstName { get; set; }
    public string? RecipientSurname { get; set; }
    public string? RecipientOrganisationName { get; set; }
    public string? Description { get; set; }
    public decimal? GrossValue { get; set; }
    public List<GiftExemptionResourceRepresentation> Exemptions { get; set; } = null!;
    public DateTime? DateOfGift { get; set; }
    public GiftType GiftType { get; set; }
    public RelationshipType? RelationshipToDeceased { get; set; }
    public string? OtherRelationshipToDeceasedDetails { get; set; }
}