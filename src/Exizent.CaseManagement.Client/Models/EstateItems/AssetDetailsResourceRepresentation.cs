namespace Exizent.CaseManagement.Client.Models.EstateItems;

public class AssetDetailsResourceRepresentation
{
    public List<AssetResourceRepresentation> TrustAssets { get; init; } = new List<AssetResourceRepresentation>();
    public List<LiabilityResourceRepresentation> TrustLiabilities { get; init; } = new List<LiabilityResourceRepresentation>();
    public List<ExemptionResourceRepresentation> Exemptions { get; init; } = new List<ExemptionResourceRepresentation>();
    public bool IsTaxToBePaidNow { get; init; }
}