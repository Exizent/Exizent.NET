namespace Exizent.CaseManagement.Client.Models.EstateItems;

public class TrustAssetResourceRepresentation
{
    public TrustAssetType? Type { get; init; }
    public string Description { get; init; } = null!;
    public decimal Value { get; init; }
}