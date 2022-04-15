namespace Exizent.CaseManagement.Client.Models;

public class EndowmentPolicyResourceRepresentation : EstateItemResourceRepresentation
{
    public AddressResourceRepresentation Address { get; init; } = null!;
    public string? Provider { get; init; }
    public string? PolicyNumber { get; init; }
    public EndowmentPolicyType PolicyType { get; init; }
    public decimal? SumAssured { get; init; }
    public decimal? BonusDue { get; init; }
    public bool PaysOnDeathOfDeceased { get; init; }
    public string? Comments { get; init; }
}