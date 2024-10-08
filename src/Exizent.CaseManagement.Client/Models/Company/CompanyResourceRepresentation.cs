namespace Exizent.CaseManagement.Client.Models.Company;

public class CompanyResourceRepresentation
{
    public string? Name { get; init; }
    public string? OfficePhoneNumber { get; init; }
    public AddressResourceRepresentation? Address { get; init; }
}