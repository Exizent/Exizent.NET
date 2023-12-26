using Dahomey.Json.Attributes;

namespace Exizent.CaseManagement.Client.Models.EstateItems;

[JsonDiscriminator(nameof(EstateItemType.DeceasedAssetsTrust))]
public class DeceasedAssetsTrustResourceRepresentation : EstateItemResourceRepresentation
{
    public DeceasedAssetsTrustType TrustType { get; init; }
    public string? NameOfSettlor { get; init; }
    public DateTime? SettlorDateOfDeathOrSettlementDate { get; init; }
    public string? Name { get; init; }
    public string? NameAndAgeOfPersonReceivingBenefit { get; init; }
    public List<TrusteeOrSolicitorResourceRepresentation> TrusteesAndSolicitors { get; init; } = new List<TrusteeOrSolicitorResourceRepresentation>();
    public string? UniqueTaxReferenceNumber { get; init; }
    public DateTime? TrustCreationDate { get; init; }
    public bool HasDetailsOfAssets { get; init; }
    public AssetDetailsResourceRepresentation PropertyBusinessSharesAssets { get; init; } = null!;
    public AssetDetailsResourceRepresentation OtherAssets { get; init; } = null!;
    public List<ExemptionResourceRepresentation> Exemptions { get; init; } = null!;
    public decimal? TotalValue { get; init; }
}