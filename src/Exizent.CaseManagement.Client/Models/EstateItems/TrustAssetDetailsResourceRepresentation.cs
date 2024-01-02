namespace Exizent.CaseManagement.Client.Models.EstateItems;

public class TrustAssetDetailsResourceRepresentation
{
    public List<TrustAssetResourceRepresentation> Assets { get; init; } = new List<TrustAssetResourceRepresentation>();
    public List<TrustLiabilityResourceRepresentation> Liabilities { get; init; } = new List<TrustLiabilityResourceRepresentation>();
    public List<TrustExemptionResourceRepresentation> Exemptions { get; init; } = new List<TrustExemptionResourceRepresentation>();
    public bool IsTaxToBePaidNow { get; init; }
}