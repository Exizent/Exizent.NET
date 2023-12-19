namespace Exizent.CaseManagement.Client.Models.EstateItems;

public class AssetDetails
{
    public List<Asset> TrustAssets { get; init; } = new List<Asset>();
    public List<Liability> TrustLiabilities { get; init; } = new List<Liability>();
    public List<Exemption> Exemptions { get; init; } = new List<Exemption>();
    public bool IsTaxToBePaidNow { get; init; }
}