namespace Exizent.CaseManagement.Client.Models.Company;

public class CompanyAddressResourceRepresentation: AddressResourceRepresentation
{
    public Guid? Id { get; init; }
    public string? OfficeName { get; init; }
}