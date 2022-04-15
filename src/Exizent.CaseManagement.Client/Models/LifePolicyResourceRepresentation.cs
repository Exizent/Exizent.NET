namespace Exizent.CaseManagement.Client.Models;

public class LifePolicyResourceRepresentation : EstateItemResourceRepresentation
{
    public AddressResourceRepresentation Address { get; init; } = null!;
    public string? Provider { get; init; }
    public string? PolicyNumber { get; init; }
    public bool IsValidForInheritanceTax { get; init; }
    public decimal? SumAssured { get; init; }
    public bool PaysOnDeathOfDeceased { get; init; }
    public string? BeneficiaryDetails { get; init; }
    public string? Comments { get; init; }
}