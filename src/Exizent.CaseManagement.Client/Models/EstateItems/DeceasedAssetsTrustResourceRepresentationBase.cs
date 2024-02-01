namespace Exizent.CaseManagement.Client.Models.EstateItems;

public abstract class DeceasedAssetsTrustResourceRepresentationBase : EstateItemResourceRepresentationBase
{
    protected DeceasedAssetsTrustResourceRepresentationBase(): base(EstateItemType.DeceasedAssetsTrust){}

    public DeceasedAssetsTrustType TrustType { get; set; }
    public string? NameOfSettlor { get; set; }
    public DateTime? SettlorDateOfDeathOrSettlementDate { get; set; }
    public string? Name { get; set; }
    public string? NameAndAgeOfPersonReceivingBenefit { get; init; }
    public List<TrusteeOrSolicitorResourceRepresentation> TrusteesAndSolicitors { get; set; } = new List<TrusteeOrSolicitorResourceRepresentation>();
    public string? UniqueTaxReferenceNumber { get; set; }
    public DateTime? TrustCreationDate { get; set; }
    public bool HasDetailsOfAssets { get; set; }
    public TrustAssetDetailsResourceRepresentation PropertyBusinessSharesAssets { get; set; } = null!;
    public TrustAssetDetailsResourceRepresentation OtherAssets { get; set; } = null!;
    public decimal? TotalValue { get; set; }
    public bool IsTaxOnTotalValueToBePaidNow { get; set; }
    public decimal? EstimatedValue { get; set; }
}