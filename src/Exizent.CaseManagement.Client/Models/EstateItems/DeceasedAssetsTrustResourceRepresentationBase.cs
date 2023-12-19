namespace Exizent.CaseManagement.Client.Models.EstateItems;

public abstract class DeceasedAssetsTrustResourceRepresentationBase : EstateItemResourceRepresentationBase
{
    protected DeceasedAssetsTrustResourceRepresentationBase(): base(EstateItemType.DeceasedAssetsTrust){}

    public DeceasedAssetsTrustType TrustType { get; set; }
    public string? NameOfSettlor { get; set; }
    public DateTime? SettlorDateOfDeathOrSettlementDate { get; set; }
    public string? Name { get; set; }
    public List<TrusteeOrSolicitor> TrusteesAndSolicitors { get; set; } = new List<TrusteeOrSolicitor>();
    public string? UniqueTaxReferenceNumber { get; set; }
    public DateTime? TrustCreationDate { get; set; }
    public bool HasDetailsOfAssets { get; set; }
    public AssetDetails PropertyBusinessSharesAssets { get; set; } = null!;
    public AssetDetails OtherAssets { get; set; } = null!;
    public List<Exemption> Exemptions { get; set; } = null!;
    public decimal? TotalValue { get; set; }
}