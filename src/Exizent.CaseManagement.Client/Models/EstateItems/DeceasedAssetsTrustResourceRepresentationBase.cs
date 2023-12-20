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
    public AssetDetailsResourceRepresentation PropertyBusinessSharesAssets { get; set; } = null!;
    public AssetDetailsResourceRepresentation OtherAssets { get; set; } = null!;
    public List<ExemptionResourceRepresentation> Exemptions { get; set; } = null!;
    public decimal? AssetValue { get; init; }
    public decimal? DebtsPayableValue { get; init; }
    public decimal? ExemptionValue { get; init; }
    public decimal? TotalValue { get; set; }
}