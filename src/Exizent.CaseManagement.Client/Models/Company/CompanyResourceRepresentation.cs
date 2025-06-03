namespace Exizent.CaseManagement.Client.Models.Company;

public class CompanyResourceRepresentation
{
    public string? Name { get; init; }
    public string? ContactNumber { get; init; }
    public CompanyAddressResourceRepresentation Address { get; init; } = null!;
    public CompanyAddressResourceRepresentation PrimaryAddress { get; init; } = null!;
    public IReadOnlyList<CompanyAddressResourceRepresentation> Addresses { get; init; } = new List<CompanyAddressResourceRepresentation>();
}