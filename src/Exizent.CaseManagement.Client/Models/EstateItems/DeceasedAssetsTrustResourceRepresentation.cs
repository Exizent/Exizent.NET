using Dahomey.Json.Attributes;

namespace Exizent.CaseManagement.Client.Models.EstateItems;

[JsonDiscriminator(nameof(EstateItemType.DeceasedAssetsTrust))]
public class DeceasedAssetsTrustResourceRepresentation : EstateItemResourceRepresentation
{
    public DeceasedAssetsTrustType TrustType { get; init; }
    public string? NameOfSettlor { get; init; }
    public DateTime? SettlorDateOfDeathOrSettlementDate { get; init; }
    public string? Name { get; init; }
    public List<TrusteeOrSolicitor> TrusteesAndSolicitors { get; init; } = new List<TrusteeOrSolicitor>();
    public string? UniqueTaxReferenceNumber { get; init; }
    public DateTime? TrustCreationDate { get; init; }
    public bool HasDetailsOfAssets { get; init; }
    public AssetDetails PropertyBusinessSharesAssets { get; init; } = null!;
    public AssetDetails OtherAssets { get; init; } = null!;
    public List<Exemption> Exemptions { get; init; } = null!;
    public decimal? TotalValue { get; init; }
}