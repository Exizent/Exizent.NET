namespace Exizent.CaseManagement.Client.Models.EstateItems;

public class Exemption
{
    public DeceasedAssetsTrustExemptionType Type { get; init; }
    public decimal Value { get; init; }
    public string? Details { get; init; }
}